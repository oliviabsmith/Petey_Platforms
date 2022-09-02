using UnityEngine;

public class camera_movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float current_pos_x;
    private Vector3 velocity = Vector3.zero;

    //follow player
    [SerializeField] private Transform player;
    [SerializeField] private float ahead_distance;
    [SerializeField] private float camera_speed;
    private float lookAhead;

    private void Update()
    {
        //Room Camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(current_pos_x, transform.position.y, transform.position.z),ref velocity, speed);

        //Follow Player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (ahead_distance * player.localScale.x), Time.deltaTime * camera_speed);
    }

    public void move_to_new_room(Transform _new_room)
    {
        current_pos_x = _new_room.position.x;
    }
}
