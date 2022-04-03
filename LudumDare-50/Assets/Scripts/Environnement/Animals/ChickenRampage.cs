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

    public bool IsOnRampage => m_IsOnRampage;
    public bool HasReachedTap => m_Path.reachedDestination;
    public bool IsTargetStillValid => m_TargetTap != null;

    void Start()
    {
        m_Walker = GetComponent<RandomWalker>();
        m_DestinationSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        m_Path = GetComponent<Pathfinding.AIPath>();
    }

    void Update()
    {
        if(IsOnRampage)
        {
            UpdateRampage();
            return;
        }

        UpdateRampageTimer(Time.deltaTime);
        if (DoRampageTrigger())
        {
            ResetRampageTimer();
            StartRampage();
        }

    }

    private void StartRampage()
    {
        Debug.Log("Start rampage");

        IEnumerable<Environnement.TankerTap> valid_taps = AvailableTaps.GetFillableTankers();
        if (valid_taps.Count() == 0)
        {
            Debug.Log("No available target for rampage");
            return;
        }

        m_TargetTap = valid_taps.ElementAt(Random.Range(0, valid_taps.Count()));
        m_DestinationSetter.target = m_TargetTap.transform;
        m_Walker.StopWalk();

        m_IsOnRampage = true;
        m_Path.maxSpeed = RampageMaxSpeed;
    }

    private void UpdateRampage()
    {
        if(!IsTargetStillValid)
        {
            Debug.Log("Target made invalid");
            StopRampage();
            return;
        }

        if(HasReachedTap)
        {
            Debug.Log("Reached target");
            //TODO: add chicken combat animation
            m_TargetTap.StartFilling();
            StopRampage();          
            return;
        }
    }

    private void StopRampage()
    {
        Debug.Log("Stop rampage");
        m_IsOnRampage = false;
        m_Walker.StartWalk();
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
