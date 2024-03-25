using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class SampleEntry : MonoBehaviour {

        // Main Context
        bool isLoaded;

        // Sub Context
        SampleUIContext uiCtx;
        SampleLogicContext logicCtx;

        // Main
        public void Start() {
            LLog.Log = Debug.Log;
            LLog.Error = Debug.LogError;
            LLog.Warning = Debug.LogWarning;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            uiCtx = new SampleUIContext(mainCanvas);
            logicCtx = new SampleLogicContext();

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
                SampleLogicDomain.Logic_EnterGame(logicCtx);
            };
            action.Invoke();
        }

        void Binding() {
            var evt = uiCtx.Evt;
            evt.Timer_OnResetClickHandle = () => {
                SampleLogicDomain.OnResetTimer(logicCtx);
            };
            evt.Timer_OnCloseClickHandle = () => {
                SampleLogicDomain.OnCloseTimer(logicCtx);
            };
        }

        public void Update() {
            if (!isLoaded) {
                return;
            }
            var dt = Time.deltaTime;
            SampleLogicDomain.Logic_Tick(logicCtx, dt);
        }

        void LateUpdate() {
            if (!isLoaded) {
                return;
            }
            SampleUIDomain.TimerPanel_TryRefresh(uiCtx, logicCtx.Timer);
        }

    }

}