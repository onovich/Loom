namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceLogicContext {

        float timer;
        public float Timer => timer;

        Sample_WorldSpaceUIContext uiContext;
        public Sample_WorldSpaceUIContext UIContext => uiContext;

        public void Inject(Sample_WorldSpaceUIContext uiContext) {
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