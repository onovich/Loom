using System;
using UnityEngine;
using MortiseFrame.Loom;

namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceEntry : MonoBehaviour {

        // Main Context
        bool isLoaded;

        // Sub Context
        Sample_WorldSpaceUIContext uiCtx;
        Sample_WorldSpaceLogicContext logicCtx;

        // Main
        public void Start() {
            LLog.Log = Debug.Log;
            LLog.Error = Debug.LogError;
            LLog.Warning = Debug.LogWarning;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            Transform worldSpaceFakeCanvas = GameObject.Find("FakeCanvas").transform;
            uiCtx = new Sample_WorldSpaceUIContext(mainCanvas, worldSpaceFakeCanvas);
            logicCtx = new Sample_WorldSpaceLogicContext();

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
                Sample_WorldSpaceLogicDomain.Logic_EnterGame(logicCtx);
            };
            action.Invoke();
        }

        void Binding() {
            var evt = uiCtx.Evt;

            evt.Timer_OnResetClickHandle = () => {
                Sample_WorldSpaceLogicDomain.OnResetTimer(logicCtx);
            };
            evt.Timer_OnCloseClickUniqueHandle = () => {
                Sample_WorldSpaceLogicDomain.OnCloseTimerUnique(logicCtx);
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
                Sample_WorldSpaceUIDomain.TimerPanel_CloseMulti(uiCtx, (Sample_MultipleTimerPanel)panel);
            };
            evt.OnOpenUniqueClickHandle = () => {
                Sample_WorldSpaceUIDomain.TimerPanel_OpenUnique(uiCtx);
            };
        }

        public void Update() {
            if (!isLoaded) {
                return;
            }
            var dt = Time.deltaTime;
            Sample_WorldSpaceLogicDomain.Logic_Tick(logicCtx, dt);
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