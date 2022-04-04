using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class FallInWaterListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_Action;
        
        public void OnInFallInWater()
        {
            m_Action?.Invoke();
        }
    }
}