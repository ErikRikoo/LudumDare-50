using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding.AIDestinationSetter))]
[RequireComponent(typeof(Pathfinding.AIPath))]
public class RandomWalker : MonoBehaviour
{

    [SerializeField] float MaxSpeed = 1;
    [SerializeField] int DirectionUpdateTime;
    [SerializeField] float m_MaxDx;
    [SerializeField] float m_MaxDy;
    [SerializeField] float m_MaxDz;

    private Pathfinding.AIDestinationSetter m_DestinationSetter;
    private Pathfinding.AIPath m_Path;
    private bool m_IsWalking;
    private float m_ElapsedTime = 0;

    void Start()
    {
        m_DestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        m_Path = GetComponent<Pathfinding.AIPath>();
        StartWalk();
    }

    public void StartWalk() 
    {
        m_IsWalking = true;
        m_Path.maxSpeed = MaxSpeed;
    }

    public void StopWalk() 
    {
        m_IsWalking = false;
    }

    public bool IsWalking()
    {
        return m_IsWalking;
    }

    private void UpdateTimer(float time) 
    {
        m_ElapsedTime += time;
    }

    private void ResetTimer()
    {
        m_ElapsedTime = 0;
    }
    private bool DoMoveTarget()
    {
        return DirectionUpdateTime < m_ElapsedTime || m_Path.reachedDestination;
    }

    private void MoveTarget()
    {
        float dx, dy, dz;
        dx = Random.Range(-m_MaxDx, m_MaxDx);
        dy = Random.Range(-m_MaxDy, m_MaxDy);
        dz = Random.Range(-m_MaxDz, m_MaxDz);
        m_DestinationSetter.target.position += new Vector3(dx, dy, dz);
    }
  
    void Update()
    {
        if (!IsWalking())
        {
            return;
        }

        UpdateTimer(Time.deltaTime);

        if (DoMoveTarget())
        {
            MoveTarget();
            ResetTimer();
        }
    }
}
