using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayEntry : MonoBehaviour {

        // Main Context
        bool isLoaded;

        // Sub Context
        Sample_UIContext uiCtx;
        Sample_LogicContext logicCtx;

        // Main
        public void Start() {
            LLog.Log = Debug.Log;
            LLog.Error = Debug.LogError;
            LLog.Warning = Debug.LogWarning;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            uiCtx = new Sample_UIContext(mainCanvas);
            logicCtx = new Sample_LogicContext();

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
                Sample_LogicDomain.Logic_EnterGame(logicCtx);
            };
            action.Invoke();
        }

        void Binding() {
            var evt = uiCtx.Evt;

            evt.Timer_OnResetClickHandle = () => {
                Sample_LogicDomain.OnResetTimer(logicCtx);
            };
            evt.Timer_OnCloseClickUniqueHandle = () => {
                Sample_LogicDomain.OnCloseTimerUnique(logicCtx);
            };
            evt.OnCloseAllClickHandle = () => {
                Sample_UIDomain.TimerPanel_CloseAll(uiCtx);
            };
            evt.OnCloseMultiGroupClickHandle = () => {
                Sample_UIDomain.TimerPanel_CloseMultiGroup(uiCtx);
            };
            evt.OnCloseUniqueClickHandle = () => {
                Sample_UIDomain.TimerPanel_CloseUnique(uiCtx);
            };
            evt.OnOpenMultiClickHandle = () => {
                Sample_UIDomain.TimerPanel_OpenMulti(uiCtx);
            };
            evt.Timer_OnCloseClickMultiHandle = (panel) => {
                Sample_UIDomain.TimerPanel_CloseMulti(uiCtx, (Sample_MultipleTimerPanel)panel);
            };
            evt.OnOpenUniqueClickHandle = () => {
                Sample_UIDomain.TimerPanel_OpenUnique(uiCtx);
            };
        }

        public void Update() {
            if (!isLoaded) {
                return;
            }
            var dt = Time.deltaTime;
            Sample_LogicDomain.Logic_Tick(logicCtx, dt);
        }

        void LateUpdate() {
            if (!isLoaded) {
                return;
            }
            Sample_UIDomain.TimerPanel_TryRefreshUnique(uiCtx, logicCtx.Timer);
            Sample_UIDomain.TimerPanel_TickRefresh(uiCtx, logicCtx.Timer);
        }

    }

}