using System;
using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Void = UnityAtoms.Void;

namespace DefaultNamespace.Animaux
{
    public class ChickenSPawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_ChickenPrefab;
        
        [SerializeField] private VoidEvent m_OnChickenDeath;
        [SerializeField] private Transform[] m_SpawnPoint;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private float m_SPawnDelay = 1f;
        [SerializeField] private int m_SpawnCOunt = 2;

        private int m_SpawnIndex;

        public int SpawnIndex
        {
            get => m_SpawnIndex;
            set
            {
                m_SpawnIndex = value % m_SpawnPoint.Length;
            }
        }

        private void Awake()
        {
            m_OnChickenDeath.Register(OnChickenDeath);
        }

        private void OnChickenDeath()
        {
            StartCoroutine(c_SpawnAfterDelay());
        }

        private IEnumerator c_SpawnAfterDelay()
        {
            yield return new WaitForSeconds(m_SPawnDelay);
            for (int i = 0; i < m_SpawnCOunt; i++)
            {
                Instantiate(m_ChickenPrefab, m_SpawnPoint[SpawnIndex++].position, Quaternion.identity, m_Parent);
            }
        }
    }
}