using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lobby
{
    public class PlayerHandling : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnPositions;
        [SerializeField] private Transform m_Parent;

        private List<LobbyPlayerController> m_Players = new();

        private int m_PlayerCount;

        private int PlayerCount
        {
            get => m_PlayerCount;
            set
            {
                var oldValue = m_PlayerCount;
                m_PlayerCount = value;
                OnPlayerCountChanged(oldValue);
            }
        }

        private void OnPlayerCountChanged(int oldValue)
        {
            
        }

        [SerializeField]
        private int m_PlayerReadyCount;
        
        public int PlayerReadyCount
        {
            get => m_PlayerReadyCount;
            set
            {
                var oldValue = m_PlayerReadyCount;

                m_PlayerReadyCount = value;
                OnReadyChanged(oldValue);
            }
        }

        private void OnReadyChanged(int oldValue)
        {
            
        }


        public void OnPlayerJoined(PlayerInput _input)
        {
            var lobbyPlayer = _input.GetComponent<LobbyPlayerController>();
            if (lobbyPlayer == null)
            {
                return;
            }

            lobbyPlayer.transform.parent = m_Parent;
            lobbyPlayer.transform.position = m_SpawnPositions[m_PlayerCount].position;
            lobbyPlayer.BindToHandler(this);
            StartCoroutine(c_EnablePlayer(_input));
            ++PlayerCount;
        }

        private IEnumerator c_EnablePlayer(PlayerInput _input)
        {
            yield return null;
            _input.SwitchCurrentActionMap("Menu");

        }

        private void EnableGameplayInputs(PlayerInput _input)
        {
            _input.SwitchCurrentActionMap("Gameplay");
        }
    }
}