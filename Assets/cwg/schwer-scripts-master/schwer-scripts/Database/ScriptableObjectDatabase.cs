using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Schwer.Database {
    public abstract class ScriptableObjectDatabase<T> : ScriptableObject where T : ScriptableObject {
        // Generated via ScriptableObjectDatabaseUtility

        public abstract void Initialise(T[] elements);
    }
}
