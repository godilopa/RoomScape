
using UnityEngine;

namespace TestKeplerians
{
  public class PuzzleItemColliderDissapear : PuzzleItem
  {
    [SerializeField]
    private Collider disableCollider;

    protected override void ItemUsedImplementation()
    {
      if (disableCollider != null)
        disableCollider.enabled = false;
    }
  }
}
