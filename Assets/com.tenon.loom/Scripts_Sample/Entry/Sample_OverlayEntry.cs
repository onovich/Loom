using System;
using UnityEngine;
using TenonKit.Loom;

namespace TenonKit.Loom.Sample {

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
            LLog.Assert = (condition, message) => Debug.Assert(condition, message);

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
                Sample_LogicDomain.OnOverlapCloseTimerUnique(logicCtx);
            };
            evt.OnCloseAllClickHandle = () => {
                Sample_OverlayUIDomain.TimerPanel_CloseAll(uiCtx);
            };
            evt.OnCloseMultiGroupClickHandle = () => {
                Sample_OverlayUIDomain.TimerPanel_CloseMultiGroup(uiCtx);
            };
            evt.OnCloseUniqueClickHandle = () => {
                Sample_OverlayUIDomain.TimerPanel_CloseUnique(uiCtx);
            };
            evt.OnOpenMultiClickHandle = () => {
                Sample_OverlayUIDomain.TimerPanel_OpenMulti(uiCtx);
            };
            evt.Timer_OnCloseClickMultiHandle = (panel) => {
                Sample_OverlayUIDomain.TimerPanel_CloseMulti(uiCtx, (Sample_OverlayMultipleTimerPanel)panel);
            };
            evt.OnOpenUniqueClickHandle = () => {
                Sample_OverlayUIDomain.TimerPanel_OpenUnique(uiCtx);
            };
        }

        void Unbinding() {
            var evt = uiCtx.Evt;
            evt.Clear();
        }

        void OnDestroy() {
            Unbinding();
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
            Sample_OverlayUIDomain.TimerPanel_TryRefreshUnique(uiCtx, logicCtx.Timer);
            Sample_OverlayUIDomain.TimerPanel_TickRefresh(uiCtx, logicCtx.Timer);
        }

    }

}