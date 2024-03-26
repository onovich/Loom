namespace MortiseFrame.Loom.Sample {

    public static class Sample_NavigationUIDoamin {

        // Navigation
        public static void NavigationPanel_OpenUnique(Sample_UIContext ctx) {
            var panel = ctx.UniquePanel_Open<Sample_NavigationPanel>();
            panel.Ctor();
            panel.OnClickOpenUniqueHandle = ctx.Evt.OnOpenUniqueClickHandle;
            panel.OnCLickOpenMultiHandle = ctx.Evt.OnOpenMultiClickHandle;
            panel.OnClickCloseUniqueHandle = ctx.Evt.OnCloseUniqueClickHandle;
            panel.OnClickCloseMultiGroupHandle = ctx.Evt.OnCloseMultiGroupClickHandle;
            panel.OnCLickAllHandle = ctx.Evt.OnCloseAllClickHandle;
        }

        public static void NavigationPanel_Close(Sample_UIContext ctx) {
            ctx.UniquePanel_Close<Sample_NavigationPanel>();
        }

    }

}