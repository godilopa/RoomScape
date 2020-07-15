
using TMPro;
using UnityEngine;

namespace TestKeplerians
{
  public class PlayerCanvas : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text reactionText;

    [SerializeField]
    private CanvasGroup canvasGroupReaction;

    [SerializeField]
    private float reactionTextFadedTime = 0.5f;

    [SerializeField]
    private float reactionTextDuration = 2f;

    private Coroutine fadeInCoroutine;

    private Coroutine fadeOutCoroutine;

    private bool fixedReactionRunning = false;

    public void ShowObjectName(string name)
    {
      nameText.text = name;
    }

    public void ShowReactionText(string reactionMsg)
    {
      if (fixedReactionRunning == false)
      {
        reactionText.text = reactionMsg;
        StopAllCoroutines();

        if (string.IsNullOrEmpty(reactionMsg))
        {
          fadeOutCoroutine = StartCoroutine(canvasGroupReaction.FadeOut(reactionTextFadedTime));
        }
        else
        {
          fadeInCoroutine = StartCoroutine(canvasGroupReaction.FadeIn(reactionTextFadedTime));
        }
      }
    }

    public void ShowReactionTextDuringFixedTime(string reactionMsg)
    {
      if (string.IsNullOrEmpty(reactionMsg) == false)
        ShowReacionTextDuringTime(reactionMsg, reactionTextDuration);
    }

    public void ShowReacionTextDuringTime(string reactionMsg, float time)
    {
      reactionText.text = reactionMsg;

      fixedReactionRunning = true;

      StopAllCoroutines();
      fadeInCoroutine = StartCoroutine(canvasGroupReaction.FadeIn(reactionTextFadedTime));
      fadeOutCoroutine = StartCoroutine(canvasGroupReaction.FadeOut(reactionTextFadedTime, time, true, FixedReactionStop));
    }

    private void FixedReactionStop()
    {
      fixedReactionRunning = false;
    }
  }
}
