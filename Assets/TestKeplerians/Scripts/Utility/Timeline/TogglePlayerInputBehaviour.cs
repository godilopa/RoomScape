
using UnityEngine.Playables;

namespace TestKeplerians
{
  public class TogglePlayerInputBehaviour : PlayableBehaviour
  {
    public bool active;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
      if (GameManager.IsSingletonValid == true)
        GameManager.Instance.Player.SetPlayerInput(active);
    }

    public override void OnGraphStop(Playable playable)
    {
      if (GameManager.IsSingletonValid == true)
        GameManager.Instance.Player.SetPlayerInput(!active);
    }
  }
}
