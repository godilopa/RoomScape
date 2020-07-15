
using System.Collections.Generic;
using UnityEngine;

namespace TestKeplerians
{
  [CreateAssetMenu(fileName = "Puzzles Data", menuName = "ScriptableObjects/Keplerians/PuzzleAsset", order = 1)]
  public class PuzzleAsset : ScriptableObject
  {
    public List<PuzzleDataInfo> PuzzlesInfo { get => puzzlesInfo; }

    [System.Serializable]
    public struct PuzzleDataInfo
    {
      public string idName;
      public bool completed;
    }

    [SerializeField]
    private List<PuzzleDataInfo> puzzlesInfo;
  }
}
