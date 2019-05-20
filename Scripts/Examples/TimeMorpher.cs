using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuskModules.Singletons;

// Is not within the Singleton namespace, as it is something that uses Singletons, and is not part of it.
namespace DuskModules {

  /// <summary> Morpher script for moving the TimeScale about smoothly. </summary>
  public class TimeMorpher : Singleton<TimeMorpher> {

    /// <summary> Value directly applied to the time scale </summary>
    protected float timeScale = 1;
    /// <summary> The desired time scale </summary>
    protected float desiredTimeScale = 1;
    /// <summary> Speed to use to morph time with </summary>
    protected float timeMorphSpeed = 2;
    
    /// <summary> Sets up the singleton before it is used </summary>
    protected override void Setup() {
			timeScale = Time.timeScale;
			desiredTimeScale = Time.timeScale;
    }

    /// <summary> Morphs the target time scale smoothly over time </summary>
    /// <param name="target"> To what time scale it should go </param>
    public static void MorphTimeScale(float target) {
      instance.StartMorph(target, instance.timeMorphSpeed);
    }
    /// <summary> Morphs the target time scale smoothly over time </summary>
    /// <param name="target"> To what time scale it should go </param>
    /// <param name="speed"> With what speed to morph. If 0, it is instant </param>
    public static void MorphTimeScale(float target, float speed) {
      instance.StartMorph(target, speed);
    }

    /// <summary> Check instant morph </summary>
    /// <param name="target"> To what time scale it should go </param>
    /// <param name="speed"> With what speed to morph. If 0, it is instant </param>
    protected void StartMorph(float target, float speed) {
			desiredTimeScale = target;
			timeMorphSpeed = speed;

      if (timeMorphSpeed == 0) {
				timeScale = desiredTimeScale;
        Time.timeScale = timeScale;
      }
    }

    // Morphs the time scale
    private void Update() {
      // Morph
      if (timeScale != desiredTimeScale) {
				timeScale = Mathf.MoveTowards(timeScale, desiredTimeScale, timeMorphSpeed * Time.unscaledDeltaTime);
      }

      // Apply to Unity
      if (Time.timeScale != timeScale) {
        Time.timeScale = timeScale;
      }
    }
  }
}
