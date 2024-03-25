using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MortiseFrame.Loom {

    public class UICore {

        UIContext ctx;

        public UICore(Canvas mainCanvas, Transform worldSpaceFakeCanvas = null, string assetsLabel = "UI") {
            ctx = new UIContext();
            ctx.SetOverlayCanvas(mainCanvas);
            ctx.SetWorldSpaceFakeCanvas(worldSpaceFakeCanvas);
            ctx.AssetsLabel = assetsLabel;
        }

        // Load
        public async Task LoadAssets() {
            try {
                var lable = ctx.AssetsLabel;
                var list = await Addressables.LoadAssetsAsync<GameObject>(lable, null).Task;
                foreach (var prefab in list) {
                    ctx.Asset_AddPrefab(prefab.name, prefab);
                }
            } catch (Exception e) {
                LLog.Log(e.ToString());
            }
        }

        // Tick
        public void LateTick(float dt) {

        }

        #region Unique Panel
        public T UniquePanel_Open<T>() where T : MonoBehaviour {
            var name = typeof(T).Name;
            var has = ctx.UniquePanel_TryGet(name, out var panel);
            if (panel != null) {
                return panel as T;
            }
            return UIFactory.UniquePanel_Open<T>(ctx);
        }

        public T UniquePanel_Get<T>() where T : MonoBehaviour {
            return ctx.UniquePanel_Get<T>();
        }

        public bool UniquePanel_TryGet<T>(out T panel) where T : MonoBehaviour {
            panel = ctx.UniquePanel_Get<T>();
            if (panel == null) {
                return false;
            }
            return true;
        }

        public void UniquePanel_Close<T>() where T : MonoBehaviour {
            UIFactory.UniquePanel_TryClose<T>(ctx);
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>() where T : MonoBehaviour {
            return UIFactory.MultiplePanel_Open<T>(ctx);
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : MonoBehaviour {
            UIFactory.MultiplePanel_TryClose<T>(ctx, panelInstance);
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : MonoBehaviour {
            ctx.MultiplePanel_GroupForEach<T>(action);
        }

        public int MultiplePanel_GroupCount<T>() where T : MonoBehaviour {
            return ctx.MultiplePanel_GroupCount<T>();
        }

        public int MultiplePanel_GetID<T>(T panel) where T : MonoBehaviour {
            return ctx.MultiplePanel_GetID(panel);
        }

        public void MultiplePanel_CloseGroup<T>() where T : MonoBehaviour {
            UIFactory.MultiplePanel_CloseGroup<T>(ctx);
        }
        #endregion

    }

}