using System.Collections.Generic;
using UnityEngine;

namespace TestKeplerians
{
  public class Puzzle : InteractableObject
  {
    public bool IsSolved { get => isSolved; set => isSolved = value; }

    public List<PuzzleItem> Items { get => items; }

    public string PuzzleId { get => puzzleId; }

    [SerializeField]
    private string puzzleId;

    [SerializeField]
    private int[] itemsToUnlock;

    [SerializeField]
    private string reactionMsgOnSolved;

    private bool isSolved;

    private List<PuzzleItem> items = new List<PuzzleItem>();

    private List<int> itemLeft = new List<int>();

    override protected void ReactionToInteract(params object[] extraInfo)
    {
      if (isSolved)
        return;

      PickableObject pickableSent = null;
      if (extraInfo.Length != 0)
        pickableSent = extraInfo[0] as PickableObject;

      if (pickableSent != null)
      {
        PuzzleItem puzzleItem = pickableSent.GetComponent<PuzzleItem>();

        if (items.Contains(puzzleItem))
        {
          textReaction = puzzleItem.ReactionToUsed;
          itemLeft.Remove(puzzleItem.ItemId);
          puzzleItem.ItemUsed();
          //@TODO:Añadir cada item provoca un cambio en el texto aparte del que ha ocurrido en la reaccion

          if (CheckSolved())
            Solved();
        }
      }

      FinishInteraction();
    }

    public void UseAllItems()
    {
      for (int i = 0; i < items.Count; i++)
      {
        if (items[i].IsUsed == false) //@HACK:Para items que no sean solucion pero sean parte del puzzle (como la puerta que es el propio puzzle)
          items[i].ItemUsed();
      }
    }

    public void SetPuzzle()
    {
      if (IsSolved == true)
        UseAllItems();
      else
        itemLeft.AddRange(itemsToUnlock);
    }

    private void Solved()
    {
      textReaction = reactionMsgOnSolved;

      isSolved = true;
      UseAllItems();

      if (PuzzleManager.IsSingletonValid)
        PuzzleManager.Instance.UpdatePuzzleInfo(puzzleId, isSolved);
    }

    private bool CheckSolved()
    {
      return itemLeft.Count == 0;
    }
  }
}