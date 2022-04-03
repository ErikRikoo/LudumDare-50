using System.Collections;
using System.Collections.Generic;
using Environnement;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Eject_object : MonoBehaviour,IEjectable
{
    
    [SerializeField] private float P_canon = 200;
    [SerializeField] private ColliderEvent TakenByCanon;
    [SerializeField] private float time_delay = 0;
    
    
    public void OnPutIn(Canon _canon,Transform lanceur)
    {
        TakenByCanon.Raise(GetComponent<Collider>());

        // l'objet va en position lanceur 
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.parent = lanceur;
        transform.localPosition = Vector3.zero;
        transform.forward = lanceur.up; 
        Debug.Log("pret à être envoyé!!!!" + transform.name + " = "+transform.forward + "= ?  " + lanceur.name + " = " +lanceur.up);
        // go animation
        StartCoroutine(delay());

    }

    public void OnPuOut(Canon _canon)
    {
        //DO NOTHING
    }
    private void byebye_object()
    {
        transform.parent= null;
        transform.GetComponent<Rigidbody>().useGravity = true;
       transform.GetComponent<Rigidbody>().AddForce((transform.forward) * P_canon);
       Debug.Log("bye bye PQ = " );
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(time_delay);
        //ejection de l'objet 
        byebye_object();
    }
}
