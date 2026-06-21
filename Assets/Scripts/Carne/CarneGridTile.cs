using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarneGridTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
      [Header("UI References")]
      [SerializeField] private Image dotImage;
      [SerializeField] private Image upLine;
      [SerializeField] private Image downLine;
      [SerializeField] private Image leftLine;
      [SerializeField] private Image rightLine;

      public Vector2Int Coordinate { get; private set; }
      public bool IsDot { get; private set; }
      public Color DotColor { get; private set; }

      public void Initialize(int x, int y)
      {
            Coordinate = new Vector2Int(x, y);
            ResetTile();
      }

      public void SetDot(Sprite sprite, Color lineColor)
      {
            IsDot = true;
            DotColor = lineColor;           

            if (dotImage != null)
            {
                  dotImage.gameObject.SetActive(true);
                  dotImage.sprite = sprite;
                  dotImage.color = Color.white;        
            }
      }

      public void ResetTile()
      {
            IsDot = false;
            DotColor = Color.clear;

            if (dotImage != null)
            {
                  dotImage.gameObject.SetActive(false);
            }

            ClearLines();
      }

      public void ClearLines()
      {
            if (upLine != null) upLine.gameObject.SetActive(false);
            if (downLine != null) downLine.gameObject.SetActive(false);
            if (leftLine != null) leftLine.gameObject.SetActive(false);
            if (rightLine != null) rightLine.gameObject.SetActive(false);
      }

      public void EnableLine(Vector2Int direction, Color color)
      {
            if (direction == Vector2Int.up && upLine != null)
            {
                  upLine.gameObject.SetActive(true);
                  upLine.color = color;
            }
            else if (direction == Vector2Int.down && downLine != null)
            {
                  downLine.gameObject.SetActive(true);
                  downLine.color = color;
            }
            else if (direction == Vector2Int.left && leftLine != null)
            {
                  leftLine.gameObject.SetActive(true);
                  leftLine.color = color;
            }
            else if (direction == Vector2Int.right && rightLine != null)
            {
                  rightLine.gameObject.SetActive(true);
                  rightLine.color = color;
            }
      }

      public void OnPointerDown(PointerEventData eventData)
      {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                  CarneGameManager.Instance.StartDrawing(this);
            }
      }

      public void OnPointerEnter(PointerEventData eventData)
      {
            CarneGameManager.Instance.ContinueDrawing(this);
      }

      public void OnPointerUp(PointerEventData eventData)
      {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                  CarneGameManager.Instance.StopDrawing();
            }
      }
}
