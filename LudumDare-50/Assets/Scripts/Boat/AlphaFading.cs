using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaFading : MonoBehaviour
{
    [SerializeField] GameObject target;
    [Range(0, 1)]
    [SerializeField] float fade_factor;

    private Material m_material;
    private Color m_old_color;

    void Start()
    {
        m_material = target.GetComponent<Renderer>().material;
    }
    void OnTriggerEnter(Collider other)
    {
        m_old_color = m_material.color;
        Color new_color = new Color(m_old_color.r, m_old_color.g, m_old_color.b, m_old_color.a * fade_factor);
        m_material.color = new_color;
    }

    void OnTriggerExit(Collider other)
    {
        m_material.color = m_old_color;
    }
}
