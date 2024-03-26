using System;
using System.Collections.Generic;
using UnityEngine;

namespace MortiseFrame.Loom {

    public class UIContext {

        // Canvas
        Canvas overlayCanvas;
        public Canvas OverlayCanvas => overlayCanvas;

        Transform worldSpaceFakeCanvas;
        public Transform WorldSpaceFakeCanvas => worldSpaceFakeCanvas;

        Camera worldSpaceCamera;
        public Camera WorldSpaceCamera => worldSpaceCamera;

        // Repo
        public Dictionary<string, IPanel> openedUniqueDict;
        public Dictionary<string/*name*/, Dictionary<int/*id*/, IPanel/*panel*/>> openedMultiDict;

        // Temp
        List<IPanel> tempList;
        List<int> intTempList;

        // ID Dict
        public Dictionary<IPanel, int> idDict;
        public int idRecord;

        // GO Dict
        public Dictionary<IPanel, GameObject> goDict;

        // Prefab
        public Dictionary<string, GameObject> prefabDict;

        // Const
        public string AssetsLabel;

        public UIContext() {
            openedUniqueDict = new Dictionary<string, IPanel>();
            openedMultiDict = new Dictionary<string, Dictionary<int, IPanel>>();
            idDict = new Dictionary<IPanel, int>();
            tempList = new List<IPanel>();
            intTempList = new List<int>();
            goDict = new Dictionary<IPanel, GameObject>();
            prefabDict = new Dictionary<string, GameObject>();
            idRecord = 0;
        }

        public int PickID() {
            return ++idRecord;
        }

        public int GetID(IPanel panel) {
            return idDict[panel];
        }

        public void Asset_AddPrefab(string name, GameObject prefab) {
            prefabDict.Add(name, prefab);
        }

        public void SetOverlayCanvas(Canvas mainCanvas) {
            this.overlayCanvas = mainCanvas;
        }

        public void SetWorldSpaceFakeCanvas(Transform worldSpaceFakeCanvas) {
            this.worldSpaceFakeCanvas = worldSpaceFakeCanvas;
        }

        public void SetWorldSpaceCamera(Camera worldSpaceCamera) {
            this.worldSpaceCamera = worldSpaceCamera;
        }

        #region Unique Panel
        public void UniquePanel_Add(string name, IPanel panel) {
            if (openedUniqueDict.ContainsKey(name)) {
                LLog.Error($"Unique Panel Add Error: Already have Type = {name}; name = {name}");
                return;
            }

            if (!(panel is MonoBehaviour go)) {
                LLog.Error($"Unique Panel Add Error: Panel is not MonoBehaviour; name = {name}");
                return;
            }
            if (panel is IPanel _panel) {
                goDict.Add(_panel, go.gameObject);
            } else {
                LLog.Error($"Unique Panel Add Error: Panel is not IPanel; name = {name}");
            }
            openedUniqueDict.Add(name, panel);
        }

        public void UniquePanel_Remove(string name, Action<GameObject> onDestroy) {
            if (!openedUniqueDict.TryGetValue(name, out var panel)) {
                LLog.Error($"Unique Panel Remove Error: Not found Type = {name}; name = {name}");
                return;
            }
            openedUniqueDict.Remove(name);
            var go = GetGameObject(panel);
            onDestroy(go);
            goDict.Remove(panel);
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
        public void MultiplePanel_Add(string name, int id, IPanel panel) {
            if (!(panel is MonoBehaviour go)) {
                LLog.Error($"Multiple Panel Add Error: Panel is not MonoBehaviour; name = {name}");
                return;
            }

            if (panel is IPanel _panel) {
                goDict.Add(_panel, go.gameObject);
            } else {
                LLog.Error($"Multiple Panel Add Error: Panel is not IPanel; name = {name}");
            }

            if (idDict.ContainsKey(panel)) {
                LLog.Error($"Multiple Panel Add Error: Already have Panel = {panel}; id = {id}; name = {name}");
            }

            idDict.Add(panel, id);

            if (!openedMultiDict.ContainsKey(name)) {
                openedMultiDict[name] = new Dictionary<int, IPanel>();
            }
            openedMultiDict[name].Add(id, panel);
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
                LLog.Error($"Multiple Panel Get Group Error: Don't have Type = {name}; name = {name}");
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

        public void MultiplePanel_Remove<T>(int id, Action<GameObject> onDestroy) where T : IPanel {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                var panel = panels[id];
                idDict.Remove(panel);
                panels.Remove(id);
                if (panels.Count == 0) {
                    openedMultiDict.Remove(name);
                }
                var go = GetGameObject(panel);
                onDestroy(go);
                goDict.Remove(panel);
            }
        }

        public void MultiplePanel_RemoveGroup<T>(Action<GameObject> onDestroy) where T : IPanel {
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
                var go = GetGameObject(panel);
                onDestroy(go);
                goDict.Remove(panel);
            }
            foreach (var id in intTempList) {
                panels.Remove(id);
            }
        }

        public int MultiplePanel_GetID<T>(T panel) where T : IPanel {
            if (idDict.TryGetValue(panel, out var id)) {
                return id;
            }
            return -1;
        }
        #endregion

        public GameObject GetGameObject(IPanel panel) {
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