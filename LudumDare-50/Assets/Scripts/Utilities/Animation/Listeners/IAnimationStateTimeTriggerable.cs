namespace Utilities
{
    public interface IAnimationStateTimeTriggerable : IAnimationStateListener
    {
        void OnTriggerTime(float _time);
    }
}