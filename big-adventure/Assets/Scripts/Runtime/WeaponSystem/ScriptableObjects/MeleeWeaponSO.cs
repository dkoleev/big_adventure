using UnityEngine;

namespace Runtime.WeaponSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "Melee", menuName = "Data/Weapons/Melee")]
    public class MeleeWeaponSO : WeaponSO {
        [SerializeField] private int damage;
    }
}