using UnityEngine;

namespace MortiseFrame.Loom {

    public static class OverlayUIFactory {

        #region Unique Panel
        public static T UniquePanel_Open<T>(OverlayUIContext ctx) where T : MonoBehaviour {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.Canvas.transform).GetComponent<T>();
            ctx.UniquePanel_Add(name, panel);
            return panel;
        }

        public static bool UniquePanel_TryClose<T>(OverlayUIContext ctx) where T : MonoBehaviour {
            string name = typeof(T).Name;
            bool has = ctx.UniquePanel_TryGet(name, out var panel);
            if (!has) {
                return false;
            }
            ctx.UniquePanel_Remove(name);
            GameObject.Destroy(panel.gameObject);
            return true;
        }

        static GameObject GetPrefab(OverlayUIContext ctx, string name) {
            bool has = ctx.prefabDict.TryGetValue(name, out var prefab);
            if (!has) {
                LLog.Error($"UIFactory.GetPrefab<{name}>: UI Prefab not found");
                return null;
            }
            return prefab;
        }
        #endregion

        #region Multiple Panel
        public static T MultiplePanel_Open<T>(OverlayUIContext ctx) where T : MonoBehaviour {
            var dict = ctx.prefabDict;
            string name = typeof(T).Name;
            var prefab = GetPrefab(ctx, name);
            var panel = GameObject.Instantiate(prefab, ctx.Canvas.transform).GetComponent<T>();
            ctx.MultiplePanel_Add(name, panel);
            return panel;
        }

        public static bool MultiplePanel_TryClose<T>(OverlayUIContext ctx, T panelInstance) where T : MonoBehaviour {
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
            GameObject.Destroy(panelInstance.gameObject);
            return true;
        }

        public static void MultiplePanel_CloseGroup<T>(OverlayUIContext ctx) where T : MonoBehaviour {
            var group = ctx.MultiplePanel_GetGroup<T>();
            LLog.Log("Close Multi Group");
            ctx.MultiplePanel_RemoveGroup<T>();

            foreach (var panel in group) {
                LLog.Log("Destroy Panel: " + panel.name);
                GameObject.Destroy(panel.gameObject);
            }
        }
        #endregion

    }

}