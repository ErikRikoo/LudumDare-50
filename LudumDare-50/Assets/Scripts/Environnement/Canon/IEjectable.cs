using UnityEngine;

namespace Environnement
{
    public interface IEjectable
    {
        void OnPutIn(Canon _canon,Transform lanceur);

        void OnPuOut(Canon _canon);
    }
}