namespace Environnement
{
    public interface IWaterInteractable
    {
        void OnEnterWater(WaterFillable _fillable);

        void OnExitWater(WaterFillable _fillable);
    }
}