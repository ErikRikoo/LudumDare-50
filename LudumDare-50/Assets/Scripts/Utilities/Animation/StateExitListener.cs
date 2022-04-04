using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class StateExitListener : MonoBehaviour, IAnimationStateExitListener
    {
        [SerializeField] private string m_ActionName;
        [SerializeField] private UnityEvent m_OnActionEnded;


        public string ListenedName => m_ActionName;
        public void OnStateExit()
        {
            m_OnActionEnded?.Invoke();
        }
    }
}