
using UnityEngine;
using UnityEngine.Playables;

namespace TestKeplerians
{
  //@HACK: Para controlar los ajustes al comienzo de la escena, lo suyo sería añadir una máquina de estados.
  [DefaultExecutionOrder(-5000)]
  public class FakeLevelStateMachine : MonoBehaviour
  {
    [SerializeField]
    private PlayableDirector sceneDirector;

    [SerializeField]
    private PlayableAsset initialCinematic;

    [SerializeField]
    private Transform gameInitPosition;

    private void Start()
    {
      if (TimelineManager.IsSingletonValid)
      {
        TimelineManager.Instance.PlayableDirector = sceneDirector;
        TimelineManager.Instance.Stop();
        TimelineManager.Instance.PlayAsset(initialCinematic);
      }

      if (GameManager.IsSingletonValid)
        GameManager.Instance.SetPlayer(gameInitPosition.position);
    }
  }
}
