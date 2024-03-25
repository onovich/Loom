using System;
using System.Collections.Generic;
using UnityEngine;

namespace MortiseFrame.Loom {

    public class UIContext {

        // Canvas
        Canvas overlayCanvas;
        public Canvas OverlayCanvas => overlayCanvas;

        // World Fake Canvas
        Transform worldSpaceFakeCanvas;

        // Prefab
        public Dictionary<string, GameObject> prefabDict;

        // Repo
        public Dictionary<string, IPanel> openedUniqueDict;
        public Dictionary<string/*name*/, Dictionary<int/*id*/, IPanel/*panel*/>> openedMultiDict;

        // Temp
        List<IPanel> tempList;
        List<int> intTempList;

        // ID Dict
        public Dictionary<IPanel, int> idDict;
        public int idRecord;

        // Const
        public string AssetsLabel;

        public UIContext() {
            prefabDict = new Dictionary<string, GameObject>();
            openedUniqueDict = new Dictionary<string, IPanel>();
            openedMultiDict = new Dictionary<string, Dictionary<int, IPanel>>();
            idDict = new Dictionary<IPanel, int>();
            tempList = new List<IPanel>();
            intTempList = new List<int>();
            idRecord = 0;
        }

        public int PickID() {
            return ++idRecord;
        }

        public void SetOverlayCanvas(Canvas mainCanvas) {
            this.overlayCanvas = mainCanvas;
        }

        public void SetWorldSpaceFakeCanvas(Transform worldFakeCanvas) {
            this.worldSpaceFakeCanvas = worldFakeCanvas;
        }

        public void Asset_AddPrefab(string name, GameObject prefab) {
            prefabDict.Add(name, prefab);
        }

        #region Unique Panel
        public void UniquePanel_Add(string name, IPanel com) {
            openedUniqueDict.Add(name, com);
        }

        public void UniquePanel_Remove(string name) {
            openedUniqueDict.Remove(name);
        }

        public bool UniquePanel_TryGet(string name, out IPanel com) {
            return openedUniqueDict.TryGetValue(name, out com);
        }

        public T UniquePanel_Get<T>() where T : IPanel {
            string name = typeof(T).Name;
            bool has = openedUniqueDict.TryGetValue(name, out var com);
            if (!has) {
                return default(T);
            }
            var panel = (T)com;
            return panel;
        }
        #endregion

        #region Multiple Panel
        public void MultiplePanel_Add(string name, int id, IPanel com) {
            idDict.Add(com, id);

            if (!openedMultiDict.ContainsKey(name)) {
                openedMultiDict[name] = new Dictionary<int, IPanel>();
            }
            openedMultiDict[name].Add(id, com);
        }

        public T MultiplePanel_Get<T>(int id) where T : IPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels) && panels.TryGetValue(id, out var panel)) {
                return (T)panel;
            }
            return default(T);
        }

        public int MultiplePanel_GroupCount<T>() where T : IPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                return panels.Count;
            }
            return 0;
        }

        public List<IPanel> MultiplePanel_GetGroup<T>() where T : IPanel {
            string name = typeof(T).Name;
            tempList.Clear();
            var has = (openedMultiDict.TryGetValue(name, out var panels));
            if (!has) {
                LLog.Log($"Multiple Panel Get Group Error: Don't have Type = {name}; name = {name}");
            }
            foreach (var kv in panels) {
                var panel = kv.Value;
                tempList.Add(panel);
            }
            return tempList;
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IPanel {
            string name = typeof(T).Name;
            var has = (openedMultiDict.TryGetValue(name, out var panels));
            if (!has) {
                return;
            }
            foreach (var kv in panels) {
                var panel = kv.Value;
                action((T)panel);
            }
        }

        public void MultiplePanel_Remove<T>(int id) where T : IPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                idDict.Remove(panels[id]);
                panels.Remove(id);
                if (panels.Count == 0) {
                    openedMultiDict.Remove(name);
                }
            }
        }

        public void MultiplePanel_RemoveGroup<T>() where T : IPanel {
            string name = typeof(T).Name;
            var has = openedMultiDict.TryGetValue(name, out var panels);
            if (!has) {
                LLog.Log($"Multiple Panel Remove Group Error: name = {name}");
            }
            intTempList.Clear();
            foreach (var kv in panels) {
                var panel = kv.Value;
                idDict.Remove(panel);
                var id = kv.Key;
                intTempList.Add(id);
            }
            foreach (var id in intTempList) {
                panels.Remove(id);
            }
        }
        #endregion

        public void Clear() {
            openedUniqueDict.Clear();
            openedMultiDict.Clear();
            tempList.Clear();
            intTempList.Clear();
            idDict.Clear();
        }

    }

}