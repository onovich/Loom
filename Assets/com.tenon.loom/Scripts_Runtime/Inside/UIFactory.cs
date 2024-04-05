using UnityEngine;

namespace TenonKit.Loom {

    internal static class UIFactory {

        #region Unique Panel
        internal static T UniquePanel_Open<T>(UIContext ctx, bool isWorldSpace = false) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var root = isWorldSpace ? ctx.WorldSpaceFakeCanvas : ctx.OverlayCanvas.transform;
            var panel = GameObject.Instantiate(prefab, root).GetComponent<T>();
            if (panel == null) {
                LLog.Error($"UIFactory.UniquePanel_Open<{name}>: Panel is null");
                return default;
            }
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        internal static bool UniquePanel_TryClose<T>(UIContext ctx) where T : IPanel {
            string name = typeof(T).Name;
            bool has = ctx.UniquePanel_TryGet(name, out var panel);
            if (!has) {
                return false;
            }
            ctx.UniquePanel_Remove(name, (go) => {
                GameObject.Destroy(go);
            });
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
        internal static T MultiplePanel_Open<T>(UIContext ctx, bool isWorldSpace) where T : IPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var root = isWorldSpace ? ctx.WorldSpaceFakeCanvas : ctx.OverlayCanvas.transform;
            var panel = GameObject.Instantiate(prefab, root).GetComponent<T>();
            var id = ctx.PickID();
            ctx.MultiplePanel_Add(name, id, panel);
            return panel;
        }

        internal static bool MultiplePanel_TryClose<T>(UIContext ctx, T panel) where T : IPanel {
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
            ctx.MultiplePanel_Remove<T>(id, (go) => {
                GameObject.Destroy(go);
            });
            return true;
        }

        internal static void MultiplePanel_CloseGroup<T>(UIContext ctx) where T : IPanel {
            var group = ctx.MultiplePanel_GetGroup<T>();
            ctx.MultiplePanel_RemoveGroup<T>((go) => {
                GameObject.Destroy(go);
            });
        }
        #endregion

    }

}