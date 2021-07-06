using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JCEx;

namespace JCEx
{ 
    public class Demo1 : MonoBehaviour
    { 
        void Start()
        {
            DebugExInit.Execute();
            DebugEx.OpenWindow();
        }
    }
}