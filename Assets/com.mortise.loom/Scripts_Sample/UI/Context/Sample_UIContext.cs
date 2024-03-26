using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MortiseFrame.Loom;
using UnityEngine;

namespace MortiseFrame.Loom.Sample {

    public class Sample_UIContext {

        UICore uiCore;
        Sample_UIEventCenter evt;
        public Sample_UIEventCenter Evt => evt;

        public IPanel[] multiPanels;

        public Sample_UIContext(Canvas mainCanvas, Transform worldSpaceFakeCanvas = null, Camera worldSpaceCamera = null) {
            uiCore = new UICore("UI", mainCanvas, worldSpaceFakeCanvas, worldSpaceCamera);
            evt = new Sample_UIEventCenter();
            multiPanels = new IPanel[9];
        }

        // Load
        public async Task LoadAssets() {
            try {
                await uiCore.LoadAssets();
            } catch (Exception e) {
                LLog.Log(e.ToString());
            }
        }

        // Tick
        public void LateTick(float dt) {
            uiCore.LateTick(dt);
        }

        #region Unique Panel
        public T UniquePanel_Open<T>(bool isWorldSpace = false) where T : IPanel {
            return uiCore.UniquePanel_Open<T>(isWorldSpace);
        }

        public T UniquePanel_Get<T>() where T : IPanel {
            return uiCore.UniquePanel_Get<T>();
        }

        public bool UniquePanel_TryGet<T>(out T panel) where T : IPanel {
            return uiCore.UniquePanel_TryGet<T>(out panel);
        }

        public void UniquePanel_Close<T>() where T : IPanel {
            uiCore.UniquePanel_Close<T>();
        }
        #endregion

        #region  Multiple Panel
        public T MultiplePanel_Open<T>(bool isWorldSpace) where T : IPanel {

            var index = MultiPanel_PickNextEmptyIndex();
            if (index == -1) {
                return default(T);
            }
            var panel = uiCore.MultiplePanel_Open<T>(isWorldSpace);
            MultiplePanel_RecordIndex(panel, index);
            return panel;
        }

        public void MultiplePanel_Close<T>(T panel) where T : IPanel {
            MultiplePanel_ClearIndexRecord(panel);
            uiCore.MultiplePanel_Close<T>(panel);
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IPanel {
            uiCore.MultiplePanel_GroupForEach<T>(action);
        }

        public void MultiplePanel_CloseGroup<T>() where T : IPanel {
            MultiplePanel_ClearAllIndexRecord();
            uiCore.MultiplePanel_CloseGroup<T>();
        }

        int MultiPanel_PickNextEmptyIndex() {
            for (int i = 0; i < multiPanels.Length; i++) {
                if (multiPanels[i] == null) {
                    return i;
                }
            }
            return -1;
        }

        bool MultiplePanel_HasPanel(int index) {
            return multiPanels[index] != null;
        }

        void MultiplePanel_RecordIndex(IPanel panel, int index) {
            multiPanels[index] = panel;
        }

        void MultiplePanel_ClearAllIndexRecord() {
            for (int i = 0; i < multiPanels.Length; i++) {
                multiPanels[i] = null;
            }
        }

        bool MultiplePanel_HasEmptyIndex() {
            for (int i = 0; i < multiPanels.Length; i++) {
                if (multiPanels[i] == null) {
                    return true;
                }
            }
            return false;
        }

        void MultiplePanel_ClearIndexRecord(IPanel panel) {
            for (int i = 0; i < multiPanels.Length; i++) {
                if (multiPanels[i] == panel) {
                    multiPanels[i] = null;
                    return;
                }
            }
        }

        public int MultiplePanel_GetIndex(IPanel panel) {
            for (int i = 0; i < multiPanels.Length; i++) {
                if (multiPanels[i] == panel) {
                    return i;
                }
            }
            return -1;
        }
        #endregion

    }

}