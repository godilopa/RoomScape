
using System.Collections.Generic;
using UnityEngine;

namespace TestKeplerians
{
  public class PuzzleManager : Singleton<PuzzleManager>
  {
    [SerializeField]
    private PuzzleAsset scriptablePuzzle;

    private Dictionary<string, bool> puzzleDataDictionary = new Dictionary<string, bool>();

    public void UpdatePuzzleInfo(string puzzleId, bool solved)
    {
      if (puzzleDataDictionary.ContainsKey(puzzleId))
        puzzleDataDictionary[puzzleId] = solved;
    }

    public void PreparePuzzles()
    {
      PuzzleItem[] items = FindObjectsOfType<PuzzleItem>();
      Puzzle[] puzzles = FindObjectsOfType<Puzzle>();

      for (int i = 0; i < puzzles.Length; i++)
      {
        for (int j = 0; j < items.Length; j++)
        {
          if (string.Compare(items[j].PuzzleIBelong, puzzles[i].PuzzleId) == 0)
            puzzles[i].Items.Add(items[j]);
        }

        puzzles[i].IsSolved = puzzleDataDictionary[puzzles[i].PuzzleId];
        puzzles[i].SetPuzzle();
      }
    }

    private void Start()
    {
      for (int i = 0; i < scriptablePuzzle.PuzzlesInfo.Count; i++)
        puzzleDataDictionary[scriptablePuzzle.PuzzlesInfo[i].idName] = scriptablePuzzle.PuzzlesInfo[i].completed;
    }
  }
}

