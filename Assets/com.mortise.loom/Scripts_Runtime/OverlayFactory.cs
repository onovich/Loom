using UnityEngine;

namespace MortiseFrame.Loom {

    public static class OverlayFactory {

        #region Unique Panel
        public static T UniquePanel_Open<T>(OverlayContext ctx) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.OverlayCanvas.transform).GetComponent<T>();
            if (panel == null) {
                LLog.Error($"UIFactory.UniquePanel_Open<{name}>: Panel is null");
                return default;
            }
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        public static bool UniquePanel_TryClose<T>(OverlayContext ctx) where T : IPanel {
            string name = typeof(T).Name;
            bool has = ctx.UniquePanel_TryGet(name, out var panel);
            if (!has) {
                return false;
            }
            ctx.UniquePanel_Remove(name);
            var go = ctx.GetGameObject(panel);
            GameObject.Destroy(go);
            return true;
        }

        static GameObject GetPrefab(OverlayContext ctx, string name) {
            bool has = ctx.prefabDict.TryGetValue(name, out var prefab);
            if (!has) {
                LLog.Error($"UIFactory.GetPrefab<{name}>: UI Prefab not found");
                return null;
            }
            return prefab;
        }
        #endregion

        #region Multiple Panel
        public static T MultiplePanel_Open<T>(OverlayContext ctx) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.OverlayCanvas.transform).GetComponent<T>();
            var id = ctx.PickID();
            ctx.MultiplePanel_Add(name, id, panel);
            return panel;
        }

        public static bool MultiplePanel_TryClose<T>(OverlayContext ctx, T panel) where T : IPanel {
            var has = (ctx.idDict.TryGetValue(panel, out var id));
            if (!has) {
                LLog.Warning("MultiplePanel_TryClose: Panel not found in ID Dict");
                return false;
            }
            has = ctx.openedMultiDict.TryGetValue(typeof(T).Name, out var panels);
            if (!has) {
                LLog.Warning("MultiplePanel_TryClose: Panel not found in Opened Multi Dict");
                return false;
            }
            ctx.MultiplePanel_Remove<T>(id);
            var go = ctx.GetGameObject(panel);
            GameObject.Destroy(go);
            return true;
        }

        public static void MultiplePanel_CloseGroup<T>(OverlayContext ctx) where T : IPanel {
            var group = ctx.MultiplePanel_GetGroup<T>();
            ctx.MultiplePanel_RemoveGroup<T>();

            foreach (var panel in group) {
                var go = ctx.GetGameObject(panel);
                GameObject.Destroy(go);
            }
        }
        #endregion

    }

}