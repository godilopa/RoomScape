
using UnityEngine;

namespace TestKeplerians
{
  public abstract class PuzzleItem : MonoBehaviour
  {
    public string PuzzleIBelong { get => puzzleIBelong; }

    public string ReactionToUsed { get => reactionToUsed; }

    public int ItemId { get => itemId; }

    public bool IsUsed { get => isUsed; }

    [SerializeField]
    private string puzzleIBelong;

    [SerializeField]
    private int itemId;

    [SerializeField]
    private string reactionToUsed;

    private bool isUsed;

    abstract protected void ItemUsedImplementation();

    public void ItemUsed()
    {
      if (isUsed == false)
      {
        isUsed = true;
        ItemUsedImplementation();
      }
    }
  }
}

