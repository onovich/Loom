namespace MortiseFrame.Loom.Sample {

    public static class Sample_WorldSpaceUIDomain {

        // Timer Unique
        public static void TimerPanel_OpenUnique(Sample_UIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_WorldSpaceUniqueTimerPanel>(true);
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickUniqueHandle;
            panel.SetWorldPos(new UnityEngine.Vector3(0, 5, 0));
        }

        public static void TimerPanel_TryRefreshUnique(Sample_UIContext ctx, float timer) {
            var has = ctx.UniquePanel_TryGet<Sample_WorldSpaceUniqueTimerPanel>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTimer(timer);
        }

        public static void TimerPanel_CloseUnique(Sample_UIContext ctx) {
            ctx.UniquePanel_Close<Sample_WorldSpaceUniqueTimerPanel>();
        }

        // Timer Multiple
        public static void TimerPanel_OpenMulti(Sample_UIContext ctx) {
            var panel = ctx.MultiplePanel_Open<Sample_WorldSpaceMultipleTimerPanel>(true);
            if (panel == null) {
                return;
            }
            panel.Ctor();
            panel.OnClickResetHandle = ctx.Evt.Timer_OnResetClickHandle;
            panel.OnClickCloseHandle = ctx.Evt.Timer_OnCloseClickMultiHandle;
            var index = ctx.MultiplePanel_GetIndex(panel);
            TimerPanel_RefreshMultiPos(panel, index);

        }

        static void TimerPanel_RefreshMultiPos(Sample_WorldSpaceMultipleTimerPanel panel, int index) {
            var x = index % 3 * 5 - 5;
            var y = 1f;
            var z = index / 3 * 3 - 3;
            var newPos = new UnityEngine.Vector3(x, y, z);
            panel.SetWorldPos(newPos);
        }

        public static void TimerPanel_CloseMulti(Sample_UIContext ctx, Sample_WorldSpaceMultipleTimerPanel panel) {
            ctx.MultiplePanel_Close(panel);
        }

        public static void TimerPanel_TickRefresh(Sample_UIContext ctx, float value) {
            ctx.MultiplePanel_GroupForEach((Sample_WorldSpaceMultipleTimerPanel panel) => {
                panel.RefreshTimer(value);
            });
        }

        public static void TimerPanel_CloseMultiGroup(Sample_UIContext ctx) {
            ctx.MultiplePanel_CloseGroup<Sample_WorldSpaceMultipleTimerPanel>();
        }

        public static void TimerPanel_CloseAll(Sample_UIContext ctx) {
            ctx.UniquePanel_Close<Sample_WorldSpaceUniqueTimerPanel>();
            ctx.MultiplePanel_CloseGroup<Sample_WorldSpaceMultipleTimerPanel>();
        }

    }

}