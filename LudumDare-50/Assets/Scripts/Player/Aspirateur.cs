using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aspirateur : MonoBehaviour
{
 //   [SerializeField] private VoidEvent event_;

 [SerializeField] private float P_expulsion = 100;
 
 [SerializeField] private float max_angle_detection = 60;
 [SerializeField] private Transform position_object;
 [SerializeField] private float Speed_come = 1;
 
 
    private List<Collider> objectProche;
    private Collider objetToAspire;

    private bool aspire_enCours = false;
  // [SerializeField]    private bool push_E = false;

    private void Awake()
    {
        objectProche = new List<Collider>();
      //  Debug.Log("APPEL ");
    }

    private void Update()
    {
        if (aspire_enCours)
            aspire_object();
    }

    public void Fire(InputAction.CallbackContext context)
    {
      //Debug.Log("phase  = " +context.phase);
      if (context.phase == InputActionPhase.Started)
      {
          find_object_to_aspire();
          aspire_enCours = true;
      }

      if (context.phase == InputActionPhase.Canceled)
      {
          if(aspire_enCours)
              aspire_enCours = false;
          else
              expulse_object();

          objetToAspire = null;

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
        if(objetToAspire == null)
            return;
        Vector3 movement_object = new Vector3(position_object.transform.position.x-objetToAspire.transform.position.x,position_object.transform.position.y-objetToAspire.transform.position.y, position_object.transform.position.z-objetToAspire.transform.position.z) *Time.deltaTime * Speed_come;
        Debug.Log("aspire object ");
      //  objetToAspire.transform.position = position_object.position;
      if (Vector3.Distance(position_object.transform.position, objetToAspire.transform.position) < 0.3)
      {
          objetToAspire.transform.parent = position_object;
          objetToAspire.attachedRigidbody.isKinematic = true;
          aspire_enCours = false;
          Debug.Log("end of aspire object ");
      }
      else
      {
          objetToAspire.transform.Translate(movement_object);
      }

      
    }

    private void expulse_object()
    {
        if (objetToAspire == null)
            return;
        objetToAspire.transform.parent = null;
        objetToAspire.attachedRigidbody.isKinematic = false;
        objetToAspire.attachedRigidbody.AddForce((transform.forward + transform.up)*P_expulsion);
        Debug.Log("expulse object = " );
    }
}
