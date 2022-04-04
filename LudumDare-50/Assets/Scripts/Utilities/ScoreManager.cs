using TMPro;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] FloatVariable m_ScoreVariable;
    [SerializeField] VoidEvent m_StartEvent;
    [SerializeField] VoidEvent m_StopEvent;
    [SerializeField]  TextMeshProUGUI m_ScoreTextMesh;

    private float m_StartTime;
    private float m_EndTime;

    private void Start()
    {
        m_StartEvent.Register(StartTimer);
        m_StopEvent.Register(StopTimer);
    }

    private void StartTimer()
    {
        m_StartTime = Time.time;
    }

    private void StopTimer()
    {
        m_EndTime = Time.time;
        m_ScoreVariable.Value = m_EndTime - m_StartTime;
        m_ScoreTextMesh.text = "Score : " + ((int)m_ScoreVariable.Value);

    }
}
