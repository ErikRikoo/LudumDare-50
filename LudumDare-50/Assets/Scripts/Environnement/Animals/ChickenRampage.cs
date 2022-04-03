using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(RandomWalker))]
[RequireComponent(typeof(Pathfinding.AIDestinationSetter))]
[RequireComponent(typeof(Pathfinding.AIPath))]
public class ChickenRampage : MonoBehaviour
{
    [SerializeField] float RampageMaxSpeed;
    [SerializeField] float RampageMinTimer;
    [Range(0, 1)]
    [SerializeField] float RampageRate;
    [SerializeField] Environnement.TankerTapCollection AvailableTaps;

    private Pathfinding.AIDestinationSetter m_DestinationSetter;
    private Pathfinding.AIPath m_Path;

    private Environnement.TankerTap m_TargetTap;
    private RandomWalker m_Walker;

    private float m_RampageTimer;
    private bool m_IsOnRampage = false;

    void Start()
    {
        m_Walker = GetComponent<RandomWalker>();
        m_DestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        m_Path = GetComponent<Pathfinding.AIPath>();
    }

    void Update()
    {
        UpdateRampageTimer(Time.deltaTime);

        if(IsOnRampage())
        {
            UpdateRampage();
            return;
        }

        if(DoRampageTrigger())
        {
            ResetRampageTimer();
            StartRampage();
        }

    }

    private void StartRampage()
    {
        m_Walker.StopWalk();
        m_Path.maxSpeed = RampageMaxSpeed;
        GetRampageTarget();
    }

    private void UpdateRampage()
    {
        if(HasReachedTap())
        {            
            //TODO: add chicken combat animation
            m_TargetTap.StartFilling();
            StopRampage();
            return;
        }
    }


    private void StopRampage()
    {
        m_Walker.StartWalk();
    }

    private void GetRampageTarget()
    {
        IEnumerable<Environnement.TankerTap> valid_taps = AvailableTaps.GetValidTankers();
        m_TargetTap = valid_taps.ElementAt(Random.Range(0, valid_taps.Count()));
        m_DestinationSetter.target = m_TargetTap.transform;
    }

    private bool HasReachedTap()
    {
        return m_Path.reachedDestination;
    }

    public bool IsOnRampage()
    {
        return m_IsOnRampage;
    }

    private void UpdateRampageTimer(float DeltaTime)
    {
        m_RampageTimer += DeltaTime;
    }
    private void ResetRampageTimer()
    {
        m_RampageTimer = 0;
    }

    private bool DoRampageTrigger()
    {
        if (RampageMinTimer <= m_RampageTimer)
        {
            return Random.value <= RampageRate;
        }
        return false;
    }
 }
