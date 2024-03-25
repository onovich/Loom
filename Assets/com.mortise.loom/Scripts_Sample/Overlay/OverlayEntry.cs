using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class OverlayEntry : MonoBehaviour {

        // Main Context
        bool isLoaded;

        // Sub Context
        OverlayUIContext uiCtx;
        OverlayLogicContext logicCtx;

        // Main
        public void Start() {
            LLog.Log = Debug.Log;
            LLog.Error = Debug.LogError;
            LLog.Warning = Debug.LogWarning;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            uiCtx = new OverlayUIContext(mainCanvas);
            logicCtx = new OverlayLogicContext();

            logicCtx.Inject(uiCtx);

            Action action = async () => {
                try {
                    await uiCtx.LoadAssets();
                } catch (Exception e) {
                    LLog.Log(e.ToString());
                }
                isLoaded = true;
                LLog.Log("Load Finished");
                Binding();
                OverlayLogicDomain.Logic_EnterGame(logicCtx);
            };
            action.Invoke();
        }

        void Binding() {
            var evt = uiCtx.Evt;
            evt.Timer_OnResetClickHandle = () => {
                OverlayLogicDomain.OnResetTimer(logicCtx);
            };
            evt.Timer_OnCloseClickHandle = () => {
                OverlayLogicDomain.OnCloseTimer(logicCtx);
            };
        }

        public void Update() {
            if (!isLoaded) {
                return;
            }
            var dt = Time.deltaTime;
            OverlayLogicDomain.Logic_Tick(logicCtx, dt);
        }

        void LateUpdate() {
            if (!isLoaded) {
                return;
            }
            OverlayUIDomain.TimerPanel_TryRefresh(uiCtx, logicCtx.Timer);
        }

    }

}