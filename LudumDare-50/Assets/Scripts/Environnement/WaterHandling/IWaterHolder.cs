namespace Environnement
{
    public interface IWaterHolder
    {
        bool AlreadyEmptying { get; }

        void OnStartEmptying();

        void OnEndEmptying();
    }
}