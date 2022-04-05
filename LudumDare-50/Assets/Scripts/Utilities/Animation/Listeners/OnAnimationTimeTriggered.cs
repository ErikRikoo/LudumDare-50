using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class OnAnimationTimeTriggered : MonoBehaviour, IAnimationStateTimeTriggerable
    {
        [SerializeField] private string m_ActionName;
        [SerializeField] private UnityEvent m_OnTriggered;


        public string ListenedName => m_ActionName;
        public void OnTriggerTime(float _time)
        {
            m_OnTriggered?.Invoke();

        }
    }
}