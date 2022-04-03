using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Environnement
{
    [RequireComponent(typeof(BoxCollider))]
    public class WaterFillable : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent m_OnTankerFull;
        
        [Header("Main Settings")]
        [SerializeField] private float m_FillingRate;
        [SerializeField] private GameObject m_WaterVisual;
        [SerializeField]private float m_FillingDamping = 1;

        private BoxCollider m_Collider;
        private Bounds m_OriginalBounds;

        [HideInInspector]
        public float FillingAmount;
        private float m_RealAmount;


        private Coroutine m_FillingCoroutine;

        private void Awake()
        {
            m_Collider = GetComponent<BoxCollider>();
            m_OriginalBounds = m_Collider.bounds;
            UpdateColliderAndVisual();
        }

        private void Update()
        {
            m_RealAmount = Mathf.Lerp(m_RealAmount, FillingAmount, m_FillingDamping * Time.deltaTime);
            UpdateColliderAndVisual();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IWaterInteractable waterInteract))
            {
                waterInteract.OnEnterWater(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IWaterInteractable waterInteract))
            {
                waterInteract.OnExitWater(this);
            }
        }

        private void UpdateColliderAndVisual()
        {
            var newSize = m_OriginalBounds.size;
            newSize.y *= m_RealAmount;
            m_Collider.size = newSize;
            var newPosition = m_OriginalBounds.center;
            newPosition.y = Mathf.LerpUnclamped(m_OriginalBounds.min.y, newPosition.y, m_RealAmount);
            m_Collider.center = newPosition;
            m_Collider.enabled = m_RealAmount > 0;
            

            if (m_WaterVisual == null)
            {
                return;
            }
            m_WaterVisual.SetActive(m_RealAmount > 0);
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
            while (FillingAmount < 1)
            {
                yield return null;
                FillingAmount = Mathf.Min(1, FillingAmount + m_FillingRate * Time.deltaTime);
            }
            
            m_OnTankerFull?.Invoke();

        }

        [Header("SpongeHandling")]
        [SerializeField] private float m_SpongeHeightOffset;
        [SerializeField] private float m_SpongeMovementDamping;
        [SerializeField] private NoiseSettings m_SpongeNoise;
        [SerializeField] private float m_SpongeNoiseIntensity;

        public void AttachOnSurface(Collider _collider, float _time)
        {
            var bounds = m_Collider.bounds;
            var newPosition = _collider.transform.localPosition;
            m_SpongeNoise.GetSignal(_time, out Vector3 _pos, out Quaternion _rotation);
            var targetHeight = bounds.max.y + _collider.bounds.extents.y + _pos.y * m_SpongeNoiseIntensity
                               - m_SpongeHeightOffset;

            
            newPosition.y = Mathf.LerpUnclamped(newPosition.y, targetHeight, m_SpongeMovementDamping);
            
            _collider.transform.position = newPosition;
        }
    }
}