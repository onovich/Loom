namespace MortiseFrame.Loom.Sample {

    public static class Sample_WorldSpaceUIDomain {

        // Timer Unique
        public static void TimerPanel_OpenUnique(Sample_WorldSpaceUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_WorldSpaceUniqueTimerPanel>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickUniqueHandle;
        }

        public static void TimerPanel_TryRefreshUnique(Sample_WorldSpaceUIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Sample_WorldSpaceUniqueTimerPanel>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_CloseUnique(Sample_WorldSpaceUIContext ctx) {
            ctx.UniquePanel_Close<Sample_WorldSpaceUniqueTimerPanel>();
        }

        // Timer Multiple
        public static void TimerPanel_OpenMulti(Sample_WorldSpaceUIContext ctx) {
            var panel = ctx.MultiplePanel_Open<Sample_WorldSpaceMultipleTimerPanel>();
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickMultiHandle;
            Timer_RefreshMultiGroupPos(ctx);
        }

        static void Timer_RefreshMultiGroupPos(Sample_WorldSpaceUIContext ctx) {
            int index = 0;
            ctx.MultiplePanel_GroupForEach<Sample_WorldSpaceMultipleTimerPanel>((Sample_WorldSpaceMultipleTimerPanel panel) => {
                index += 1;
                TimerPanel_RefreshMultiPos(panel, index);
            });
        }

        static void TimerPanel_RefreshMultiPos(Sample_WorldSpaceMultipleTimerPanel panel, int index) {
            var oldPos = panel.transform.localPosition;
            var newPos = new UnityEngine.Vector2(oldPos.x, -60 * index + 60);
            panel.SetPos(newPos);
        }

        public static void TimerPanel_CloseMulti(Sample_WorldSpaceUIContext ctx, Sample_WorldSpaceMultipleTimerPanel panel) {
            ctx.MultiplePanel_Close(panel);
            Timer_RefreshMultiGroupPos(ctx);
        }

        public static void TimerPanel_TickRefresh(Sample_WorldSpaceUIContext ctx, float value) {
            ctx.MultiplePanel_GroupForEach((Sample_WorldSpaceMultipleTimerPanel panel) => {
                panel.RefreshTimer(value);
            });
        }

        public static void TimerPanel_CloseMultiGroup(Sample_WorldSpaceUIContext ctx) {
            ctx.MultiplePanel_CloseGroup<Sample_WorldSpaceMultipleTimerPanel>();
        }

        public static void TimerPanel_CloseAll(Sample_WorldSpaceUIContext ctx) {
            ctx.UniquePanel_Close<Sample_WorldSpaceUniqueTimerPanel>();
            ctx.MultiplePanel_CloseGroup<Sample_WorldSpaceMultipleTimerPanel>();
        }

        // Navigation
        public static void WorldSpaceNavigationPanel_OpenUnique(Sample_WorldSpaceUIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_WorldSpaceNavigationPanel>();
            panel.Ctor();
            panel.OnClickOpenUniqueHandle = ctx.Evt.OnOpenUniqueClickHandle;
            panel.OnCLickOpenMultiHandle = ctx.Evt.OnOpenMultiClickHandle;
            panel.OnClickCloseUniqueHandle = ctx.Evt.OnCloseUniqueClickHandle;
            panel.OnClickCloseMultiGroupHandle = ctx.Evt.OnCloseMultiGroupClickHandle;
            panel.OnCLickAllHandle = ctx.Evt.OnCloseAllClickHandle;
        }

        public static void WorldSpaceNavigationPanel_Close(Sample_WorldSpaceUIContext ctx) {
            ctx.UniquePanel_Close<Sample_WorldSpaceNavigationPanel>();
        }

    }

}