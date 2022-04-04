using System.Collections;
using System.Collections.Generic;
using Environnement;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Eject_object : MonoBehaviour,IEjectable
{
    
    [SerializeField] private float P_canon = 200;
    [SerializeField] private ColliderEvent Taken;
    //[SerializeField] private float time_delay = 0;
    private Animator m_Animator;
    private notCatchable _notCatchable;
    
    public void OnPutIn(Canon _canon,Transform lanceur)
    {
        Taken.Raise(GetComponent<Collider>());

        m_Animator = _canon.GetComponent<Animator>();
        // l'objet va en position lanceur 
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.parent = lanceur;
        transform.forward = lanceur.forward;
        transform.localPosition = Vector3.zero + (lanceur.transform.forward * GetComponent<Collider>().bounds.extents.magnitude);
        
        _notCatchable=gameObject.AddComponent<notCatchable>();
        Debug.Log("pret à être envoyé!!!!" + transform.name + " = "+transform.forward + "= ?  " + lanceur.name + " = " +lanceur.up);
        // go animation
        m_Animator.SetTrigger("Fire");
       // StartCoroutine(delay());

    }

    public void OnPuOut(Canon _canon)
    {
        //DO NOTHING
    }
    public void byebye_object()
    {
        
        transform.parent= null;
        transform.GetComponent<Rigidbody>().useGravity = true;
       transform.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * P_canon);
       Debug.Log("bye bye PQ = " );
       
       Destroy(_notCatchable); // TODO : un destroy qui fonctionne  

    }

    // private IEnumerator delay()
    // {
    //     yield return new WaitForSeconds(time_delay);
    //     //ejection de l'objet 
    //     byebye_object();
    // }
}
