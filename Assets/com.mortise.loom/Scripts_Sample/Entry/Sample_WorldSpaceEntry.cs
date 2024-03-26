using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceEntry : MonoBehaviour {

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
            Transform worldSpaceFakeCanvas = GameObject.Find("FakeCanvas").transform;
            Camera worldSpaceCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            uiCtx = new Sample_UIContext(mainCanvas, worldSpaceFakeCanvas, worldSpaceCamera);
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
                Sample_LogicDomain.OnWorldSpaceCloseTimerUnique(logicCtx);
            };
            evt.OnCloseAllClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_CloseAll(uiCtx);
            };
            evt.OnCloseMultiGroupClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_CloseMultiGroup(uiCtx);
            };
            evt.OnCloseUniqueClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_CloseUnique(uiCtx);
            };
            evt.OnOpenMultiClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_OpenMulti(uiCtx);
            };
            evt.Timer_OnCloseClickMultiHandle = (panel) => {
                Sample_WorldSpaceUIDomain.TimerPanel_CloseMulti(uiCtx, (Sample_WorldSpaceMultipleTimerPanel)panel);
            };
            evt.OnOpenUniqueClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_OpenUnique(uiCtx);
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
            Sample_WorldSpaceUIDomain.TimerPanel_TryRefreshUnique(uiCtx, logicCtx.Timer);
            Sample_WorldSpaceUIDomain.TimerPanel_TickRefresh(uiCtx, logicCtx.Timer);
        }

    }

}