using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuskModules.Singletons;

// Is not within the Singleton namespace, as it is something that uses Singletons, and is not part of it.
namespace DuskModules {

  /// <summary> Can run coroutines for non-Monobehaviour scripts such as ScriptableObjects </summary>
  public class CoroutineRunner : Singleton<CoroutineRunner> {

    /// <summary> Sets up the singleton before it is used </summary>
    protected override void Setup() {
      DontDestroyOnLoad(gameObject);
    }

    /// <summary> Starts a coroutine </summary>
    /// <param name="coroutine"> The coroutine to run </param>
    /// <returns> The running coroutine </returns>
    public static Coroutine RunCoroutine(IEnumerator coroutine) {
      return instance.StartCoroutine(coroutine);
    }

    /// <summary> Stops a coroutine </summary>
    /// <param name="coroutine"> The coroutine to stop </param>
    public static void QuitCoroutine(Coroutine coroutine) {
      instance.StopCoroutine(coroutine);
    }

  }

}