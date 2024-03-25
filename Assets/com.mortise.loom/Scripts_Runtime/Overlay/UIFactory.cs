using UnityEngine;

namespace MortiseFrame.Loom {

    public static class UIFactory {

        #region Unique Panel
        public static T UniquePanel_Open<T>(UIContext ctx) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.OverlayCanvas.transform).GetComponent<T>();
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        public static bool UniquePanel_TryClose<T>(UIContext ctx) where T : IPanel {
            string name = typeof(T).Name;
            bool has = ctx.UniquePanel_TryGet(name, out var panel);
            if (!has) {
                return false;
            }
            ctx.UniquePanel_Remove(name);
            GameObject.Destroy(panel.GO);
            return true;
        }

        static GameObject GetPrefab(UIContext ctx, string name) {
            bool has = ctx.prefabDict.TryGetValue(name, out var prefab);
            if (!has) {
                LLog.Error($"UIFactory.GetPrefab<{name}>: UI Prefab not found");
                return null;
            }
            return prefab;
        }
        #endregion

        #region Multiple Panel
        public static T MultiplePanel_Open<T>(UIContext ctx) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.OverlayCanvas.transform).GetComponent<T>();
            var id = ctx.PickID();
            panel.SetID(id);
            ctx.MultiplePanel_Add(name, id, panel);
            return panel;
        }

        public static bool MultiplePanel_TryClose<T>(UIContext ctx, T panelInstance) where T : IPanel {
            var has = (ctx.idDict.TryGetValue(panelInstance, out var id));
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
            GameObject.Destroy(panelInstance.GO);
            return true;
        }

        public static void MultiplePanel_CloseGroup<T>(UIContext ctx) where T : IPanel {
            var group = ctx.MultiplePanel_GetGroup<T>();
            LLog.Log("Close Multi Group");
            ctx.MultiplePanel_RemoveGroup<T>();

            foreach (var panel in group) {
                LLog.Log("Destroy Panel: " + panel.GO.name);
                GameObject.Destroy(panel.GO);
            }
        }
        #endregion

    }

}