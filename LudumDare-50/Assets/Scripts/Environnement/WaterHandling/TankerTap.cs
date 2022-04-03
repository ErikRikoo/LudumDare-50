using System;
using UnityEngine;

namespace Environnement
{
    public class TankerTap : MonoBehaviour
    {
        [SerializeField] private TankerTapCollection m_Collection;
        
        [SerializeField] private WaterFillable m_Fillable;
        private bool m_IsBlocked;
        private bool m_IsFilling;

        public bool IsBlocked => m_IsBlocked;
        public bool IsFilling => m_IsFilling;

        private void Awake()
        {
            //StartFilling();
            m_Collection.AddElement(this);
        }

        public void StartFilling()
        {
            m_Fillable.StartFilling();
            m_IsFilling = true;
        }

        public void StopFilling()
        {
            m_Fillable.StopFilling();
            m_IsBlocked = true;
            m_IsFilling = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITankerTapInteractable tankerTapInteract))
            {
                tankerTapInteract.OnApproachTap(this);
            }
        }
    }
}