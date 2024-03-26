namespace MortiseFrame.Loom.Sample {

    public static class Sample_LogicDomain {

        // Timer
        public static void OnResetTimer(Sample_LogicContext ctx) {
            Sample_LogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnOverlapCloseTimerUnique(Sample_LogicContext ctx) {
            Sample_OverlayUIDomain.TimerPanel_CloseUnique(ctx.UIContext);
        }

        public static void OnWorldSpaceCloseTimerUnique(Sample_LogicContext ctx) {
            Sample_WorldSpaceUIDomain.TimerPanel_CloseUnique(ctx.UIContext);
        }

        public static void Logic_EnterGame(Sample_LogicContext ctx) {
            Sample_NavigationUIDoamin.NavigationPanel_OpenUnique(ctx.UIContext);
        }

        public static void Logic_ResetTimer(Sample_LogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(Sample_LogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}