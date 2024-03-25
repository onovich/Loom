namespace MortiseFrame.Loom.Sample {

    public static class OverlayLogicDomain {

        public static void OnResetTimer(OverlayLogicContext ctx) {
            OverlayLogicDomain.Logic_ResetTimer(ctx);
        }

        public static void OnCloseTimer(OverlayLogicContext ctx) {
            OverlayUIDomain.TimerPanel_Close(ctx.UIContext);
        }

        public static void Logic_EnterGame(OverlayLogicContext ctx) {
            OverlayUIDomain.OverlayNavigationPanel_Open(ctx.UIContext);
        }

        public static void Logic_ResetTimer(OverlayLogicContext ctx) {
            ctx.ClearTimer();
        }

        public static void Logic_Tick(OverlayLogicContext ctx, float dt) {
            ctx.IncTimer(dt);
        }

    }

}