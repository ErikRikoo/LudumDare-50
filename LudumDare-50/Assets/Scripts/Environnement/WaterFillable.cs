using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Environnement
{
    [RequireComponent(typeof(BoxCollider))]
    public class WaterFillable : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_OnTankerFull;
        
        [SerializeField] private float m_FillingRate;
        [SerializeField] private GameObject m_WaterVisual;
        private BoxCollider m_Collider;
        private Bounds m_OriginalBounds;

        private float m_FillingAmount;

        private Coroutine m_FillingCoroutine;

        private Dictionary<Collider, Coroutine> m_Sponges = new Dictionary<Collider, Coroutine>();

        private void Awake()
        {
            m_Collider = GetComponent<BoxCollider>();
            m_OriginalBounds = m_Collider.bounds;
            UpdateColliderAndVisual();
        }

        private void Start()
        {
            StartFilling();
        }

        private void UpdateColliderAndVisual()
        {
            var newSize = m_OriginalBounds.size;
            newSize.y *= m_FillingAmount;
            m_Collider.size = newSize;
            var newPosition = m_OriginalBounds.center;
            newPosition.y = Mathf.LerpUnclamped(m_OriginalBounds.min.y, newPosition.y, m_FillingAmount);
            m_Collider.center = newPosition;
            m_Collider.enabled = m_FillingAmount > 0;
            

            if (m_WaterVisual == null)
            {
                return;
            }
            m_WaterVisual.SetActive(m_FillingAmount > 0);
            var newWaterPosition = m_WaterVisual.transform.localPosition;
            newWaterPosition.y = m_Collider.bounds.max.y;
            m_WaterVisual.transform.position = newWaterPosition;
        }

        public void StartFilling()
        {
            m_FillingCoroutine = StartCoroutine(c_Fill());
        }

        public void StopFilling()
        {
            StopCoroutine(m_FillingCoroutine);
        }

        private IEnumerator c_Fill()
        {
            while (m_FillingAmount < 1)
            {
                yield return null;
                m_FillingAmount = Mathf.Min(1, m_FillingAmount + m_FillingRate * Time.deltaTime);
                UpdateColliderAndVisual();
            }
            
            m_OnTankerFull?.Invoke();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out SpongeObject sponge))
            {
                m_Sponges.TryAdd(other, StartCoroutine(c_SpongeWater(sponge)));
                if (sponge.Rigidbody != null)
                {
                    sponge.Rigidbody.isKinematic = true;   
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (m_Sponges.TryGetValue(other, out var coroutine))
            {
                StopCoroutine(coroutine);
                if (other.TryGetComponent<Rigidbody>(out var rigidbody))
                {
                    rigidbody.isKinematic = false;
                }
            }

            m_Sponges.Remove(other);
        }
        
        [Header("SpongeHandling")]
        [SerializeField] private float m_SpongeHeightOffset;
        [SerializeField] private float m_SpongeMovementDamping;
        [SerializeField] private NoiseSettings m_SpongeNoise;
        [SerializeField] private float m_SpongeNoiseIntensity;

        private IEnumerator c_SpongeWater(SpongeObject _sponge)
        {
            float startTime = Time.time + Random.Range(0, 5.17f);
            while (true)
            {
                yield return null;
                _sponge.AbsorbWater(ref m_FillingAmount);
                UpdateColliderAndVisual();
                UpdateSpongeObject(_sponge, Time.time - startTime);
            }
        }

        private void UpdateSpongeObject(SpongeObject _sponge, float _time)
        {
            var bounds = m_Collider.bounds;
            var newPosition = _sponge.transform.localPosition;
            m_SpongeNoise.GetSignal(_time, out Vector3 _pos, out Quaternion _rotation);
            var targetHeight = bounds.max.y + _sponge.Collider.bounds.extents.y + _pos.y * m_SpongeNoiseIntensity
                            - m_SpongeHeightOffset;

            
            newPosition.y = Mathf.LerpUnclamped(newPosition.y, targetHeight, m_SpongeMovementDamping);
            
            _sponge.transform.position = newPosition;
        }
    }
}