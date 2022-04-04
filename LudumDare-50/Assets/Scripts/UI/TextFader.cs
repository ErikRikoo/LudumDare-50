using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextFader : MonoBehaviour
    {
        [SerializeField] private float m_FadeDuration = 0.5f;
        [Tooltip("Disappear or Appear")]
        [SerializeField] private bool m_Appear;


        private TextMeshProUGUI m_Text;

        private void Awake()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            UpdateAlpha(0);

        }

        public void ChangeText(string _text)
        {
            m_Text.text = _text;
            UpdateAlpha(1);

            StartCoroutine(c_Fading());
        }

        private IEnumerator c_Fading()
        {
            float time = m_FadeDuration;
            while (time > 0)
            {
                yield return null;
                time -= Time.deltaTime;
                UpdateAlpha(time / m_FadeDuration);
            }
            
            UpdateAlpha(0);
            
        }

        private void UpdateAlpha(float _interp)
        {
            m_Text.alpha = m_Appear? 1 - _interp:_interp;
        }
    }
}