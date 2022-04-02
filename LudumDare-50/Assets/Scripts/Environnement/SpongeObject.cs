using System;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Events;

namespace Environnement
{
    public class SpongeObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> m_OnFillingAmountChanged;
        [SerializeField] private UnityEvent m_OnFull;
        
        
        [Header("Settings")]
        [SerializeField] private float m_MaxAmount;
        [SerializeField] private float m_FillingRate;

        private float m_FillingAmount;
        private Collider m_Collider;

        public Collider Collider => m_Collider;
        
        private Rigidbody m_Rigidbody;

        public Rigidbody Rigidbody => m_Rigidbody;


        private void Awake()
        {
            m_Collider = GetComponent<Collider>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public bool CanAbsorbWater => m_FillingAmount < m_MaxAmount;

        public void AbsorbWater(ref float _tankerFillAmount)
        {
            var amount = Mathf.Min(m_MaxAmount - m_FillingAmount, m_FillingRate * Time.deltaTime, _tankerFillAmount);
            m_FillingAmount += amount;
            _tankerFillAmount -= amount;
            m_OnFillingAmountChanged?.Invoke(m_FillingAmount/m_MaxAmount);
            if (!CanAbsorbWater)
            {
                m_OnFull?.Invoke();
            }
        }
        
        
    }
}