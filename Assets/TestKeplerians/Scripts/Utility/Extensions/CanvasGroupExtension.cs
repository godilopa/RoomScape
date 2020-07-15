using System.Collections;
using UnityEngine;

namespace TestKeplerians
{
  public static class CanvasGroupExtension
  {
    public static void Hide(this CanvasGroup self)
    {
      self.interactable = self.blocksRaycasts = false;
      self.alpha = 0.0f;
    }

    public static void Show(this CanvasGroup self)
    {
      self.interactable = self.blocksRaycasts = true;
      self.alpha = 1.0f;
    }

    public static IEnumerator FadeOut(this CanvasGroup self, float time, float wait = 0.0f, bool hide = true, System.Action func = null)
    {
      if (wait > 0.0f)
        yield return new WaitForSeconds(wait);

      self.interactable = false;

      while (self.alpha > 0.0f)
      {
        self.alpha -= Time.deltaTime / time;
        yield return null;
      }

      if (hide == true)
        self.Hide();

      if (func != null)
        func();

      yield return null;
    }

    public static IEnumerator FadeIn(this CanvasGroup self, float time, float wait = 0.0f, bool show = true, System.Action func = null)
    {
      if (wait > 0.0f)
        yield return new WaitForSeconds(wait);

      while (self.alpha < 1.0f)
      {
        self.alpha += Time.deltaTime / time;
        yield return null;
      }

      if (show == true)
        self.Show();

      if (func != null)
        func();

      yield return null;
    }
  }
}
