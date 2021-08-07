using UnityEngine;

namespace Runtime.Core {
    public class DescriptionBaseSO : SerializableScriptableObject {
        [TextArea] [SerializeField] 
        private string description;       
    }
}