using System;
using System.Threading.Tasks;
using MortiseFrame.Loom;
using UnityEngine;

namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceUIContext {

        UICore uiCore;
        Sample_WorldSpaceUIEventCenter evt;
        public Sample_WorldSpaceUIEventCenter Evt => evt;

        public Sample_WorldSpaceUIContext(Canvas mainCanvas, Transform worldSpaceFakeCanvas) {
            uiCore = new UICore(mainCanvas, worldSpaceFakeCanvas);
            evt = new Sample_WorldSpaceUIEventCenter();
        }

        // Load
        public async Task LoadAssets() {
            try {
                await uiCore.LoadAssets();
            } catch (Exception e) {
                LLog.Log(e.ToString());
            }
        }

        // Tick
        public void LateTick(float dt) {
            uiCore.LateTick(dt);
        }

        #region Unique Panel
        public T UniquePanel_Open<T>() where T : IPanel {
            LLog.Log("UniquePanel_Open");
            return uiCore.UniquePanel_Open<T>();
        }

        public T UniquePanel_Get<T>() where T : IPanel {
            return uiCore.UniquePanel_Get<T>();
        }

        public bool UniquePanel_TryGet<T>(out T panel) where T : IPanel {
            return uiCore.UniquePanel_TryGet<T>(out panel);
        }

        public void UniquePanel_Close<T>() where T : IPanel {
            uiCore.UniquePanel_Close<T>();
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>() where T : IPanel {
            return uiCore.MultiplePanel_Open<T>();
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : IPanel {
            uiCore.MultiplePanel_Close<T>(panelInstance);
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IPanel {
            uiCore.MultiplePanel_GroupForEach<T>(action);
        }

        public void MultiplePanel_CloseGroup<T>() where T : IPanel {
            uiCore.MultiplePanel_CloseGroup<T>();
        }
        #endregion

    }

}