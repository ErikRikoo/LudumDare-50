using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(RandomWalker))]
public class ChickenRampage : MonoBehaviour
{
    [SerializeField] float RampageMinTimer;
    [Range(0, 1)]
    [SerializeField] float RampageRate;
    [SerializeField] Environnement.TankerTapCollection AvailableTaps;

    private RandomWalker m_Walker;
    private float m_RampageTimer;
    private Environnement.TankerTap m_TargetTap;
    private bool m_IsOnRampage = false;

    void Start()
    {
        m_Walker = GetComponent<RandomWalker>();
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
        TargetNewTap();
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

        ChaseTap();

    }

    private void ChaseTap()
    {
        //TODO: implement chase
    }

    private void StopRampage()
    {
        m_Walker.StartWalk();
    }

    private void TargetNewTap()
    {
        IEnumerable<Environnement.TankerTap> valid_taps = AvailableTaps.GetValidTankers();
        m_TargetTap = valid_taps.ElementAt(Random.Range(0, valid_taps.Count()));
    }

    private bool HasReachedTap()
    {
        // FIXME: handle chicken reach
        return false;
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
