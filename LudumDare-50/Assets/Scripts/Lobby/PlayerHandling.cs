using System.Collections;
using System.Collections.Generic;
using UI;
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lobby
{
    struct PlayerEntry
    {
        public LobbyPlayerController Controller;
        public PlayerInput Input;
    }
    
    public class PlayerHandling : MonoBehaviour
    {
        [SerializeField] private VoidEvent m_GameStarted;
        
        [SerializeField] private TextFader m_TextDisplay;
        
        [SerializeField] private Color[] m_PlayerColors;
        [SerializeField] private Transform[] m_SpawnPositions;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private int m_StartTimerDuration;
        

        private List<PlayerEntry> m_Players = new();

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
            if (oldValue < m_PlayerCount)
            {
                if (m_PlayerCount > m_PlayerReadyCount)
                {
                    StopTimerIfNeeded();
                }
            }
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
            if (oldValue < m_PlayerReadyCount)
            {
                if (m_PlayerReadyCount == m_PlayerCount)
                {
                    StartTimer();
                }
            }
            else
            {
                if (m_PlayerReadyCount < m_PlayerCount)
                {
                    StopTimerIfNeeded();
                }
            }
        }

        private Coroutine m_TimerCoroutine;
        
        private void StartTimer()
        {
            if (m_TimerCoroutine != null)
            {
                return;
            }

            m_TimerCoroutine = StartCoroutine(c_Timer());
        }

        private void StopTimerIfNeeded()
        {
            if (m_TimerCoroutine == null)
            {
                return;
            }
            
            StopCoroutine(m_TimerCoroutine);
            m_TimerCoroutine = null;
        }

        private IEnumerator c_Timer()
        {
            for (int time = m_StartTimerDuration; time > 0; --time)
            {
                UpdateText(time);
                yield return new WaitForSeconds(1);
            }

            DisplayGo();
            StartGame();
        }

        private void DisplayGo()
        {
            m_TextDisplay.ChangeText("GO!");
        }

        private void UpdateText(int time)
        {
            m_TextDisplay.ChangeText(time.ToString());
        }

        public void OnPlayerJoined(PlayerInput _input)
        {
            var lobbyPlayer = _input.GetComponent<LobbyPlayerController>();
            if (lobbyPlayer == null)
            {
                return;
            }

            m_Players.Add(new PlayerEntry
            {
                Controller = lobbyPlayer,
                Input = _input,
            });
            lobbyPlayer.transform.parent = m_Parent;
            lobbyPlayer.transform.position = m_SpawnPositions[m_PlayerCount].position;
            lobbyPlayer.transform.forward = m_SpawnPositions[m_PlayerCount].forward;
            lobbyPlayer.BindToHandler(this, m_PlayerColors[m_PlayerCount]);
            StartCoroutine(c_EnablePlayer(_input));
            ++PlayerCount;
        }

        private IEnumerator c_EnablePlayer(PlayerInput _input)
        {
            yield return null;
            _input.SwitchCurrentActionMap("Menu");
        }

        private void StartGame()
        {
            m_GameStarted?.Raise();
            foreach (var player in m_Players)
            {
                EnableGameplayInputs(player.Input);
            }
        }
        
        private void EnableGameplayInputs(PlayerInput _input)
        {
            _input.SwitchCurrentActionMap("Gameplay");
        }
    }
}