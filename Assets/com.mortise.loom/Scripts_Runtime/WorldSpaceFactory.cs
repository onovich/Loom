using UnityEngine;

namespace MortiseFrame.Loom {

    public static class WorldSpaceFactory {

        #region Unique Panel
        public static T UniquePanel_Open<T>(WorldSpaceContext ctx) where T : IWorldPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.WorldSpaceFakeCanvas).GetComponent<T>();
            if (panel == null) {
                LLog.Error($"UIFactory.UniquePanel_Open<{name}>: Panel is null");
                return default;
            }
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        public static bool UniquePanel_TryClose<T>(WorldSpaceContext ctx) where T : IWorldPanel {
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

        static GameObject GetPrefab(WorldSpaceContext ctx, string name) {
            bool has = ctx.prefabDict.TryGetValue(name, out var prefab);
            if (!has) {
                LLog.Error($"UIFactory.GetPrefab<{name}>: UI Prefab not found");
                return null;
            }
            return prefab;
        }
        #endregion

        #region Multiple Panel
        public static T MultiplePanel_Open<T>(WorldSpaceContext ctx) where T : IWorldPanel {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.WorldSpaceFakeCanvas).GetComponent<T>();
            var id = ctx.PickID();
            ctx.MultiplePanel_Add(name, id, panel);
            return panel;
        }

        public static bool MultiplePanel_TryClose<T>(WorldSpaceContext ctx, T panel) where T : IWorldPanel {
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

        public static void MultiplePanel_CloseGroup<T>(WorldSpaceContext ctx) where T : IWorldPanel {
            var group = ctx.MultiplePanel_GetGroup<T>();
            ctx.MultiplePanel_RemoveGroup<T>((go) => {
                GameObject.Destroy(go);
            });
        }
        #endregion

    }

}