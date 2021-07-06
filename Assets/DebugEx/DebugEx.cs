using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCEx
{ 
    public class DebugEx
    {
        private static DebugWindow mInstance;
        public static void OpenWindow()
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("JCEx.DebugWindows", typeof(DebugWindow)).GetComponent<DebugWindow>();
                //mInstance.gameObject.hideFlags = HideFlags.HideInHierarchy;
            }
        }


        public static void CloseWindow()
        {
            if(mInstance != null && mInstance.gameObject != null)
                GameObject.Destroy(mInstance.gameObject);
        }


        public static void Log(object message)
        {
            DebugWindow.Log(message);
        }

        public static void LogFormat(string format, params object[] args)
        {
            DebugWindow.LogFormat(format, args);
        }

        public static void AddButton(string btnName, System.Action action)
        {
            DebugWindow.AddButton(btnName, action);
        }

        public static void ShowTexture(string texName, Texture texture)
        {
            DebugWindow.ShowTexture(texName, texture);
        }

        public static bool UnShowTexture(string texName)
        {
            return DebugWindow.UnShowTexture(texName);
        }
    }
}