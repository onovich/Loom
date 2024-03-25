namespace MortiseFrame.Loom.Sample {

    public static class Sample_OverlayUIDomain {

        // Timer Unique
        public static void TimerPanel_OpenUnique(Sample_OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_UniqueTimerPanel>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickUniqueHandle;
        }

        public static void TimerPanel_TryRefreshUnique(Sample_OverlayUIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Sample_UniqueTimerPanel>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_CloseUnique(Sample_OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Sample_UniqueTimerPanel>();
        }

        // Timer Multiple
        public static void TimerPanel_OpenMulti(Sample_OverlayUIContext ctx) {
            var panel = ctx.MultiplePanel_Open<Sample_MultipleTimerPanel>();
            panel.Ctor();
            panel.SetInWorldSpace(false);
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickMultiHandle;
            var id = panel.ID;
            Timer_RefreshMultiGroupPos(ctx);
        }

        static void Timer_RefreshMultiGroupPos(Sample_OverlayUIContext ctx) {
            int index = 0;
            ctx.MultiplePanel_GroupForEach<Sample_MultipleTimerPanel>((Sample_MultipleTimerPanel panel) => {
                index += 1;
                TimerPanel_RefreshMultiPos(panel, index);
            });
        }

        static void TimerPanel_RefreshMultiPos(Sample_MultipleTimerPanel panel, int index) {
            var oldPos = panel.transform.localPosition;
            var newPos = new UnityEngine.Vector2(oldPos.x, -70 * index + 70);
            panel.SetPos(newPos);
        }

        public static void TimerPanel_CloseMulti(Sample_OverlayUIContext ctx, Sample_MultipleTimerPanel panel) {
            ctx.MultiplePanel_Close(panel);
            Timer_RefreshMultiGroupPos(ctx);
        }

        public static void TimerPanel_TickRefresh(Sample_OverlayUIContext ctx, float value) {
            ctx.MultiplePanel_GroupForEach((Sample_MultipleTimerPanel panel) => {
                panel.RefreshTimer(value);
            });
        }

        public static void TimerPanel_CloseMultiGroup(Sample_OverlayUIContext ctx) {
            ctx.MultiplePanel_CloseGroup<Sample_MultipleTimerPanel>();
        }

        public static void TimerPanel_CloseAll(Sample_OverlayUIContext ctx) {
            ctx.UniquePanel_Close<Sample_UniqueTimerPanel>();
            ctx.MultiplePanel_CloseGroup<Sample_MultipleTimerPanel>();
        }

        // Navigation
        public static void OverlayNavigationPanel_OpenUnique(Sample_OverlayUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_OverlayNavigationPanel>();
            panel.Ctor();
            panel.SetInWorldSpace(false);
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