using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class CarneGameManager : MonoBehaviour
{
      public static CarneGameManager Instance { get; private set; }

      [Header("UI Grid Setup")]
      [SerializeField] private RectTransform gridContainer;
      [SerializeField] private GameObject tilePrefab;
      [SerializeField] private GridLayoutGroup gridLayout;

      [Header("Level Data")]
      public CarneLevelData[] levels;       
      [SerializeField] private float timeLimit = 60f;        

      private CarneLevelData currentLevel;      

      [Header("UI Text")]
      [SerializeField] private TMP_Text timerText;
      [SerializeField] private TMP_Text levelTitleText;

      private List<CarneGridTile> allTiles = new List<CarneGridTile>();
      private List<CarneGridTile> occupiedTiles = new List<CarneGridTile>();        
      private List<CarneGridTile> currentPath = new List<CarneGridTile>();        
      private List<List<CarneGridTile>> completedPaths = new List<List<CarneGridTile>>();         

      private bool isDragging = false;
      private Color activeColor;
      private CarneGridTile lastPoint;       
      private int completedLines = 0;

      private void Awake()
      {
            Instance = this;
      }

      private void Start()
      {
            int levelIndex = CarneScoreManager.Instance != null ? CarneScoreManager.Instance.CurrentLevelIndex : 0;

            if (levelIndex >= levels.Length) levelIndex = levels.Length - 1;
            if (levelIndex < 0) levelIndex = 0;

            currentLevel = levels[levelIndex];

            if (levelTitleText != null)
            {
                  levelTitleText.text = "Nível: " + (levelIndex + 1);
            }

            CreateGrid();
      }

      private void Update()
      {
            float timeLeft = timeLimit - CarneScoreManager.Instance.LevelTime;

            if (timeLeft <= 0)
            {
                  CarneScoreManager.Instance.SetWinState(false);
                  CarneScoreManager.Instance.SetScore(0);
                  SceneManager.LoadScene("CarneFinalScene");
                  return;
            }

            if (timerText != null)
            {
                  float seconds = Mathf.FloorToInt(timeLeft % 60);
                  timerText.text = $"{seconds:00}";
            }
      }

      public static int GetScore()
      {
            return CarneScoreManager.Instance.Score;
      }

      private void CreateGrid()
      {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = currentLevel.gridSize;

            for (int y = currentLevel.gridSize - 1; y >= 0; y--)
            {
                  for (int x = 0; x < currentLevel.gridSize; x++)
                  {
                        GameObject obj = Instantiate(tilePrefab, gridContainer);
                        CarneGridTile tile = obj.GetComponent<CarneGridTile>();
                        tile.Initialize(x, y);
                        allTiles.Add(tile);
                  }
            }

            foreach (DotPair pair in currentLevel.dotPairs)
            {
                  CarneGridTile tile1 = GetTileAt(pair.position1.x, pair.position1.y);
                  if (tile1 != null) tile1.SetDot(pair.dotSprite, pair.lineColor);

                  CarneGridTile tile2 = GetTileAt(pair.position2.x, pair.position2.y);
                  if (tile2 != null) tile2.SetDot(pair.dotSprite, pair.lineColor);
            }
      }

      private CarneGridTile GetTileAt(int x, int y)
      {
            foreach (CarneGridTile t in allTiles)
            {
                  if (t.Coordinate.x == x && t.Coordinate.y == y) return t;
            }
            return null;
      }

      public void StartDrawing(CarneGridTile clickedTile)
      {
            if (clickedTile == null) return;

            if (occupiedTiles.Contains(clickedTile))
            {
                  DeleteCompletedLineContaining(clickedTile);
            }

            if (clickedTile.IsDot && !occupiedTiles.Contains(clickedTile))
            {
                  isDragging = true;
                  activeColor = clickedTile.DotColor;
                  lastPoint = clickedTile;

                  currentPath.Clear();
                  currentPath.Add(clickedTile);

                  occupiedTiles.Add(clickedTile);
            }
      }

      public void ContinueDrawing(CarneGridTile hoveredTile)
      {
            if (!isDragging || hoveredTile == null) return;

            if (hoveredTile != lastPoint)
            {
                  TryConnectTile(hoveredTile);
            }
      }

      public void StopDrawing()
      {
            if (isDragging)
            {
                  isDragging = false;
                  UndoCurrentPath();       
            }
      }

      private void UndoCurrentPath()
      {
            foreach (CarneGridTile tile in currentPath)
            {
                  tile.ClearLines();
                  occupiedTiles.Remove(tile);
            }

            currentPath.Clear();
      }

      private void DeleteCompletedLineContaining(CarneGridTile clickedTile)
      {
            List<CarneGridTile> pathToRemove = null;

            foreach (var path in completedPaths)
            {
                  if (path.Contains(clickedTile))
                  {
                        pathToRemove = path;
                        break;
                  }
            }

            if (pathToRemove != null)
            {
                  foreach (CarneGridTile tile in pathToRemove)
                  {
                        tile.ClearLines();
                        occupiedTiles.Remove(tile);
                  }

                  completedPaths.Remove(pathToRemove);
                  completedLines--;      
            }
      }

      private void TryConnectTile(CarneGridTile newTile)
      {
            int dist = Mathf.Abs(newTile.Coordinate.x - lastPoint.Coordinate.x) +
                       Mathf.Abs(newTile.Coordinate.y - lastPoint.Coordinate.y);

            if (dist != 1) return;   

            if (newTile.IsDot)
            {
                  if (newTile.DotColor == activeColor && newTile != currentPath[0])
                  {
                        ConnectVisuals(lastPoint, newTile);
                        if (!occupiedTiles.Contains(newTile)) occupiedTiles.Add(newTile);

                        isDragging = false;     

                        completedLines++;

                        List<CarneGridTile> finishedPath = new List<CarneGridTile>(currentPath);
                        finishedPath.Add(newTile);       
                        completedPaths.Add(finishedPath);

                        if (completedLines == currentLevel.dotPairs.Count)
                        {
                              CarneScoreManager.Instance.SetWinState(true);

                              float timePercent = CarneScoreManager.Instance.LevelTime / timeLimit;
                              int finalScore = 1;

                              if (timePercent <= 0.333f) finalScore = 3;
                              else if (timePercent <= 0.666f) finalScore = 2;

                              CarneScoreManager.Instance.SetScore(finalScore);

                              SceneManager.LoadScene("CarneFinalScene");  
                        }
                  }
                  return;        
            }

            if (occupiedTiles.Contains(newTile))
            {
                  return;     
            }

            occupiedTiles.Add(newTile);
            currentPath.Add(newTile);
            ConnectVisuals(lastPoint, newTile);

            lastPoint = newTile;
      }

      private void ConnectVisuals(CarneGridTile from, CarneGridTile to)
      {
            Vector2Int dir = to.Coordinate - from.Coordinate;

            from.EnableLine(dir, activeColor);

            to.EnableLine(-dir, activeColor);
      }

}
