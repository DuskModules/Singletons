using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DuskModules.Singletons {

  /// <summary> Instantiates all given singleton behaviours when it awakes. Stays alive, and prevents new instances of this script from doing the same. </summary>
  /// <summary> Ensures all singletons are instantiated only a single time during the entire game runtime. </summary>
  public class SingletonSetup : SingletonBehaviour<SingletonSetup> {

    /// <summary> Parents all objects to itself </summary>
    public bool parentToSelf;

    /// <summary> The objects to create </summary>
    public List<GameObject> initializeObjects;

    // Create in awake
    void Awake() {
			StayAlive();

      if (this == instance) {
        for (int i = 0; i < initializeObjects.Count; i++) {
          if (initializeObjects[i] != null) {
            GameObject obj = Instantiate(initializeObjects[i]);
            obj.name = initializeObjects[i].name;
            if (parentToSelf) obj.transform.parent = transform;
          }
        }
      }
    }
  }
}