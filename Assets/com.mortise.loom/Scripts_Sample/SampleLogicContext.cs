namespace MortiseFrame.Loom.Sample {

    public class SampleLogicContext {

        float timer;
        public float Timer => timer;

        SampleUIContext uiContext;
        public SampleUIContext UIContext => uiContext;

        public void Inject(SampleUIContext uiContext) {
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