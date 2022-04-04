using System;
using UnityEngine;

namespace Environnement
{
    public class Canon : MonoBehaviour
    {
        [SerializeField] private Transform lanceur;
        private Collider objet_to_eject;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEjectable putInCanon))
            {
                objet_to_eject = other;
                putInCanon.OnPutIn(this,lanceur);
            }
        }

        public void eject_object()
        {
            objet_to_eject.GetComponent<Eject_object>().byebye_object();
        }
    }
}