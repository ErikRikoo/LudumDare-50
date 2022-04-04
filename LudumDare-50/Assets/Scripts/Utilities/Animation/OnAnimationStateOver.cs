using UnityEngine;

namespace Utilities
{
    public class OnAnimationStateOver : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.gameObject.TryGetComponent(out IAnimationStateExitListener listener))
            {
                if (stateInfo.IsName(animator.GetLayerName(layerIndex) + "." + listener.ListenedName))
                {
                    listener.OnStateExit();
                }
            }
        }
    }
}