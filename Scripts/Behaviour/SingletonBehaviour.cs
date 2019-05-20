using UnityEngine;
using System.Collections;

namespace DuskModules.Singletons {

  /// <summary> The Singleton Behaviour is a basic MonoBehaviour implementation of ISingleton. </summary>
  /// <typeparam name="T"> The class of what inherits it. </typeparam>
  public class SingletonBehaviour<T> : MonoBehaviour, ISingleton where T : MonoBehaviour, ISingleton {

    /// <summary> The static instance </summary>
    protected static T _instance;
    /// <summary> The static instance of this manager </summary>
    public static T instance {
      get {
        if (_instance == null || _instance.gameObject == null) {
          _instance = SingletonUtility.Get<T>();
        }
        return _instance;
      }
    }

    /// <summary> The awake of the manager. Makes sur </summary>
    protected void StayAlive() {
      if (_instance == null || _instance == this) {
        _instance = this as T;
        if (transform.parent == null)
          DontDestroyOnLoad(gameObject);
      }
      else {
        Destroy(gameObject);
      }
    }
  }
}