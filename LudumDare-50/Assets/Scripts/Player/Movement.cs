using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    [SerializeField] private float m_Speed = 1f;
    [Range(1, 10)]
    [SerializeField] private float m_Damping;

    private Vector2 m_TargetMovement;
    private Vector2 m_Movement;

    public void OnMovement(InputAction.CallbackContext _context)
    {
        m_TargetMovement = _context.ReadValue<Vector2>();
    }


    private void Update()
    {
        m_Movement = Vector2.Lerp(m_Movement, m_TargetMovement, m_Damping * Time.deltaTime);
        Vector2 planeMovement = m_Movement * (Time.deltaTime * m_Speed);
        m_target.transform.Translate(planeMovement.x, 0, planeMovement.y, Space.World);
    }
}