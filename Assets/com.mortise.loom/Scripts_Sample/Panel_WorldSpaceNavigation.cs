using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Panel_WorldSpaceNavigation : MonoBehaviour {

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

    }

}