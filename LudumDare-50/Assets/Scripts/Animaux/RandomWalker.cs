using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
public class RandomWalker : MonoBehaviour
{
    [SerializeField] float MaxSpeed = 1;
    [SerializeField] int DirectionUpdateTime;
    [SerializeField] private int m_MaxDx;
    [SerializeField] private int m_MaxDz;


    private AIPath m_Path;
    private Transform m_InitialTarget;
    private bool m_IsWalking;
    private float m_ElapsedTime = 0;

    void Start()
    {
        m_Path = GetComponent<AIPath>();
        m_Path.maxSpeed = MaxSpeed;
        StartWalk();
    }

    public void StartWalk() 
    {
        m_IsWalking = true;
        m_Path.maxSpeed = MaxSpeed;
        MoveTarget();
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
        dz = Random.Range(-m_MaxDz, m_MaxDz);
        Vector3 newPosition = new Vector3(dx, 0, dz);
        m_Path.destination = newPosition;
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
