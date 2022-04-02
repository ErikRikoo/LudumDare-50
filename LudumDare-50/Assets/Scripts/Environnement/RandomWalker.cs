using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalker : MonoBehaviour
{

    [Range (0f, 5f)]
    [SerializeField] float speed;
    [Range(0, 360)]
    [SerializeField] float min_rotation;
    [Range(0, 360)]
    [SerializeField] float max_rotation;

    [SerializeField] int frames_to_rotate;
    [SerializeField] int direction_update_time;

    private Rigidbody m_rb;
    private int m_frame_count = 0;
    private float m_delta_rot;
    private float m_elapsed_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_frame_count < frames_to_rotate)
        {
            m_frame_count += 1;
            UpdateDirection();
            return;
        }

        m_elapsed_time += Time.deltaTime;
        if (direction_update_time < m_elapsed_time)
        {
            ChangeDirection();
            m_elapsed_time = 0;
        }
    }

    void ChangeDirection()
    {
        float angle = Random.Range(-max_rotation, max_rotation);
        m_delta_rot = angle / (float)frames_to_rotate;
        m_frame_count = 0;
    }

    private void UpdateDirection() 
    {
        transform.Rotate(0, m_delta_rot, 0, Space.World);
        m_rb.velocity = speed * transform.forward;
    }
}
