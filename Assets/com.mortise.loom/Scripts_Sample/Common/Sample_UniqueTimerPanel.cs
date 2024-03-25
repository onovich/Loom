using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_UniqueTimerPanel : MonoBehaviour, IPanel {

        [SerializeField] Text txt_timer;
        [SerializeField] Button btn_close;
        [SerializeField] Button btn_reset;

        public Action OnClickCloseHandle;
        public Action OnClickResetHandle;

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
            btn_close.onClick.AddListener(() => {
                OnClickCloseHandle?.Invoke();
            });
            btn_reset.onClick.AddListener(() => {
                OnClickResetHandle?.Invoke();
            });
        }

        public void RefreshTimer(float timer) {
            txt_timer.text = timer.ToString("F4");
        }

    }

}