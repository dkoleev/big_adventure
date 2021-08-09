using Runtime.SaveSystem;
using UnityEditor;
using UnityEngine;

namespace Editor.ToolsMenu {
    public static class ProgressEditor {
        [MenuItem("Avocado/Save/Reset")]
        public static void EraseSave() {
            SaveSystem.ClearProgress();
            Debug.LogWarning("Save file deleted");
        }
    }
}