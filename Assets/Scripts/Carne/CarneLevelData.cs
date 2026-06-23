using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DotPair
{
      public Sprite dotSprite;                 
      public Color lineColor;                 
      public Vector2Int position1;        
      public Vector2Int position2;        
}

[System.Serializable]
public class CarneLevelData
{
      public int gridSize = 5;
      public List<DotPair> dotPairs = new List<DotPair>();
}
