namespace Environnement
{
    public interface ITankerTapInteractable
    {
        void OnApproachTap(TankerTap _tankerTap);

        void OnAwayTap(TankerTap _tankerTap);
    }
}