﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Schwer.Database {
    public abstract class ScriptableObjectDatabase<T> : ScriptableObject where T : ScriptableObject {
        // Generated via ScriptableObjectDatabaseUtility

        public abstract void Initialise(T[] elements);

        protected I[] FilterByID<I>(I[] elements) where I : ScriptableObject, IID {
            var filteredElements = new List<I>();
            var filteredIDs = new List<int>();

            for (int i = 0; i < elements.Length; i++) {
                if (filteredIDs.Contains(elements[i].id)) {
                    var sharedID = filteredElements[filteredIDs.IndexOf(elements[i].id)].name;
                    Debug.LogWarning($"'{elements[i].name}' was excluded from {this.name} because it shares its ID ({elements[i].id}) with '{sharedID}'.");
                }
                else {
                    filteredElements.Add(elements[i]);
                    filteredIDs.Add(elements[i].id);
                }
            }
            return filteredElements.OrderBy(i => i.id).ToArray();
        }

        protected I GetFromID<I>(int id, I[] elements) where I : ScriptableObject, IID {
            I result = null;
            foreach (var element in elements) {
                if (element == null) {
                    Debug.LogWarning($"{this.GetType().Name} contains a null entry. Please regenerate the database and try again");
                    continue;
                }

                if (element.id == id) {
                    result =element;
                }
            }
            if (result == null) Debug.LogWarning($"{typeof(I).Name} with ID '{id}' was not found in the {this.GetType().Name}.");
            return result;
        }
    }

    public interface IID {
        int id { get; }
    }
}
