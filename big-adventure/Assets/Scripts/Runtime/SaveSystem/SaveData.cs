using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.SaveSystem {
    [Serializable]
    public class SaveData {
        public string locationId;
        public List<SerializedItemStack> itemStacks = new List<SerializedItemStack>();
        
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