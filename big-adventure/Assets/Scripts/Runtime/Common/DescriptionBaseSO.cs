using UnityEngine;

namespace Common {
    public class DescriptionBaseSO : ScriptableObject {
        [TextArea] [SerializeField] 
        private string description;       
    }
}