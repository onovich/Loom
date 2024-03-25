namespace MortiseFrame.Loom.Sample {

    public class OverlayLogicContext {

        float timer;
        public float Timer => timer;

        OverlayUIContext uiContext;
        public OverlayUIContext UIContext => uiContext;

        public void Inject(OverlayUIContext uiContext) {
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