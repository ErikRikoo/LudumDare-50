using System;
using UnityEngine;

namespace Environnement
{
    public class TankerTap : MonoBehaviour
    {
        [SerializeField] private TankerTapCollection m_Collection;
        
        [SerializeField] private WaterFillable m_Fillable;
        private bool m_IsBlocked;

        public bool IsBlocked => m_IsBlocked;

        private void Awake()
        {
            StartFilling();
            m_Collection.AddElement(this);
        }

        public void StartFilling()
        {
            m_Fillable.StartFilling();
        }

        public void StopFilling()
        {
            m_Fillable.StopFilling();
            m_IsBlocked = true;
        }
    }
}