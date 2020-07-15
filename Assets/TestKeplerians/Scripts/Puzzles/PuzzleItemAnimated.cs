
using UnityEngine;

namespace TestKeplerians
{
  [RequireComponent(typeof(AnimatedObjectController))]
  public class PuzzleItemAnimated : PuzzleItem
  {
    [SerializeField]
    private string animatedTrigerNameOnUsed;

    private AnimatedObjectController animatedObjectController;

    protected override void ItemUsedImplementation()
    {
      animatedObjectController.TriggerState(animatedTrigerNameOnUsed);
    }

    private void Awake()
    {
      animatedObjectController = GetComponent<AnimatedObjectController>();
    }
  }
}
