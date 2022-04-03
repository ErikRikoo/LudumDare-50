using System;
using UnityEngine;

namespace Environnement
{
    public class TankerTap : MonoBehaviour
    {
        [SerializeField] private TankerTapCollection m_Collection;
        
        [SerializeField] private WaterFillable m_Fillable;
        private bool m_IsBlocked;

      //  private int justTest = 0;
        public bool IsBlocked => m_IsBlocked;

        // private void Update()
        // {
        //     if((justTest%100) == 0)
        //         Debug.Log(("la position du parent = " + transform.position));
        //     justTest++;
        // }

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
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITankerTapInteractable tankerTapInteract))
            {
                tankerTapInteract.OnApproachTap(this);
            }
        }
    }
}