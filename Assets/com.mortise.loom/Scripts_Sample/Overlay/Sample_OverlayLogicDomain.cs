namespace MortiseFrame.Loom.Sample {

    public static class Sample_OverlayLogicDomain {

        // Timer
        public static void OnResetTimer(Sample_OverlayLogicContext ctx) {
            Sample_OverlayLogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnCloseTimerUnique(Sample_OverlayLogicContext ctx) {
            Sample_OverlayUIDomain.TimerPanel_CloseUnique(ctx.UIContext);
        }

        public static void Logic_EnterGame(Sample_OverlayLogicContext ctx) {
            Sample_OverlayUIDomain.OverlayNavigationPanel_OpenUnique(ctx.UIContext);
        }

        public static void Logic_ResetTimer(Sample_OverlayLogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(Sample_OverlayLogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}