using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayEntry : MonoBehaviour {

        // Main Context
        bool isLoaded;

        // Sub Context
        Sample_OverlayUIContext uiCtx;
        Sample_OverlayLogicContext logicCtx;

        // Main
        public void Start() {
            LLog.Log = Debug.Log;
            LLog.Error = Debug.LogError;
            LLog.Warning = Debug.LogWarning;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            uiCtx = new Sample_OverlayUIContext(mainCanvas);
            logicCtx = new Sample_OverlayLogicContext();

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
                Sample_OverlayLogicDomain.Logic_EnterGame(logicCtx);
            };
            action.Invoke();
        }

        void Binding() {
            var evt = uiCtx.Evt;
            evt.Timer_OnResetClickHandle = () => {
                Sample_OverlayLogicDomain.OnResetTimer(logicCtx);
            };
            evt.Timer_OnCloseClickHandle = () => {
                Sample_OverlayLogicDomain.OnCloseTimer(logicCtx);
            };
        }

        public void Update() {
            if (!isLoaded) {
                return;
            }
            var dt = Time.deltaTime;
            Sample_OverlayLogicDomain.Logic_Tick(logicCtx, dt);
        }

        void LateUpdate() {
            if (!isLoaded) {
                return;
            }
            Sample_OverlayUIDomain.TimerPanel_TryRefresh(uiCtx, logicCtx.Timer);
        }

    }

}