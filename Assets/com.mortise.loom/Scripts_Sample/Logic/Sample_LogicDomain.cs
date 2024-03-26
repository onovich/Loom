namespace MortiseFrame.Loom.Sample {

    public static class Sample_LogicDomain {

        // Timer
        public static void OnResetTimer(Sample_LogicContext ctx) {
            Sample_LogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnCloseTimerUnique(Sample_LogicContext ctx) {
            Sample_UIDomain.TimerPanel_CloseUnique(ctx.UIContext);
        }

        public static void Logic_EnterGame(Sample_LogicContext ctx) {
            Sample_UIDomain.OverlayNavigationPanel_OpenUnique(ctx.UIContext);
        }

        public static void Logic_ResetTimer(Sample_LogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(Sample_LogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}