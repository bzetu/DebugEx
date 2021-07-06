using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JCEx
{ 
    public class DebugExInit
    {
        private static bool mIsInit;
        public static void Execute()
        {
            if (mIsInit)
                return;

            OnInit();
            mIsInit = true;
        }


        private static void OnInit()
        {
            DebugEx.AddButton("Print Hello World", () => {
                DebugEx.Log("Hello World!");
            });

            DebugEx.AddButton("Print Hello World2", () => {
                DebugEx.Log("Hello World2!");
            });

            DebugEx.AddButton("Grab1", () => {
                var mGrab = VolumeManager.instance.stack.GetComponent<Grab>();
                DebugEx.ShowTexture("Grab1", mGrab.RT);
            });

            DebugEx.AddButton("Grab2", () => {
                var mGrab = VolumeManager.instance.stack.GetComponent<Grab>();
                DebugEx.ShowTexture("Grab2", mGrab.RT);
            });
        }

    }
}