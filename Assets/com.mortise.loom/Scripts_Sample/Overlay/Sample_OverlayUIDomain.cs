namespace MortiseFrame.Loom.Sample {

    public static class Sample_OverlayUIDomain {

        // Timer Unique
        public static void TimerPanel_OpenUnique(Sample_OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_OverlayUniqueTimerPanel>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickUniqueHandle;
        }

        public static void TimerPanel_TryRefreshUnique(Sample_OverlayUIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Sample_OverlayUniqueTimerPanel>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_CloseUnique(Sample_OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Sample_OverlayUniqueTimerPanel>();
        }

        // Timer Multiple
        public static void TimerPanel_OpenMulti(Sample_OverlayUIContext ctx) {
            var panel = ctx.MultiplePanel_Open<Sample_OverlayMultipleTimerPanel>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickMultiHandle;
            Timer_RefreshMultiGroupPos(ctx);
        }

        static void Timer_RefreshMultiGroupPos(Sample_OverlayUIContext ctx) {
            int index = 0;
            ctx.MultiplePanel_GroupForEach<Sample_OverlayMultipleTimerPanel>((Sample_OverlayMultipleTimerPanel panel) => {
                index += 1;
                TimerPanel_RefreshMultiPos(panel, index);
            });
        }

        static void TimerPanel_RefreshMultiPos(Sample_OverlayMultipleTimerPanel panel, int index) {
            var oldPos = panel.transform.localPosition;
            var newPos = new UnityEngine.Vector2(oldPos.x, -60 * index + 60);
            panel.SetPos(newPos);
        }

        public static void TimerPanel_CloseMulti(Sample_OverlayUIContext ctx, Sample_OverlayMultipleTimerPanel panel) {
            ctx.MultiplePanel_Close(panel);
            Timer_RefreshMultiGroupPos(ctx);
        }

        public static void TimerPanel_TickRefresh(Sample_OverlayUIContext ctx, float value) {
            ctx.MultiplePanel_GroupForEach((Sample_OverlayMultipleTimerPanel panel) => {
                panel.RefreshTimer(value);
            });
        }

        public static void TimerPanel_CloseMultiGroup(Sample_OverlayUIContext ctx) {
            ctx.MultiplePanel_CloseGroup<Sample_OverlayMultipleTimerPanel>();
        }

        public static void TimerPanel_CloseAll(Sample_OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Sample_OverlayUniqueTimerPanel>();
            ctx.MultiplePanel_CloseGroup<Sample_OverlayMultipleTimerPanel>();
        }

        // Navigation
        public static void OverlayNavigationPanel_OpenUnique(Sample_OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_OverlayNavigationPanel>();
            panel.Ctor();
            panel.OnClickOpenUniqueHandle = ctx.Evt.OnOpenUniqueClickHandle;
            panel.OnCLickOpenMultiHandle = ctx.Evt.OnOpenMultiClickHandle;
            panel.OnClickCloseUniqueHandle = ctx.Evt.OnCloseUniqueClickHandle;
            panel.OnClickCloseMultiGroupHandle = ctx.Evt.OnCloseMultiGroupClickHandle;
            panel.OnCLickAllHandle = ctx.Evt.OnCloseAllClickHandle;
        }

        public static void OverlayNavigationPanel_Close(Sample_OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Sample_OverlayNavigationPanel>();
        }

    }

}