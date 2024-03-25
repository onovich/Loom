using System;
using UnityEngine;

namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayUIEventCenter {

        // Timer
        public Action Timer_OnResetClickHandle;
        public void Timer_OnResetClick() {
            Timer_OnResetClickHandle?.Invoke();
        }

        public Action Timer_OnCloseClickUniqueHandle;
        public void Timer_OnCloseClickUnique() {
            Timer_OnCloseClickUniqueHandle?.Invoke();
        }

        public Action<MonoBehaviour> Timer_OnCloseClickMultiHandle;
        public void Timer_OnCloseClickMulti(MonoBehaviour panel) {
            Timer_OnCloseClickMultiHandle?.Invoke(panel);
        }

        // Navigation
        public Action OnOpenUniqueClickHandle;
        public void OnOpenUniqueClick() {
            OnOpenUniqueClickHandle?.Invoke();
        }

        public Action OnOpenMultiClickHandle;
        public void OnOpenMultiClick() {
            OnOpenMultiClickHandle?.Invoke();
        }

        public Action OnCloseUniqueClickHandle;
        public void OnCloseUniqueClick() {
            OnCloseUniqueClickHandle?.Invoke();
        }

        public Action OnCloseMultiGroupClickHandle;
        public void OnCloseMultiGroupClick() {
            OnCloseMultiGroupClickHandle?.Invoke();
        }

        public Action OnCloseAllClickHandle;
        public void OnCloseAllClick() {
            OnCloseAllClickHandle?.Invoke();
        }

        public void Clear() {
            Timer_OnResetClickHandle = null;
            Timer_OnCloseClickUniqueHandle = null;
            Timer_OnCloseClickMultiHandle = null;
            OnOpenUniqueClickHandle = null;
            OnOpenMultiClickHandle = null;
            OnCloseUniqueClickHandle = null;
            OnCloseMultiGroupClickHandle = null;
            OnCloseAllClickHandle = null;
        }

    }

}