
using UnityEngine;

namespace TestKeplerians
{
  public class PuzzleItemDissapear : PuzzleItem
  {
    [SerializeField]
    private GameObject disableGameObject;

    protected override void ItemUsedImplementation()
    {
      if (disableGameObject != null)
        disableGameObject.SetActive(false);
    }
  }
}
