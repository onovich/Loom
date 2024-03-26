using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MortiseFrame.Loom {

    public class UICore {

        UIContext ctx;

        public UICore(string assetsLabel, Canvas mainCanvas, Transform worldSpaceFakeCanvas = null, Camera worldSpaceCamera = null) {
            ctx = new UIContext();
            ctx.AssetsLabel = assetsLabel;
            if (mainCanvas != null) {
                ctx.SetOverlayCanvas(mainCanvas);
            }
            if (worldSpaceFakeCanvas != null) {
                ctx.SetWorldSpaceFakeCanvas(worldSpaceFakeCanvas);
                LLog.Assert(worldSpaceCamera != null, "worldSpaceCamera != null");
                ctx.SetWorldSpaceCamera(worldSpaceCamera);
                LLog.Log("World Space UI Enabled");
            }
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
                LLog.Error(e.ToString());
            }
        }

        // Tick
        public void LateTick(float dt) {

        }

        #region Unique Panel
        public T UniquePanel_Open<T>(bool isWorldSpace = false) where T : IPanel {
            var name = typeof(T).Name;
            var has = ctx.UniquePanel_TryGet(name, out var panel);
            if (panel != null) {
                if (isWorldSpace) {
                    SetWorldSpaceCamera(panel);
                }
                return (T)panel;
            }
            panel = UIFactory.UniquePanel_Open<T>(ctx, isWorldSpace);
            if (isWorldSpace) {
                SetWorldSpaceCamera(panel);
            }
            return (T)panel;
        }

        void SetWorldSpaceCamera<T>(T panel) where T : IPanel {
            if (panel is MonoBehaviour _panel) {
                _panel.GetComponent<Canvas>().worldCamera = ctx.WorldSpaceCamera;
            }
        }

        public T UniquePanel_Get<T>() where T : IPanel {
            return ctx.UniquePanel_Get<T>();
        }

        public bool UniquePanel_TryGet<T>(out T panel) where T : IPanel {
            panel = ctx.UniquePanel_Get<T>();
            if (panel == null) {
                return false;
            }
            return true;
        }

        public void UniquePanel_Close<T>() where T : IPanel {
            var succ = UIFactory.UniquePanel_TryClose<T>(ctx);
            if (!succ) {
                LLog.Warning($"UniquePanel_Close<{typeof(T).Name}>: Panel not found");
            }
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>(bool isWorldSpace = false) where T : IPanel {
            var panel = UIFactory.MultiplePanel_Open<T>(ctx, isWorldSpace);
            if (isWorldSpace) {
                SetWorldSpaceCamera(panel);
            }
            return panel;
        }

        public void MultiplePanel_Close<T>(T panelInstance) where T : IPanel {
            UIFactory.MultiplePanel_TryClose<T>(ctx, panelInstance);
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IPanel {
            ctx.MultiplePanel_GroupForEach<T>(action);
        }

        public int MultiplePanel_GroupCount<T>() where T : IPanel {
            return ctx.MultiplePanel_GroupCount<T>();
        }

        public void MultiplePanel_CloseGroup<T>() where T : IPanel {
            UIFactory.MultiplePanel_CloseGroup<T>(ctx);
        }
        #endregion

        public int GetID(IPanel panel) {
            return ctx.GetID(panel);
        }

    }

}