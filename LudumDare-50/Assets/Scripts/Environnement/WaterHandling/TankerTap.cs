using System;
using UnityEngine;

namespace Environnement
{
    public class TankerTap : MonoBehaviour
    {
        [SerializeField] private WaterFillable m_Fillable;

        private void Awake()
        {
            StartFilling();
        }

        public void StartFilling()
        {
            m_Fillable.StartFilling();
        }

        public void StopFilling()
        {
            m_Fillable.StopFilling();
        }
    }
}