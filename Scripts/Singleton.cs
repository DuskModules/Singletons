using UnityEngine;

namespace DuskModules.Singletons {

  /// <summary> Singleton which can instantiate and create itself, preparing itself before being actually used. </summary>
  /// <typeparam name="I"> Type of singleton </typeparam>
  public abstract class Singleton<I> : MonoBehaviour where I : Singleton<I> {

    /// <summary> Name of Singleton GameObject container </summary>
    private const string singletonObjName = "Singletons";
		
    /// <summary> The instance of this singleton. </summary>
    public static I instance {
      get {
        if (_instance == null || _instance.gameObject == null) _instance = FindObjectOfType<I>();
        if (_instance == null || _instance.gameObject == null) CreateInstance();
        return _instance;
      }
		}
		protected static I _instance;

		/// <summary> Creates an instance </summary>
		private static void CreateInstance() {
      GameObject singletonObj = GameObject.Find(singletonObjName);
      if (singletonObj == null) {
        singletonObj = new GameObject(singletonObjName);
        DontDestroyOnLoad(singletonObj);
      }

      GameObject myObj = new GameObject(typeof(I).Name);
      myObj.transform.parent = singletonObj.transform;
      _instance = myObj.AddComponent<I>();
      _instance.Setup();
    }

    /// <summary> Sets up the singleton before it is used. Call this base AFTER adding components to the singleton, if any. </summary>
    protected virtual void Setup() {
      ISingletonComponent[] subs = gameObject.GetComponentsInChildren<ISingletonComponent>();
      for (int i = 0; i < subs.Length; i++) {
        subs[i].Setup();
      }
    }
  }

  /// <summary> Singleton which instantiates itself, linked to an .asset config file linked before the singleton is used. </summary>
  /// <typeparam name="I"> Type of singleton </typeparam>
  /// <typeparam name="C"> Type of config file </typeparam>
  public abstract class Singleton<I, C> : Singleton<I> where I : Singleton<I, C> where C : SingletonConfig<C> {

    /// <summary> Config .asset file </summary>
    public static C config { get; private set; }

    /// <summary> Sets up the singleton before it is used </summary>
    protected override void Setup() {
      config = SingletonConfig<C>.instance;

      base.Setup();
    }

  }

}