using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace DuskModules.Singletons {

  /// <summary> Config file for singletons </summary>
  /// <typeparam name="C"> Type of config </typeparam>
  public abstract class SingletonConfig<C> : ScriptableObject where C : SingletonConfig<C> {

    /// <summary> Finds and gets the .asset in the resources of this config type </summary>
    public static C instance {
      get {
        string cName = typeof(C).Name;
        C config = Resources.Load<C>(cName);

        // If config cannot be found, create it.
        if (config == null) {
          config = CreateInstance<C>();

#if UNITY_EDITOR
          // Create asset in default resources folder. It can be moved to a different one if need be.
          string properPath = Path.Combine(Application.dataPath, "Resources");
          if (!Directory.Exists(properPath)) {
            AssetDatabase.CreateFolder("Assets", "Resources");
          }
          string fullPath = Path.Combine(Path.Combine("Assets", "Resources"), cName + ".asset");
          AssetDatabase.CreateAsset(config, fullPath);
#endif
        }
        return config;
      }
    }

    /// <summary> Opens this configuration. </summary>
    protected static void OpenConfigFile() {
#if UNITY_EDITOR
      Selection.activeObject = instance;
#endif
    }
  }

}