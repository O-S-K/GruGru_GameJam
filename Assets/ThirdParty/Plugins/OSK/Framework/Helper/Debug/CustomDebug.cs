using System;
using UnityEngine;

namespace OSK
{
    public static class CustomDebug
    {
#if UNITY_EDITOR
        static bool isLogMessage = true;
#else
        static bool isLogMessage = false;
#endif

        #region LOG
        public static void Log(object message, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.Log($"<b><color=green>_Log</color></b>: " + message);
        }

        public static void LogWithColor(object message, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.Log($"<b><color=red>_LogWithColor</color></b>: " + message);
        }

        public static void Log(object tile, object message, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.Log($"<b><color=green>_Log</color></b>:_{tile}: " + message);
        }

        public static void LogWarring(object message, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.LogWarning("<b><color=yellow>_LogWarring</color></b>: " + message);
        }

        public static void LogError(object message, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.LogError("<b><color=red>_LogErro</color></b>: " + message);
        }

        public static void LogException(Exception _exception, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.LogException(_exception);
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color, bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.DrawLine(start, end, color);
        }


        public static void PauseEditor(bool ableLog = true)
        {
            if (ableLog && isLogMessage)
                Debug.Log("<b><color=red>Pause Editor</color></b>"); Debug.Break();
        }
        #endregion

        #region Assert
        public static void Assert(bool condition, bool ableLog = true)
        {
            if (ableLog && isLogMessage && condition)
                Debug.Log($"<b><color=red>_Log</color></b>: " + "Value Not Set || Not Reference");
        }

        public static void Assert(bool condition, string message, bool ableLog = true)
        {
            if (ableLog && isLogMessage && condition)
                Debug.Log($"<b><color=red>_Log</color></b>: " + message);
        }

        public static void Assert(bool condition, string format, bool ableLog = true, params object[] args)
        {
            if (ableLog && isLogMessage && condition)
                Debug.Log($"<b><color=red>_Log</color></b>: " + string.Format(format, args));
        }
        #endregion

    }
}
