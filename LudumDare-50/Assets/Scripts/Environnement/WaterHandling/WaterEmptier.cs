using System;
using UnityEngine;

namespace Environnement
{
    public class WaterEmptier : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IWaterHolder holder))
            {
                holder.OnStartEmptying();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IWaterHolder holder))
            {
                holder.OnEndEmptying();
            }
        }
    }
}