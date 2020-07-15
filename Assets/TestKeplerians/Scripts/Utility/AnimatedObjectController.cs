
using UnityEngine;

namespace TestKeplerians
{
  [RequireComponent(typeof(Animator))]
  public class AnimatedObjectController : MonoBehaviour
  {
    private Animator animator;

    private int interactable = Animator.StringToHash("CanInteract");

    public void TriggerState(string state)
    {
      animator.SetTrigger(state);
    }

    public bool PlayingInteractableState()
    {
      return animator.GetCurrentAnimatorStateInfo(0).tagHash == interactable;
    }

    private void Awake()
    {
      animator = GetComponent<Animator>();
    }
  }
}
