using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    public class UIDissolvePass : ScriptableRenderPass
    {
        private const string mCommandBufferName = "CommandBuffer_Grab";
        private RenderTargetIdentifier mSourceRT_Id;
        private Grab mGrab;

        public UIDissolvePass(RenderPassEvent @event)
        {
            this.renderPassEvent = @event;
        }

        public void Setup(RenderTargetIdentifier sourceRT, Grab grab)
        {
            mSourceRT_Id = sourceRT;
            mGrab = grab;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(mCommandBufferName);
            Blit(cmd, mSourceRT_Id, mGrab.RT);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }


        public override void FrameCleanup(CommandBuffer cmd)
        {
            
        }

    }
}