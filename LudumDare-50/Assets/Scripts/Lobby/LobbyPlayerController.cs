using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lobby
{
    public class LobbyPlayerController : MonoBehaviour
    {
        [SerializeField] private Renderer m_RendererToUpdate;
        
        private Vector3 m_OriginalScale;
        private PlayerHandling m_PlayerHandling;
        private bool m_IsReady;

        private void Awake()
        {
            m_OriginalScale = transform.localScale;
        }

        public void BindToHandler(PlayerHandling playerHandling, Color _playerColor)
        {
            m_PlayerHandling = playerHandling;
            m_RendererToUpdate.material.color = _playerColor;
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