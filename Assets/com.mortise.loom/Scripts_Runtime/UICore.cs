using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MortiseFrame.Loom {

    public class UICore {

        UIContext ctx;

        public UICore() {
            ctx = new UIContext();
        }

        // Load
        public async Task LoadAssets() {
            var list = await Addressables.LoadAssetsAsync<GameObject>("UI", null).Task;
            foreach (var prefab in list) {
                ctx.Asset_AddPrefab(prefab.name, prefab);
            }
        }

        // Tick
        public void LateTick(float dt) {

        }

        #region Unique Panel
        public T UniquePanel_Open<T>() where T : MonoBehaviour {
            return UIFactory.UniquePanel_Open<T>(ctx);
        }

        public void UniquePanel_Close<T>() where T : MonoBehaviour {
            UIFactory.UniquePanel_Close<T>(ctx);
        }

        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>() where T : MonoBehaviour {
            return UIFactory.MultiplePanel_Open<T>(ctx);
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : MonoBehaviour {
            UIFactory.MultiplePanel_Close<T>(ctx, panelInstance);
        }


        #endregion

    }

}