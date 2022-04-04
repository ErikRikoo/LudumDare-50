namespace Utilities
{
    public interface IAnimationStateExitListener
    {
        public string ListenedName { get; }
        void OnStateExit();
    }
}