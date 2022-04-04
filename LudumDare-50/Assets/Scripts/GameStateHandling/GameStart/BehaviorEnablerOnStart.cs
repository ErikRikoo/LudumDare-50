using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GameStateHandling
{
    public class BehaviorEnablerOnStart : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private VoidEvent m_GameStart;
        
        [SerializeField] private MonoBehaviour[] m_Behaviors;

        private void Awake()
        {
            m_GameStart.Register(OnGameStarted);
            foreach (var behavior in m_Behaviors)
            {
                behavior.enabled = false;
            }
        }

        public void OnGameStarted()
        {
            foreach (var behavior in m_Behaviors)
            {
                behavior.enabled = true;
            }
        }
    }
}