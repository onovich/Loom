using System.Collections.Generic;
using UnityEngine;

namespace MortiseFrame.Loom {

    public class OverlayUIContext {

        // Canvas
        Canvas canvas;
        public Canvas Canvas => canvas;

        Transform worldFakeCanvas;

        // Prefab
        public Dictionary<string, GameObject> prefabDict;

        // Repo
        public Dictionary<string, MonoBehaviour> openedUniqueDict;
        public Dictionary<string/*name*/, Dictionary<int/*id*/, MonoBehaviour/*panel*/>> openedMultiDict;

        // Temp
        List<MonoBehaviour> tempList;
        List<int> intTempList;

        // ID Dict
        public Dictionary<MonoBehaviour, int> idDict;
        public int idRecord;

        public OverlayUIContext() {
            prefabDict = new Dictionary<string, GameObject>();
            openedUniqueDict = new Dictionary<string, MonoBehaviour>();
            openedMultiDict = new Dictionary<string, Dictionary<int, MonoBehaviour>>();
            idDict = new Dictionary<MonoBehaviour, int>();
            tempList = new List<MonoBehaviour>();
            intTempList = new List<int>();
            idRecord = 0;
        }

        public void Inject(Canvas mainCanvas) {
            this.canvas = mainCanvas;
        }

        public void Asset_AddPrefab(string name, GameObject prefab) {
            prefabDict.Add(name, prefab);
        }

        #region Unique Panel
        public void UniquePanel_Add(string name, MonoBehaviour com) {
            openedUniqueDict.Add(name, com);
        }

        public void UniquePanel_Remove(string name) {
            openedUniqueDict.Remove(name);
        }

        public bool UniquePanel_TryGet(string name, out MonoBehaviour com) {
            return openedUniqueDict.TryGetValue(name, out com);
        }

        public T UniquePanel_Get<T>() where T : MonoBehaviour {
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
        public void MultiplePanel_Add(string name, MonoBehaviour com) {
            var id = ++idRecord;
            idDict.Add(com, id);

            if (!openedMultiDict.ContainsKey(name)) {
                openedMultiDict[name] = new Dictionary<int, MonoBehaviour>();
            }
            openedMultiDict[name].Add(id, com);
        }

        public T MultiplePanel_Get<T>(int id) where T : MonoBehaviour {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels) && panels.TryGetValue(id, out var panel)) {
                return panel as T;
            }
            return null;
        }

        public List<MonoBehaviour> MultiplePanel_GetGroup<T>() where T : MonoBehaviour {
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

        public void MultiplePanel_Remove<T>(int id) where T : MonoBehaviour {
            string name = typeof(T).Name;
            if (openedMultiDict.TryGetValue(name, out var panels)) {
                idDict.Remove(panels[id]);
                panels.Remove(id);
                if (panels.Count == 0) {
                    openedMultiDict.Remove(name);
                }
            }
        }

        public void MultiplePanel_RemoveGroup<T>() where T : MonoBehaviour {
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