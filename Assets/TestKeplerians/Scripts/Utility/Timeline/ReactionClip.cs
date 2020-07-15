using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TestKeplerians
{
  [Serializable]
  public class ReactionClip : PlayableAsset, ITimelineClipAsset
  {
    public string reactionMsg;

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
      ScriptPlayable<ReactionBehaviour> playable = ScriptPlayable<ReactionBehaviour>.Create(graph);

      ReactionBehaviour playableBehaviour = playable.GetBehaviour();

      playableBehaviour.reactionMsg = reactionMsg;

      return playable;
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
      get { return ClipCaps.None; }
    }
  }
}