using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
  public SceneLoader(MonoBehaviour mono)
  {
    this.mono = mono;
  }

  public delegate void AfterLoad();
  public event AfterLoad OnAfterLoad;

  private MonoBehaviour mono;

  private int currentSceneBuildIndex = -1;

  private bool isLoading = false;

  public void GoTo(string sceneName)
  {
    if (isLoading == false)
    {
      Debug.LogFormat("Loading '{0}'", sceneName);
      GoTo(SceneUtility.GetBuildIndexByScenePath(sceneName));
    }
    else
      Debug.LogWarning("Scene loading in progress.");
  }

  public void GoTo(int sceneBuildIndex)
  {
    if (isLoading == false)
    {
      if (sceneBuildIndex != currentSceneBuildIndex && sceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        mono.StartCoroutine(LoadAdditive(sceneBuildIndex));
      else
        Debug.LogWarningFormat("Scene {0} is already loaded or not found.", sceneBuildIndex);
    }
    else
      Debug.LogWarning("Scene loading in progress.");
  }

  private IEnumerator LoadAdditive(int sceneBuildIndex)
  {
    isLoading = true;

    if (currentSceneBuildIndex != -1)
      yield return UnloadAsync();

    yield return LoadAsync(sceneBuildIndex);

    if (OnAfterLoad != null)
      OnAfterLoad();

    yield return SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneBuildIndex));

    currentSceneBuildIndex = sceneBuildIndex;

    isLoading = false;

    yield return null;
  }

  private IEnumerator LoadAsync(int sceneBuildIndex)
  {
    AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
    asyncOp.allowSceneActivation = true;

    while (asyncOp.isDone == false)
      yield return new WaitForEndOfFrame();

    Debug.LogFormat("Loaded scene '{0}'", sceneBuildIndex);

    currentSceneBuildIndex = sceneBuildIndex;

    yield return null;
  }

  private IEnumerator UnloadAsync()
  {
    if (currentSceneBuildIndex != -1)
    {
      AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(currentSceneBuildIndex);
      asyncOp.allowSceneActivation = true;

      while (asyncOp.isDone == false)
        yield return new WaitForEndOfFrame();

      Debug.LogFormat("Unloaded scene '{0}'", currentSceneBuildIndex);

      currentSceneBuildIndex = -1;
    }
    else
      Debug.LogWarning("No scene loaded.");

    yield return null;
  }
}
