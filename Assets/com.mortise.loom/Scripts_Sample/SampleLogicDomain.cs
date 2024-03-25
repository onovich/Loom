namespace MortiseFrame.Loom.Sample {

    public static class SampleLogicDomain {

        public static void OnResetTimer(SampleLogicContext ctx) {
            SampleLogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnCloseTimer(SampleLogicContext ctx) {
            SampleUIDomain.TimerPanel_Close(ctx.UIContext);
        }

        public static void Logic_EnterGame(SampleLogicContext ctx) {
            SampleUIDomain.TimerPanel_Open(ctx.UIContext);
        }

        public static void Logic_ResetTimer(SampleLogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(SampleLogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}