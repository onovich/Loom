using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayUIEventCenter {

        // Timer
        public Action Timer_OnResetClickHandle;
        public void Timer_OnResetClick() {
            Timer_OnResetClickHandle?.Invoke();
        }

        public Action Timer_OnCloseClickHandle;
        public void Timer_OnCloseCick() {
            Timer_OnCloseClickHandle?.Invoke();
        }

        // Overlay Navigation
        public Action Overlay_OnOpenUniqueClickHandle;
        public void Overlay_OnOpenUniqueClick() {
            Overlay_OnOpenUniqueClickHandle?.Invoke();
        }

        public Action Overlay_OnOpenMultiClickHandle;
        public void Overlay_OnOpenMultiClick() {
            Overlay_OnOpenMultiClickHandle?.Invoke();
        }

        public Action Overlay_OnCloseUniqueClickHandle;
        public void Overlay_OnCloseUniqueClick() {
            Overlay_OnCloseUniqueClickHandle?.Invoke();
        }

        public Action Overlay_OnCloseMultiGroupClickHandle;
        public void Overlay_OnCloseMultiGroupClick() {
            Overlay_OnCloseMultiGroupClickHandle?.Invoke();
        }

        public Action Overlay_OnCloseAllClickHandle;
        public void Overlay_OnCloseAllClick() {
            Overlay_OnCloseAllClickHandle?.Invoke();
        }

        public void Clear() {
            Timer_OnResetClickHandle = null;
            Timer_OnCloseClickHandle = null;
            Overlay_OnOpenUniqueClickHandle = null;
            Overlay_OnOpenMultiClickHandle = null;
            Overlay_OnCloseUniqueClickHandle = null;
            Overlay_OnCloseMultiGroupClickHandle = null;
            Overlay_OnCloseAllClickHandle = null;
        }

    }

}