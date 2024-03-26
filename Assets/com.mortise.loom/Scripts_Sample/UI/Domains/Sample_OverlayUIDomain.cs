namespace MortiseFrame.Loom.Sample {

    public static class Sample_OverlayUIDomain {

        // Timer Unique
        public static void TimerPanel_OpenUnique(Sample_UIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_OverlayUniqueTimerPanel>(false);
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickUniqueHandle;
        }

        public static void TimerPanel_TryRefreshUnique(Sample_UIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Sample_OverlayUniqueTimerPanel>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_CloseUnique(Sample_UIContext ctx) {
            ctx.UniquePanel_Close<Sample_OverlayUniqueTimerPanel>();
        }

        // Timer Multiple
        public static void TimerPanel_OpenMulti(Sample_UIContext ctx) {
            var panel = ctx.MultiplePanel_Open<Sample_OverlayMultipleTimerPanel>(false);
            if (panel == null) {
                return;
            }
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickMultiHandle;
            Timer_RefreshMultiGroupPos(ctx);
        }

        static void Timer_RefreshMultiGroupPos(Sample_UIContext ctx) {
            int index = 0;
            ctx.MultiplePanel_GroupForEach<Sample_OverlayMultipleTimerPanel>((Sample_OverlayMultipleTimerPanel panel) => {
                TimerPanel_OverlayRefreshMultiPos(panel, index);
                index += 1;
            });
        }

        static void TimerPanel_OverlayRefreshMultiPos(Sample_OverlayMultipleTimerPanel panel, int index) {
            var oldPos = panel.transform.localPosition;
            var newPos = new UnityEngine.Vector2(oldPos.x, -60 * index + 60);
            panel.SetPos(newPos);
        }

        public static void TimerPanel_CloseMulti(Sample_UIContext ctx, Sample_OverlayMultipleTimerPanel panel) {
            ctx.MultiplePanel_Close(panel);
            Timer_RefreshMultiGroupPos(ctx);
        }

        public static void TimerPanel_TickRefresh(Sample_UIContext ctx, float value) {
            ctx.MultiplePanel_GroupForEach((Sample_OverlayMultipleTimerPanel panel) => {
                panel.RefreshTimer(value);
            });
        }

        public static void TimerPanel_CloseMultiGroup(Sample_UIContext ctx) {
            ctx.MultiplePanel_CloseGroup<Sample_OverlayMultipleTimerPanel>();
        }

        public static void TimerPanel_CloseAll(Sample_UIContext ctx) {
            ctx.UniquePanel_Close<Sample_OverlayUniqueTimerPanel>();
            ctx.MultiplePanel_CloseGroup<Sample_OverlayMultipleTimerPanel>();
        }

    }

}