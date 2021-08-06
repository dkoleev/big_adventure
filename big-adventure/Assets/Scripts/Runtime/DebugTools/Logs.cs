using UnityEngine;

namespace Runtime.DebugTools {
    public static class Logs {
        public static void Log(string value) {
#if DEBUG
            Debug.Log(value);
#endif
        }
    }
}
