using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    public class GrabFeature : ScriptableRendererFeature
    {
        public RenderPassEvent mEvent = RenderPassEvent.AfterRenderingPostProcessing;

        private UIDissolvePass mPass;

        private Grab mGrab;

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!Application.isPlaying)
                return;

            mGrab = VolumeManager.instance.stack.GetComponent<Grab>();
            if (mGrab == null)
                return;
            if (!mGrab.IsActive())
                return;

            mPass.Setup(renderer.cameraColorTarget, mGrab);

            //添加到渲染列表
            renderer.EnqueuePass(mPass);
        }


        public override void Create()
        {
            mPass = new UIDissolvePass(mEvent);
        }
    }
}