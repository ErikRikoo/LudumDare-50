using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GameStateHandling
{
    public class GameStartBroadcaster : MonoBehaviour
    {
        [SerializeField] private VoidEvent m_GameStarted;

        private void Awake()
        {
            if (m_GameStarted == null)
            {
                Debug.LogError("Game Start Broadcaster must have Game Started name");
                return;
            }
            
            m_GameStarted.Register(BroadcastGameStarted);
        }

        public void BroadcastGameStarted()
        {
            BroadcastMessage("OnGameStarted");
        }
    }
}