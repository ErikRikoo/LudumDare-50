using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStateHandling;
using UnityEngine;


[RequireComponent(typeof(RandomWalker))]
[RequireComponent(typeof(Pathfinding.AIPath))]
public class ChickenRampage : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    [SerializeField] float RampageMaxSpeed;
    [SerializeField] float RampageMinTimer;
    [Range(0, 1)]
    [SerializeField] float RampageRate;
    [SerializeField] Environnement.TankerTapCollection AvailableTaps;

    private Pathfinding.AIPath m_Path;

    private Environnement.TankerTap m_TargetTap;
    private RandomWalker m_Walker;

    private float m_RampageTimer;
    private bool m_IsOnRampage = false;
    private bool m_AttackStarted = false;

    public bool IsOnRampage => m_IsOnRampage;
    public bool HasReachedTap => m_Path.reachedDestination;
    public bool IsTargetStillValid => m_TargetTap != null;

    void Start()
    {
        m_Walker = GetComponent<RandomWalker>();
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
        Debug.Log("Taps count = " + valid_taps.Count());
        if (valid_taps.Count() == 0)
        {
            Debug.Log("No available target for rampage");
            return;
        }

        m_TargetTap = valid_taps.ElementAt(Random.Range(0, valid_taps.Count()));
        m_Path.destination = m_TargetTap.transform.position;
        m_Walker.StopWalk();
        m_Path.canMove = true;
        m_Animator.SetFloat("Movement", 1.2f);


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

        if(HasReachedTap && !m_AttackStarted)
        {
            Debug.Log("Reached target");
            m_Animator.SetTrigger("Attack");
            m_AttackStarted = true;
            //TODO: add chicken combat animation
       
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

    public void OnAttackEnded()
    {
        m_AttackStarted = false;
        m_TargetTap.StartFilling();
        m_TargetTap = null;
        StopRampage();   
    }
}
