using System;
using Cinemachine;
using UnityEngine;

namespace Boat
{
    public class BoatMovement : MonoBehaviour
    {
        [SerializeField] private NoiseSettings m_Settings;
        [SerializeField] private bool m_ChangePosition = true;
        [SerializeField] private bool m_ChangeRotation = true;
        
        private float m_StartTime;
        private Vector3 m_StartPostion;
        private Quaternion m_StartRotation;

        private void Start()
        {
            m_StartTime = Time.time;
            m_StartPostion = transform.position;
            m_StartRotation = transform.rotation;
        }

        private void Update()
        {
            var currentAge = Time.time - m_StartTime;
            m_Settings.GetSignal(currentAge, out Vector3 movement, out Quaternion rotation);

            if (m_ChangePosition)
            {
                var newPosition = m_StartPostion + movement;
                transform.position = newPosition;
            }

            if (m_ChangeRotation)
            {
                var newRotation = m_StartRotation * rotation;
                transform.rotation = newRotation;
            }
            
        }
    }
}