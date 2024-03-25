using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MortiseFrame.Loom {

    public class OverlayUICore {

        OverlayUIContext ctx;

        public OverlayUICore(Canvas mainCanvas) {
            ctx = new OverlayUIContext();
            ctx.Inject(mainCanvas);
        }

        // Load
        public async Task LoadAssets() {
            try {
                var list = await Addressables.LoadAssetsAsync<GameObject>("UI", null).Task;
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
            return OverlayUIFactory.UniquePanel_Open<T>(ctx);
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
            OverlayUIFactory.UniquePanel_Close<T>(ctx);
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>() where T : MonoBehaviour {
            return OverlayUIFactory.MultiplePanel_Open<T>(ctx);
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : MonoBehaviour {
            OverlayUIFactory.MultiplePanel_Close<T>(ctx, panelInstance);
        }

        public void MultiplePanel_CloseGroup<T>() where T : MonoBehaviour {
            OverlayUIFactory.MultiplePanel_CloseGroup<T>(ctx);
        }
        #endregion

    }

}