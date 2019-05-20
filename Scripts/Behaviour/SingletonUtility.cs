using System.Collections.Generic;
using UnityEngine;
using System;

namespace DuskModules.Singletons {

  /// <summary> Contains static functions for handling the Singleton interface, which only works for Monobehaviour implementations of it. </summary>
  public class SingletonUtility {

    /// <summary> Dictionary containing the instance for each singleton class in use. </summary>
    protected static Dictionary<Type, ISingleton> _singletons;
    /// <summary> Dictionary containing the instance for each singleton class in use. </summary>
    public static Dictionary<Type, ISingleton> singletons {
      get {
        if (_singletons == null) _singletons = new Dictionary<Type, ISingleton>();
        return _singletons;
      }
    }

    /// <summary> Finds and returns the singleton instance of the given class type </summary>
    /// <typeparam name="T"> The class type to find the singleton of </typeparam>
    /// <returns> The instance of the singleton </returns>
    public static T Get<T>() where T : MonoBehaviour, ISingleton {
      return Get<T>(false);
    }

    /// <summary> Finds and returns the singleton instance of the given class type </summary>
    /// <typeparam name="T"> The class type to find the singleton of </typeparam>
    /// <param name="create"> Whether to allow creating an empty object with the required script attached, if none found. </param>
    /// <returns> The instance of the singleton </returns>
    public static T Get<T>(bool create) where T : MonoBehaviour, ISingleton {
      Type t = typeof(T);
      T found = null;

      // Returns the singleton if it's within the dictionary
      if (singletons.ContainsKey(t)) {
        found = (T)singletons[t];
        if (found != null && found.gameObject != null)
          return (T)singletons[t];
        else
          singletons.Remove(t);
      }

      // If not within, try to find it.
      found = GameObject.FindObjectOfType<T>();

      // If not found by usual means, it might be deactivated. Find it manually.
      if (found == null) {
        Transform[] allObjects = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < allObjects.Length; i++) {
          if (allObjects[i].parent == null) {
            found = allObjects[i].GetComponentInChildren<T>(true);
            if (found != null)
              break;
          }
        }
      }

      // If none found and allowed to create, create it.
      if (found == null && create) {
        found = (new GameObject(typeof(T).Name)).AddComponent<T>();
      }

      // Add to dictionary if found
      if (found != null) {
        singletons.Add(t, found);
      }

      return found;
    }

    /// <summary> Keeps the given object alive between scenes, and deletes it if it already exists. </summary>
    /// <typeparam name="T"> The type of the singleton </typeparam>
    /// <param name="keep"> The object to keep </param>
    /// <returns> False if destroyed </returns>
    public static bool KeepAlive<T>(T keep) where T : MonoBehaviour, ISingleton {
      Type t = typeof(T);

      // If the singleton does not yet exist, or it is the same as the one already recorded, continue.
      if (!singletons.ContainsKey(t) || (T)singletons[t] == keep) {
        // If the first of it's type.
        if (keep.transform.parent == null) {
          GameObject.DontDestroyOnLoad(keep.gameObject);
        }
        singletons.Add(t, keep);
        return true;
      }
      else {
        // If it already contains a different instance of the same type, the new one is faulty and must be deleted!
        GameObject.Destroy(keep.gameObject);
        return false;
      }
    }
  }
}