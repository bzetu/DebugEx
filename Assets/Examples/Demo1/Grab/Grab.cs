using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityEngine.Rendering.Universal
{
    [Serializable, VolumeComponentMenu("Post-processing/Grab")]
    public class Grab : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("Grab Enable")]
        public BoolParameter enabled = new BoolParameter(false);

        private RenderTexture mRT;

        public RenderTexture RT
        {
            get 
            {
                if(mRT == null)
                    mRT = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
                return mRT;
            }
        }

        public bool IsActive()
        {
            return enabled.value;
        }

        public bool IsTileCompatible() => false;
    }
}