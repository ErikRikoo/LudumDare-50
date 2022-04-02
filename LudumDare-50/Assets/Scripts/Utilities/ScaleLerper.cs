using System;
using UnityEngine;

namespace Utilities
{
    public class ScaleLerper : MonoBehaviour
    {
        [SerializeField] private Vector3 m_TargetScale;
        private Vector3 m_OriginalScale;

        private void Awake()
        {
            m_OriginalScale = transform.localScale;
        }

        public void Lerp(float _factor)
        {
            transform.localScale = Vector3.LerpUnclamped(m_OriginalScale, m_TargetScale, _factor);
        }
    }
}