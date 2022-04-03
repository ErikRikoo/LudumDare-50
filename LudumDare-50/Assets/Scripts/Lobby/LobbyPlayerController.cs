using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lobby
{
    public class LobbyPlayerController : MonoBehaviour
    {

        private Vector3 m_OriginalScale;
        private PlayerHandling m_PlayerHandling;
        private bool m_IsReady;

        private void Awake()
        {
            m_OriginalScale = transform.localScale;
        }

        public void BindToHandler(PlayerHandling playerHandling)
        {
            m_PlayerHandling = playerHandling;
        }

        public void OnReady(InputAction.CallbackContext _context)
        {
            if (m_IsReady)
            {
                return;
            }
            
            ++m_PlayerHandling.PlayerReadyCount;
            transform.localScale *= 2f;
            m_IsReady = true;
        }

        public void OnCancelReady(InputAction.CallbackContext _context)
        {
            if (!m_IsReady)
            {
                return;
            }
            
            --m_PlayerHandling.PlayerReadyCount;
            transform.localScale = m_OriginalScale;
            m_IsReady = false;
        }
    }
}