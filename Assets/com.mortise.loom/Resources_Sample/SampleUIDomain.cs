namespace MortiseFrame.Loom.Sample {

    public static class SampleUIDomain {

        public static void TimerPanel_Open(SampleUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Panel_Timer>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickHandle;
        }

        public static void TimerPanel_TryRefresh(SampleUIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Panel_Timer>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_Close(SampleUIContext ctx) {
            ctx.UniquePanel_Close<Panel_Timer>();
        }

    }

}