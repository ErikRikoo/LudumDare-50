using UnityEngine;

namespace Utilities
{
    public class OnAnimationTimed : StateMachineBehaviour
    {
        [SerializeField] private float m_TriggeredTime;

        private float m_Time;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_Time = Time.time;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_Time += Time.deltaTime;
            if (m_Time < m_TriggeredTime)
            {
                return;
            }
            
            if (animator.gameObject.TryGetComponent(out IAnimationStateTimeTriggerable listener))
            {
                if (stateInfo.IsName(animator.GetLayerName(layerIndex) + "." + listener.ListenedName))
                {
                    listener.OnTriggerTime(m_Time);
                }
            }
        }
    }
}