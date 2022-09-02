using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previous_room;
    [SerializeField] private Transform next_room;
    [SerializeField] private camera_movement cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.move_to_new_room(next_room);
            else
                cam.move_to_new_room(previous_room);
        }
    }
}
