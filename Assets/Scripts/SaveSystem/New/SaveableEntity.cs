using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.New
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string id = string.Empty;
        public string Id => id;
        [ContextMenu("Generate Id")]
        private void GenerateId() => id = Guid.NewGuid().ToString();

        public object CaptureState()
        {
            var states = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                states[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return states;
        }

        public void RestoreState(object state)
        {
            var states = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                if(states.TryGetValue(saveable.GetType().ToString(), out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
    
    
}