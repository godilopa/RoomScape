
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestKeplerians
{
  [RequireComponent(typeof(Button))]
  public class SceneButtonController : MonoBehaviour
  {
    [SerializeField]
    private string sceneName = "Scene name";

    [SerializeField]
    private bool tweenOnStart = true;

    private Button button;

    private void StartGame()
    {
      GameManager.Instance.GoToScene(sceneName);
    }

    private void ActiveButton()
    {
      button.interactable = true;
    }

    private void Awake()
    {
      button = GetComponent<Button>();

      if (tweenOnStart == true)
      {
        button.interactable = false;
        transform.localScale = Vector3.zero;
      }
    }

    private void OnEnable()
    {
      button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
      button.onClick.RemoveListener(StartGame);
    }

    private void Start()
    {
      if (tweenOnStart == true)
        DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3(1, 1, 1), 1).OnComplete(ActiveButton);
    }
  }
}
