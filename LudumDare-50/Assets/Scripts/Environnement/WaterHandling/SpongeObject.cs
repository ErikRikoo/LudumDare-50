using System;
using System.Collections;
using Cinemachine;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Environnement
{
    public class SpongeObject : MonoBehaviour, IWaterInteractable
    {
        [Header("Events")]
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
            m_OnFillingAmountChanged?.Invoke(m_FillingAmount / m_MaxAmount);
            if (!CanAbsorbWater)
            {
                m_OnFull?.Invoke();
            }
        }

        private Coroutine m_Coroutine;

        private IEnumerator c_SpongeWater(WaterFillable _waterFillable)
        {
            float startTime = Time.time + Random.Range(0, 5.17f);
            while (true)
            {
                yield return null;
                AbsorbWater(ref _waterFillable.FillingAmount);
                _waterFillable.AttachOnSurface(Collider, Time.time - startTime);
            }
        }

        public void OnEnterWater(WaterFillable _fillable)
        {
            m_Coroutine = StartCoroutine(c_SpongeWater(_fillable));

            if (Rigidbody != null)
            {
                Rigidbody.isKinematic = true;
            }
        }

        public void OnExitWater(WaterFillable _fillable)
        {
            StopCoroutine(m_Coroutine);
            if (Rigidbody != null)
            {
                Rigidbody.isKinematic = false;
            }
        }
    }
}