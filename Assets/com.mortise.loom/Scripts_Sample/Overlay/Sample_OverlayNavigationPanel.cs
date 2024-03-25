using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_OverlayNavigationPanel : MonoBehaviour, IPanel {

        [SerializeField] Button btn_unique_open;
        [SerializeField] Button btn_multi_open;

        [SerializeField] Button btn_unique_close;
        [SerializeField] Button btn_multi_closeGroup;
        [SerializeField] Button btn_closeAll;

        public Action OnClickOpenUniqueHandle;
        public Action OnCLickOpenMultiHandle;
        public Action OnClickCloseUniqueHandle;
        public Action OnClickCloseMultiGroupHandle;
        public Action OnCLickAllHandle;

        int id;
        int IPanel.ID => id;
        GameObject IPanel.GO => gameObject;

        bool inWorldSpace;
        public bool InWorldSpace => inWorldSpace;

        public void SetInWorldSpace(bool value) {
            inWorldSpace = value;
        }

        public void SetID(int id) {
            this.id = id;
        }

        public void Ctor() {
            btn_unique_open.onClick.AddListener(() => {
                OnClickOpenUniqueHandle?.Invoke();
            });
            btn_multi_open.onClick.AddListener(() => {
                OnCLickOpenMultiHandle?.Invoke();
            });
            btn_unique_close.onClick.AddListener(() => {
                OnClickCloseUniqueHandle?.Invoke();
            });
            btn_multi_closeGroup.onClick.AddListener(() => {
                OnClickCloseMultiGroupHandle?.Invoke();
            });
            btn_closeAll.onClick.AddListener(() => {
                OnCLickAllHandle?.Invoke();
            });
        }

    }

}