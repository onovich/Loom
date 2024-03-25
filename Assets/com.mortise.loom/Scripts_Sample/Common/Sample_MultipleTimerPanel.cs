using UnityEngine;
using UnityEngine.UI;
using System;

namespace MortiseFrame.Loom.Sample {

    public class Sample_MultipleTimerPanel : MonoBehaviour, IPanel {

        [SerializeField] Text txt_timer;
        [SerializeField] Button btn_close;
        [SerializeField] Button btn_reset;

        public Action<MonoBehaviour> OnClickCloseHandle;
        public Action OnClickResetHandle;

        int id;
        public int ID => id;
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