using System;
using System.Threading.Tasks;
using MortiseFrame.Loom;
using UnityEngine;

namespace MortiseFrame.Loom.Sample {

    public class OverlayUIContext {

        UICore uiCore;
        OverlayUIEventCenter evt;
        public OverlayUIEventCenter Evt => evt;

        public OverlayUIContext(Canvas mainCanvas) {
            uiCore = new UICore(mainCanvas);
            evt = new OverlayUIEventCenter();
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
        public T UniquePanel_Open<T>() where T : MonoBehaviour {
            return uiCore.UniquePanel_Open<T>();
        }

        public T UniquePanel_Get<T>() where T : MonoBehaviour {
            return uiCore.UniquePanel_Get<T>();
        }

        public bool UniquePanel_TryGet<T>(out T panel) where T : MonoBehaviour {
            return uiCore.UniquePanel_TryGet<T>(out panel);
        }

        public void UniquePanel_Close<T>() where T : MonoBehaviour {
            uiCore.UniquePanel_Close<T>();
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>() where T : MonoBehaviour {
            return uiCore.MultiplePanel_Open<T>();
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : MonoBehaviour {
            uiCore.MultiplePanel_Close<T>(panelInstance);
        }
        #endregion

    }

}