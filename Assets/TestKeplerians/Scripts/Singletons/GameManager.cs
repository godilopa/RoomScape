
using UnityEngine;

namespace TestKeplerians
{
  public class GameManager : Singleton<GameManager>
  {
    public PlayerController Player { get => player; }

    [Header("Scene to load")]
    [SerializeField, Tooltip("Scene to Start, it cant be 0 (0 is for root)")]
    private int startSceneIndex = -1;

    [SerializeField]
    private PlayerController player;

    private SceneLoader loader;

    public void GoToScene(int sceneIndex)
    {
      loader.GoTo(sceneIndex);
    }

    public void GoToScene(string sceneName)
    {
      //@HACK:Para que los scripts de jugador no den error en el menu que no tiene camara (añadir diccioanrio de eventos segun escena, en el loader?)
      if (sceneName == "Menu_Alvaro_Godinez")
        player.ActivePlayer(false);

      loader.GoTo(sceneName);
    }

    public void SetPlayer(Vector3 position)
    {
      player.SetPlayerPosition(position);
      player.ActivePlayer(true);
    }

    public void SetPlayerInput(bool enable)
    {
      player.SetPlayerInput(enable);
    }

    private void AfterLoad()
    {
      PuzzleManager.Instance.PreparePuzzles();
    }

    override protected void Awake()
    {
      base.Awake();
      loader = new SceneLoader(this);
    }

    private void Start()
    {
      if (startSceneIndex > 0)
        GoToScene(startSceneIndex);
      else
        Debug.LogWarningFormat("StartSceneIndex scene must be greater than 0");
    }

    private void OnEnable()
    {
      if (loader != null)
        loader.OnAfterLoad += AfterLoad;
    }

    private void OnDisable()
    {
      if (loader != null)
        loader.OnAfterLoad -= AfterLoad;
    }
  }
}
