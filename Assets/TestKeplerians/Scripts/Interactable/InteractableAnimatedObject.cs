using UnityEngine;

namespace TestKeplerians
{
  [RequireComponent(typeof(AnimatedObjectController))]
  public class InteractableAnimatedObject : InteractableObject
  {
    [Header("Animated Options")]
    [SerializeField]
    private string animatorState1;

    [SerializeField]
    private string animatorState2;

    [SerializeField]
    private string overrideReactionMsg;

    private AnimatedObjectController animatedObjectController;

    private bool toggleReaction = false;

    private string originalReactionMsg;

    override protected void ReactionToInteract(params object[] extraInfo)
    {
      if (toggleReaction == false)
      {
        textReaction = originalReactionMsg;
        animatedObjectController.TriggerState(animatorState1);
        toggleReaction = true;
      }
      else
      {
        textReaction = overrideReactionMsg;
        animatedObjectController.TriggerState(animatorState2);
        toggleReaction = false;
      }
    }

    private void Awake()
    {
      animatedObjectController = GetComponent<AnimatedObjectController>();
    }

    private void Start()
    {
      originalReactionMsg = textReaction;
    }

    private void Update()
    {
      if (IsInteracting == true && animatedObjectController.PlayingInteractableState() == false)
        FinishInteraction();
    }
  }
}
