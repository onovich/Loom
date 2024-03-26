namespace MortiseFrame.Loom.Sample {

    public class Sample_LogicContext {

        float timer;
        public float Timer => timer;

        Sample_UIContext uiContext;
        public Sample_UIContext UIContext => uiContext;

        public void Inject(Sample_UIContext uiContext) {
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