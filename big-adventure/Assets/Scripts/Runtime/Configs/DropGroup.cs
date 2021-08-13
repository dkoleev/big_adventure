using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Configs {
    [Serializable]
    public class DropGroup {
        [SerializeField] private List<DropItem> drops;
        [SerializeField] private float dropRate;

        public List<DropItem> Drops => drops;
        public float DropRate => dropRate;
    }
}