using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_WorldSpaceMultipleTimerPanel : MonoBehaviour, IWorldPanel {

        [SerializeField] Text txt_timer;
        [SerializeField] Button btn_close;
        [SerializeField] Button btn_reset;

        public Action<MonoBehaviour> OnClickCloseHandle;
        public Action OnClickResetHandle;

        public void Ctor() {
            btn_close.onClick.AddListener(() => {
                OnClickCloseHandle?.Invoke(this);
            });
            btn_reset.onClick.AddListener(() => {
                OnClickResetHandle?.Invoke();
            });
        }

        public void SetPos(Vector2 pos) {
            transform.localPosition = pos;
        }

        public void RefreshTimer(float timer) {
            txt_timer.text = timer.ToString("F4");
        }

    }

}