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
    private bool m_is_walking;
    private int m_remaining_frames_to_rotate = 0;
    private float m_delta_rotation;
    private float m_elapsed_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        StartWalk();
        UpdateDirection();
    }


    public void StartWalk() 
    {
        m_is_walking = true;
    }

    public void StopWalk() 
    {
        m_is_walking = false;
    }

    public bool IsWalking()
    {
        return m_is_walking;
    }

    public bool IsRotating()
    {
        return m_remaining_frames_to_rotate > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsWalking())
        {
            return;
        }

        //still perform the rotation
        if(IsRotating())
        {
            UpdateDirection();
            return;
        }

        // take a new direction
        m_elapsed_time += Time.deltaTime;
        if (direction_update_time < m_elapsed_time)
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeDirection();
    }

    void ChangeDirection()
    {
        int sens = Random.Range(0, 2) * 2 - 1;
        float angle = sens * Random.Range(min_rotation, max_rotation);
        m_delta_rotation = angle / (float)frames_to_rotate;
        m_remaining_frames_to_rotate = frames_to_rotate;
        m_elapsed_time = 0;
    }

    private void UpdateDirection() 
    {
        transform.Rotate(0, m_delta_rotation, 0, Space.World);
        m_rb.velocity = speed * transform.forward;
        m_remaining_frames_to_rotate -= 1;
    }
}
