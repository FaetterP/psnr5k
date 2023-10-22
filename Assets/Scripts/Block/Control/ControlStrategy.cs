using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Block.Control
{
    abstract class ControlStrategy : MonoBehaviour
    {
        public UnityEvent<float> e_onAngleChanged = new UnityEvent<float>();

        public void AddListenerOnAngleChanged(UnityAction<float> action)
        {
            e_onAngleChanged.AddListener(action);
        }
        public void RemoveListenerOnAngleChanged(UnityAction<float> action)
        {
            e_onAngleChanged.RemoveListener(action);
        }

        protected void UpdateAngle(float value)
        {
            e_onAngleChanged.Invoke(value);
        }
    }
}
