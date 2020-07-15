
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace TestKeplerians
{
  public class TimelineManager : Singleton<TimelineManager>
  {
    public PlayableDirector PlayableDirector { set => playableDirector = value; }

    private PlayableDirector playableDirector;

    private float speedStep = 0.01f;

    private float timelineDuration;

    private bool timelinePlaying;

    private float playSpeed = 0;

    private const float MaximumSpeed = 4.0f;

    private const float MinimumSpeed = 1.0f;

    public void PlayAsset(PlayableAsset asset)
    {
      if (timelinePlaying == true)
        return;

      playableDirector.Play(asset);
      timelinePlaying = true;
      Debug.LogFormat("Timeline Manager play {0}", asset.name);

      StartCoroutine(WaitForTimelineToFinish());
    }

    public void Stop()
    {
      if (timelinePlaying == false)
        return;

      Debug.LogFormat("Timeline Manager stop {0}", playableDirector.playableAsset.name);
      playableDirector.Stop();

      timelinePlaying = false;
    }

    [ContextMenu("IncreseSpeed")]
    public void IncreseSpeed()
    {
      if (timelinePlaying == false)
        return;

      playableDirector.timeUpdateMode = DirectorUpdateMode.Manual;
      playSpeed = Mathf.Clamp(speedStep + playSpeed, MinimumSpeed, MaximumSpeed);

      playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(playSpeed);

      playableDirector.Stop();

      timelinePlaying = false;
    }

    IEnumerator WaitForTimelineToFinish()
    {
      timelineDuration = (float)playableDirector.duration;

      yield return new WaitForSeconds(timelineDuration);

      timelinePlaying = false;
    }

    override protected void Awake()
    {
      base.Awake();

      playableDirector = GetComponent<PlayableDirector>();
    }
  }
}
