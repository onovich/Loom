namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayLogicContext {

        float timer;
        public float Timer => timer;

        Sample_OverlayUIContext uiContext;
        public Sample_OverlayUIContext UIContext => uiContext;

        public void Inject(Sample_OverlayUIContext uiContext) {
            this.uiContext = uiContext;
        }

        public void IncTimer(float dt) {
            this.timer += dt;
        }

        public void ClearTimer() {
            this.timer = 0;
        }

    }

}