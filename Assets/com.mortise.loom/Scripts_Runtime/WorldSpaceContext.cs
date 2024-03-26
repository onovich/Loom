using System;
using System.Collections.Generic;
using UnityEngine;

namespace MortiseFrame.Loom {

    public class WorldSpaceContext {

        // World Fake Canvas
        Transform worldSpaceFakeCanvas;
        public Transform WorldSpaceFakeCanvas => worldSpaceFakeCanvas;

        // Prefab
        public Dictionary<string, GameObject> prefabDict;

        // Repo
        public Dictionary<string, IWorldPanel> openedUniqueDict;
        public Dictionary<string/*name*/, Dictionary<int/*id*/, IWorldPanel/*panel*/>> openedMultiDict;

        // Temp
        List<IWorldPanel> tempList;
        List<int> intTempList;

        // ID Dict
        public Dictionary<IWorldPanel, int> idDict;
        public int idRecord;

        // GO Dict
        public Dictionary<IWorldPanel, GameObject> goDict;

        // Const
        public string AssetsLabel;

        public WorldSpaceContext() {
            prefabDict = new Dictionary<string, GameObject>();
            openedUniqueDict = new Dictionary<string, IWorldPanel>();
            openedMultiDict = new Dictionary<string, Dictionary<int, IWorldPanel>>();
            idDict = new Dictionary<IWorldPanel, int>();
            tempList = new List<IWorldPanel>();
            intTempList = new List<int>();
            goDict = new Dictionary<IWorldPanel, GameObject>();
            idRecord = 0;
        }

        public int PickID() {
            return ++idRecord;
        }

        public void SetWorldSpaceFakeCanvas(Transform worldFakeCanvas) {
            this.worldSpaceFakeCanvas = worldFakeCanvas;
        }

        public void Asset_AddPrefab(string name, GameObject prefab) {
            prefabDict.Add(name, prefab);
        }

        #region Unique Panel
        public void UniquePanel_Add(string name, IWorldPanel panel) {
            if (openedUniqueDict.ContainsKey(name)) {
                LLog.Error($"Unique Panel Add Error: Already have Type = {name}; name = {name}");
                return;
            }

            if (!(panel is MonoBehaviour go)) {
                LLog.Error($"Unique Panel Add Error: Panel is not MonoBehaviour; name = {name}");
                return;
            }
            if (panel is IWorldPanel _panel) {
                goDict.Add(_panel, go.gameObject);
            }
            openedUniqueDict.Add(name, panel);
        }

        public void UniquePanel_Remove(string name) {
            if (!openedUniqueDict.TryGetValue(name, out var panel)) {
                LLog.Error($"Unique Panel Remove Error: Not found Type = {name}; name = {name}");
                return;
            }
            goDict.Remove(panel);
            openedUniqueDict.Remove(name);
        }

        public bool UniquePanel_TryGet(string name, out IWorldPanel com) {
            return openedUniqueDict.TryGetValue(name, out com);
        }

        public T UniquePanel_Get<T>() where T : IWorldPanel {
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
        public void MultiplePanel_Add(string name, int id, IWorldPanel panel) {
            if (!(panel is MonoBehaviour go)) {
                LLog.Error($"Multiple Panel Add Error: Panel is not MonoBehaviour; name = {name}");
                return;
            }

            if (panel is IWorldPanel _panel) {
                goDict.Add(_panel, go.gameObject);
            }

            if (idDict.ContainsKey(panel)) {
                LLog.Error($"Multiple Panel Add Error: Already have Panel = {panel}; id = {id}; name = {name}");
            }

            idDict.Add(panel, id);

            if (!openedMultiDict.ContainsKey(name)) {
                openedMultiDict[name] = new Dictionary<int, IWorldPanel>();
            }
            openedMultiDict[name].Add(id, panel);
        }

        public T MultiplePanel_Get<T>(int id) where T : IWorldPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels) && panels.TryGetValue(id, out var panel)) {
                return (T)panel;
            }
            return default(T);
        }

        public int MultiplePanel_GroupCount<T>() where T : IWorldPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                return panels.Count;
            }
            return 0;
        }

        public List<IWorldPanel> MultiplePanel_GetGroup<T>() where T : IWorldPanel {
            string name = typeof(T).Name;
            tempList.Clear();
            var has = (openedMultiDict.TryGetValue(name, out var panels));
            if (!has) {
                LLog.Error($"Multiple Panel Get Group Error: Don't have Type = {name}; name = {name}");
            }
            foreach (var kv in panels) {
                var panel = kv.Value;
                tempList.Add(panel);
            }
            return tempList;
        }

        public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IWorldPanel {
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

        public void MultiplePanel_Remove<T>(int id) where T : IWorldPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                idDict.Remove(panels[id]);
                panels.Remove(id);
                if (panels.Count == 0) {
                    openedMultiDict.Remove(name);
                }
                goDict.Remove(panels[id]);
            }
        }

        public void MultiplePanel_RemoveGroup<T>() where T : IWorldPanel {
            string name = typeof(T).Name;
            var has = openedMultiDict.TryGetValue(name, out var panels);
            if (!has) {
                LLog.Error($"Multiple Panel Remove Group Error: name = {name}");
            }
            intTempList.Clear();
            foreach (var kv in panels) {
                var panel = kv.Value;
                idDict.Remove(panel);
                var id = kv.Key;
                intTempList.Add(id);
                goDict.Remove(panel);
            }
            foreach (var id in intTempList) {
                panels.Remove(id);
            }
        }
        #endregion

        public GameObject GetGameObject(IWorldPanel panel) {
            if (goDict.TryGetValue(panel, out var go)) {
                return go;
            }
            return null;
        }

        public void Clear() {
            openedUniqueDict.Clear();
            openedMultiDict.Clear();
            tempList.Clear();
            intTempList.Clear();
            idDict.Clear();
        }

    }

}