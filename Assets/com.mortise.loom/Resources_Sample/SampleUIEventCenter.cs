using System;

namespace MortiseFrame.Loom.Sample {

    public class SampleUIEventCenter {

        // Timer
        public Action Timer_OnResetClickHandle;
        public void Timer_OnResetClick() {
            Timer_OnResetClickHandle?.Invoke();
        }

        public Action Timer_OnCloseClickHandle;
        public void Timer_OnCloseCick() {
            Timer_OnCloseClickHandle?.Invoke();
        }

        public void Clear() {
            Timer_OnResetClickHandle = null;
            Timer_OnCloseClickHandle = null;
        }

    }

}