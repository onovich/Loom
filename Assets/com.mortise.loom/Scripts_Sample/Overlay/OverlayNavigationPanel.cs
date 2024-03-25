using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class OverlayNavigationPanel : MonoBehaviour {

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