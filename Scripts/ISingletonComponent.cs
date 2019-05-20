using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules.Singletons {

  /// <summary> Interface for any component that can be attached to a singleton, and can setup during the singleton's setup. </summary>
  public interface ISingletonComponent {
    void Setup();
  }

}