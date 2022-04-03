using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirateur : MonoBehaviour
{
    [SerializeField] private ColliderEvent TakenByCanon;

 [SerializeField] private Animator m_Animator;
 
 [SerializeField] private float P_expulsion = 100;
 
 [SerializeField] private float max_angle_detection = 60;
 [SerializeField] private Transform position_object;
 [SerializeField] private float Speed_come = 1;
 [SerializeField] private Transform default_parent = null;
 

 private int m_AspireKey;
    private List<Collider> objectProche;
    private Collider objetToAspire;

    private bool aspire_enCours = false;
  // [SerializeField]    private bool push_E = false;

    private void Awake()
    {
        objectProche = new List<Collider>();
        m_AspireKey = Animator.StringToHash("Aspire");
        TakenByCanon.Register(DropObject);
    }

    private void DropObject(Collider _collider)
    {
       // Debug.Log("le canon a choppé un objet = " + _collider.GetComponent<Transform>().name);
        if (_collider == objetToAspire)
        {
            objetToAspire.transform.parent = default_parent;
            objetToAspire.transform.up = Vector3.up;
            objetToAspire.attachedRigidbody.isKinematic = false;
            objetToAspire = null;
        }
    }

    private void Update()
    {
        if (aspire_enCours && (objetToAspire != null))
            aspire_object();
    }

    public void Fire(InputAction.CallbackContext context)
    {
      //Debug.Log("phase  = " +context.phase);
      if (context.phase == InputActionPhase.Started)
      {
          m_Animator?.SetBool(m_AspireKey, true);
          find_object_to_aspire();
          if (objetToAspire == null)
              return;
          aspire_enCours = true;
          objetToAspire.GetComponent<Rigidbody>().isKinematic = true;
          //objetToAspire.enabled = false;
          
      }

      if (context.phase == InputActionPhase.Canceled)
      {
          if(aspire_enCours)
              aspire_enCours = false;
          else
              expulse_object();

          objetToAspire = null;

          m_Animator?.SetBool(m_AspireKey, false);
      }
    }

    private void OnTriggerEnter(Collider other)//quand un objet entre dans la 
    {
          
            objectProche.Add(other);
            
    }

    private void OnTriggerExit(Collider other)
    {
        objectProche.Remove(other);
    }
    
    private void find_object_to_aspire()
    {
        if (objectProche.Count < 1)
            return;
        
        
        for (int i = 0; i < objectProche.Count; i++)
        {
            if (!verif_angle(objectProche[i]))
                continue;
            if(!objetToAspire)
                objetToAspire = objectProche[0];
            if (Vector3.Distance(transform.position,objectProche[i].transform.position) < Vector3.Distance(transform.position,objetToAspire.transform.position))
                objetToAspire = objectProche[i];
        }
        //Debug.Log("le plus proche = " + objetToAspire.name);
    }

    private bool verif_angle(Collider other)
    {
        Vector2 vect_aspirateur = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 vect_object = new Vector2(other.transform.position.x-transform.position.x, other.transform.position.z-transform.position.z);
        float angle = Vector2.SignedAngle(vect_aspirateur, vect_object);
        Debug.Log("angle for collide = " + angle);
        if (Mathf.Abs(angle) <= max_angle_detection)
            return true;
        return false;
    }

    private void aspire_object()
    {
        Vector3 movement_object = (position_object.transform.position-objetToAspire.transform.position) *Time.deltaTime * Speed_come;
        Debug.Log("aspire object =  " + movement_object); 
        if (Vector3.Distance(position_object.transform.position, objetToAspire.transform.position) < 0.3)
        {
            objetToAspire.transform.parent = position_object;
            Debug.Log("backward =  " + -position_object.transform.forward);
            objetToAspire.transform.up = -position_object.transform.forward;
            objetToAspire.transform.localPosition = Vector3.zero;
            aspire_enCours = false;
            Debug.Log("end of aspire object ");
        }
        else
        {
            objetToAspire.transform.Translate(movement_object,Space.World);
        }

      
    }

    private void expulse_object()
    {
        if (objetToAspire == null)
            return;
        objetToAspire.transform.parent = default_parent;
        objetToAspire.transform.up = Vector3.up;
        objetToAspire.attachedRigidbody.isKinematic = false;
        objetToAspire.attachedRigidbody.AddForce((transform.forward + transform.up)*P_expulsion);
        Debug.Log("expulse object = " );
    }
}
