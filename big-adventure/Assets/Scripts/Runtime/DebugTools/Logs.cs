using UnityEngine;

namespace Runtime.DebugTools {
    public static class Logs {
        public static void Log(string value) {
#if DEBUG
            Debug.Log(value);
#endif
        }
        
        public static void LogWarning(string value) {
#if DEBUG
            Debug.LogWarning(value);
#endif
        }
        
        public static void LogError(string value) {
#if DEBUG
            Debug.LogError(value);
#endif
        }
    }
}
