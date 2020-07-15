using UnityEngine;

namespace TestKeplerians
{
  [DisallowMultipleComponent]
  public class InteractableObject : MonoBehaviour, IInteractable
  {
    public bool IsInteracting { get => isInteracting; }

    public string TextName { get => textName; }

    public string TextReaction { get => textReaction; }

    public string TextThought { get => textThought; }

    public InteractableGroup InteractableGroup { get => interactableGroup; set => interactableGroup = value; }

    [Header("Messages")]

    [SerializeField]
    protected string textName;

    [SerializeField]
    protected string textReaction;

    [SerializeField]
    protected string textThought;

    private bool isInteracting = false;

    private InteractableGroup interactableGroup;

    protected virtual void ReactionToInteract(params object[] extraInfo)
    {

    }

    protected void FinishInteraction()
    {
      isInteracting = false;
    }

    public void OnInteract(params object[] extraInfo)
    {
      if (isInteracting == false)
      {
        isInteracting = true;

        if (interactableGroup != null)
          interactableGroup.AdviceOthers(this);

        ReactionToInteract(extraInfo);
      }
    }
  }
}
