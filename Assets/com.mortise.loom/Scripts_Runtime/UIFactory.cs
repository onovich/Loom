using UnityEngine;

namespace MortiseFrame.Loom {

    public static class UIFactory {

        #region Unique Panel
        public static T UniquePanel_Open<T>(UIContext ctx) where T : MonoBehaviour {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.Canvas.transform).GetComponent<T>();
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        public static void UniquePanel_Close<T>(UIContext ctx) where T : MonoBehaviour {
            string name = typeof(T).Name;
            bool has = ctx.UniquePanel_TryGet(name, out var panel);
            if (!has) {
                LLog.Warning($"UIFactory.Close<{name}>: Panel not found");
                return;
            }
            ctx.UniquePanel_Remove(name);
            GameObject.Destroy(panel.gameObject);
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
        public static T MultiplePanel_Open<T>(UIContext ctx) where T : MonoBehaviour {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.Canvas.transform).GetComponent<T>();
            ctx.MultiplePanel_Add(name, panel);
            return panel;
        }

        public static void MultiplePanel_Close<T>(UIContext ctx, T panelInstance) where T : MonoBehaviour {
            var has = (ctx.idDict.TryGetValue(panelInstance, out var id));
            if (!has) {
                LLog.Warning($"UIFactory.MultiplePanel_Close: Panel or ID not found for {typeof(T).Name}");
            }
            has = ctx.openedMultiDict.TryGetValue(typeof(T).Name, out var panels);
            if (!has) {
                LLog.Warning($"UIFactory.MultiplePanel_Close: Panel or ID not found for {typeof(T).Name}");
            }
            ctx.MultiplePanel_Remove<T>(id);
            GameObject.Destroy(panelInstance.gameObject);
        }

        public static void MultiplePanel_CloseGroup<T>(UIContext ctx) where T : MonoBehaviour {
            var group = ctx.MultiplePanel_GetGroup<T>();
            foreach (var panel in group) {
                GameObject.Destroy(panel);
            }
            ctx.MultiplePanel_RemoveGroup<T>();
        }
        #endregion

    }

}