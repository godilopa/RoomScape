
using UnityEngine;

namespace TestKeplerians
{
  public class InteractableGroup : MonoBehaviour
  {
    [SerializeField]
    private InteractableObject[] interactables;

    public void AdviceOthers(InteractableObject advisor)
    {
      for (int i = 0; i < interactables.Length; i++)
      {
        if (interactables[i] != advisor)
          interactables[i].OnInteract();
      }
    }

    private void Start()
    {
      for (int i = 0; i < interactables.Length; i++)
        interactables[i].InteractableGroup = this;
    }
  }
}
