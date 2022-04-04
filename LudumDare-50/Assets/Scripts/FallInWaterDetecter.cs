using System;
using System.Security.Cryptography;
using UnityEngine;

namespace DefaultNamespace
{
    public class FallInWaterDetecter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FallInWaterListener listener))
            {
                listener.OnInFallInWater();
                Destroy(listener.gameObject);
            }
        }
    }
}