using System;
using UnityEngine;

namespace Environnement
{
    public class Canon : MonoBehaviour
    {
        [SerializeField] private Transform lanceur;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEjectable putInCanon))
            {
                putInCanon.OnPutIn(this,lanceur);
            }
        }
    }
}