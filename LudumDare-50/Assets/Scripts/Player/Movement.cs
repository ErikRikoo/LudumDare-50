using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_target;
    [SerializeField] private float m_Speed = 1f;
    [Range(1, 10)]
    [SerializeField] private float m_Damping;

    private int m_MovementParameter;
    private Vector2 m_TargetMovement;
    private Vector2 m_Movement;

    private void Awake()
    {
        m_MovementParameter = Animator.StringToHash("Movement");
    }

    public void OnMovement(InputAction.CallbackContext _context)
    {
        m_TargetMovement = _context.ReadValue<Vector2>();
    }


    private void Update()
    {
        m_Movement = Vector2.Lerp(m_Movement, m_TargetMovement, m_Damping * Time.deltaTime);
        Vector2 planeMovement = m_Movement * m_Speed;
        Vector2 smoothedMovement = planeMovement * Time.deltaTime;
        m_target.transform.Translate(smoothedMovement.x, 0, smoothedMovement.y, Space.World);
        Debug.Log($"Movement = {m_Movement}; planeMovement = {planeMovement}; smoothedMovement={smoothedMovement}");
        
        m_Animator.SetFloat(m_MovementParameter, planeMovement.magnitude / m_Speed);
    }
}