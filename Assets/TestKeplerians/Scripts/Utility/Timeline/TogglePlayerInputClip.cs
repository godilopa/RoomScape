using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TestKeplerians
{
  [Serializable]
  public class TogglePlayerInputClip : PlayableAsset, ITimelineClipAsset
  {
    public bool active;

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
      ScriptPlayable<TogglePlayerInputBehaviour> playable = ScriptPlayable<TogglePlayerInputBehaviour>.Create(graph);

      TogglePlayerInputBehaviour playableBehaviour = playable.GetBehaviour();

      playableBehaviour.active = active;

      return playable;
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
      get { return ClipCaps.None; }
    }
  }
}