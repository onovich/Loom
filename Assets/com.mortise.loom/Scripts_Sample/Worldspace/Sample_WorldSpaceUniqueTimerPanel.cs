using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceUniqueTimerPanel : MonoBehaviour, IPanel {

        [SerializeField] Text txt_timer;
        [SerializeField] Button btn_close;
        [SerializeField] Button btn_reset;

        public Action OnClickCloseHandle;
        public Action OnClickResetHandle;

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