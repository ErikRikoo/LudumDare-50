using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Lobby
{
    public class LobbyPlayerController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private Renderer m_RendererToUpdate;
        [SerializeField] private AudioSource m_ReadySource;
        
        [SerializeField] private UnityEvent<bool> m_ReadynessChanged;
        
        
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
            m_ReadySource.Play();
            m_Animator.SetBool("Ready",true);
            m_ReadynessChanged?.Invoke(true);
            //transform.localScale *= 1.1f;
            m_IsReady = true;
        }

        public void OnCancelReady(InputAction.CallbackContext _context)
        {
            if (!m_IsReady)
            {
                return;
            }
            
            --m_PlayerHandling.PlayerReadyCount;
            m_Animator.SetBool("Ready",false);
            m_ReadynessChanged?.Invoke(false);
            transform.localScale = m_OriginalScale;
            m_IsReady = false;
        }
    }
}