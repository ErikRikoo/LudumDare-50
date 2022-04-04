using System;
using System.Collections;
using System.Collections.Generic;
using Environnement;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Tortue : MonoBehaviour,ITankerTapInteractable
{
    [SerializeField] private float Speed_tortue = 1;
    [SerializeField] private ColliderEvent Taken;
    
    private Animator m_Animator;
    private bool GoToHole = false;
    private TankerTap Hole_to_block;
    private void Update()
    {
        if (GoToHole)
        {
            movementToHole();
        }
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("Movement", 0.5f);
    }

    public void movementToHole()
    {
        Vector3 movement_object = (Hole_to_block.transform.position-transform.position) *Time.deltaTime * Speed_tortue;
        Debug.Log("aspire object =  " + movement_object);
        if (Vector3.Distance(Hole_to_block.transform.position, transform.position) < 0.3)
        {
            transform.parent = Hole_to_block.transform;
            transform.up = Hole_to_block.transform.up;
            transform.localPosition = Vector3.zero;
            GoToHole = false;
            m_Animator.SetFloat("Movement", 0.5f);
            Hole_to_block.StopFilling();
            Debug.Log("end of block hole ");
        }
        else
        {
            transform.Translate(movement_object,Space.World);
        }
    }
    public void OnApproachTap(TankerTap _tankerTap)
    {
        Debug.Log("LA TORTUE EST PRETE A BOUCHER LE TROU");
        Taken.Raise(GetComponent<Collider>());
        // marcher jusqu'au trou
         Hole_to_block  = _tankerTap;
         transform.forward = -(Hole_to_block.transform.position - transform.position).normalized;
         GoToHole = true;
         m_Animator.SetFloat("Movement", 1f);
         //arreter l'écoulement 
         

    }

    public void OnAwayTap(TankerTap _tankerTap)
    {
        //rien faire ici 
    }
}
