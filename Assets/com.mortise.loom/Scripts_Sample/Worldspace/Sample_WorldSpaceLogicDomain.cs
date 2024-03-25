namespace MortiseFrame.Loom.Sample {

    public static class Sample_WorldSpaceLogicDomain {

        // Timer
        public static void OnResetTimer(Sample_WorldSpaceLogicContext ctx) {
            Sample_WorldSpaceLogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnCloseTimerUnique(Sample_WorldSpaceLogicContext ctx) {
            Sample_WorldSpaceUIDomain.TimerPanel_CloseUnique(ctx.UIContext);
        }

        public static void Logic_EnterGame(Sample_WorldSpaceLogicContext ctx) {
            Sample_WorldSpaceUIDomain.WorldSpaceNavigationPanel_OpenUnique(ctx.UIContext);
        }

        public static void Logic_ResetTimer(Sample_WorldSpaceLogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(Sample_WorldSpaceLogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}