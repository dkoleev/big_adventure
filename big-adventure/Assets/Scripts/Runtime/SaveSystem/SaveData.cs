using System;
using UnityEngine;

namespace Runtime.SaveSystem {
    [Serializable]
    public class SaveData {
        public string locationId;
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void LoadFromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
