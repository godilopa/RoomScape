
using UnityEngine.Playables;

namespace TestKeplerians
{
  public class ReactionBehaviour : PlayableBehaviour
  {
    public string reactionMsg;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
      if (GameManager.IsSingletonValid == true)
        GameManager.Instance.Player.PlayerCanvas.ShowReacionTextDuringTime(reactionMsg, (float)playable.GetDuration());
    }
  }
}
