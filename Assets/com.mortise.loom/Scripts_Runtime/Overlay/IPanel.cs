using UnityEngine;

namespace MortiseFrame.Loom {

    public interface IPanel {

        int ID { get; }
        bool IsUnique { get; }
        bool InWorldSpace { get; }
        GameObject GO { get; }

        void SetID(int id);
        void SetInWorldSpace(bool value);

    }

}