namespace MortiseFrame.Loom.Sample {

    public static class OverlayUIDomain {

        // Timer
        public static void TimerPanel_Open(OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Panel_Timer>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickHandle;
        }

        public static void TimerPanel_TryRefresh(OverlayUIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Panel_Timer>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_Close(OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Panel_Timer>();
        }

        // Overlay Navigation
        public static void OverlayNavigationPanel_Open(OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<OverlayNavigationPanel>();
            panel.Ctor();
            panel.OnClickOpenUniqueHandle = ctx.Evt.Overlay_OnOpenUniqueClickHandle;
            panel.OnCLickOpenMultiHandle = ctx.Evt.Overlay_OnOpenMultiClickHandle;
            panel.OnClickCloseUniqueHandle = ctx.Evt.Overlay_OnCloseUniqueClickHandle;
            panel.OnClickCloseMultiGroupHandle = ctx.Evt.Overlay_OnCloseMultiGroupClickHandle;
            panel.OnCLickAllHandle = ctx.Evt.Overlay_OnCloseAllClickHandle;
        }

        public static void OverlayNavigationPanel_Close(OverlayUIContext ctx) {
            ctx.UniquePanel_Close<OverlayNavigationPanel>();
        }

    }

}