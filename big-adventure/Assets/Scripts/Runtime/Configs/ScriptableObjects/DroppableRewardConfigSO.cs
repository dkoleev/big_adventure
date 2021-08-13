using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Configs.ScriptableObjects {
    [CreateAssetMenu(fileName = "DroppableRewardConfig", menuName = "Config/Reward Dropping Rate Config")]
    public class DroppableRewardConfigSO : ScriptableObject {
        [Tooltip("The list of drop goup that can be dropped by this critter when killed")]
        [SerializeField] private List<DropGroup> dropGroups = new List<DropGroup>();

        public List<DropGroup> DropGroups => dropGroups;

        public virtual DropGroup DropSpecialItem() {
            return null;
        }
    }
}