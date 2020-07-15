using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
  private static T instance;

  public static bool IsSingletonValid
  {
    get { return instance != null; }
  }

  public static T Instance
  {
    get
    {
      if (instance == null)
      {
        // Making sure that there's not other instances of the same type in memory.
        instance = FindObjectOfType<T>();

        if (instance == null)
        {
          GameObject obj = new GameObject();
          obj.name = typeof(T).Name;
          instance = obj.AddComponent<T>();
        }
      }

      return instance;
    }
  }

  protected virtual void Awake()
  {
    Debug.LogFormat("Singleton '{0}' awakes.", typeof(T));

    if (instance == null)
    {
      instance = this as T;

      // Making sure that my Singleton instance will persist in memory across every scene.
      DontDestroyOnLoad(this.gameObject);
    }
    else
    {
      Debug.LogErrorFormat("Singleton already exists.", typeof(T));
      Destroy(gameObject);
    }
  }

  protected virtual void OnDestroy()
  {
    Debug.LogFormat("Singleton '{0}' destroy.", typeof(T));

    if (instance == this)
      instance = null;
  }
}