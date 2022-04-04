using System;
using System.Collections;
using Cinemachine;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Environnement
{
    public class SpongeObject : MonoBehaviour, IWaterInteractable, IWaterHolder
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<float> m_OnFillingAmountChanged;
        [SerializeField] private UnityEvent m_OnFull;


        [Header("Settings")] 
        [SerializeField] private float m_MaxAmount;
        [SerializeField] private float m_FillingRate;

        [SerializeField]
        private float m_FillingAmount;

        private float FillingAmount
        {
            get => m_FillingAmount;
            set
            {
                m_FillingAmount = value;
                m_OnFillingAmountChanged?.Invoke(m_FillingAmount / m_MaxAmount);

            }
        }
        
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
            FillingAmount += amount;
            _tankerFillAmount -= amount;
            if (!CanAbsorbWater)
            {
                m_OnFull?.Invoke();
            }
        }

        private Coroutine m_Coroutine;

        private IEnumerator c_SpongeWater(WaterFillable _waterFillable)
        {
            _waterFillable.AttachOnSurface(Collider);

            while (true)
            {
                yield return null;
                AbsorbWater(ref _waterFillable.FillingAmount);
            }
        }

        public void OnEnterWater(WaterFillable _fillable)
        {
            if (m_Coroutine != null)
            {
                return;
            }
            m_Coroutine = StartCoroutine(c_SpongeWater(_fillable));

            if (Rigidbody != null)
            {
                Rigidbody.useGravity = false;
            }
        }

        public void OnExitWater(WaterFillable _fillable)
        {
            if (m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }

            if (Rigidbody != null)
            {
                Rigidbody.useGravity = true;
            }
        }

        [SerializeField] private float m_EmptyingSpeed;
        
        private Coroutine m_EmptyCoroutine;

        public bool AlreadyEmptying => m_EmptyCoroutine != null || IsEmpty;

        public bool IsEmpty => m_FillingAmount <= 0f;
        public void OnStartEmptying()
        {
            Debug.Log("On Start Emptying: " + m_EmptyCoroutine + " " + m_FillingAmount);
            if (AlreadyEmptying)
            {
                return;
            }

            m_EmptyCoroutine = StartCoroutine(c_Emptying());
        }

        private IEnumerator c_Emptying()
        {
            while (!IsEmpty)
            {
                yield return null;
                var emptyValue = m_EmptyingSpeed * Time.deltaTime;
                if (emptyValue > FillingAmount)
                {
                    break;
                }
                FillingAmount -= emptyValue;
            }

            FillingAmount = 0;
        }

        public void OnEndEmptying()
        {
            if (m_EmptyCoroutine == null)
            {
                return;
            }
            
            StopCoroutine(m_EmptyCoroutine);
        }
    }
}