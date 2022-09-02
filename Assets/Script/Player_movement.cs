using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float jump_cooldown;
    private float horizontalInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    //update the player per frame rate. Vector xyz (-1 to 1)
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.freezeRotation = true;

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput > -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);       //flip the sprite left and right



        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", is_grounded());

        if (jump_cooldown > 0.2f)
        {
            
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            
            if (on_wall() && is_grounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                //body.velocity = new Vector2(body.velocity.x, speed);
                jump();
        }
        else
            jump_cooldown += Time.deltaTime;
            
    }
    private void jump()
    {
        if (is_grounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(is_grounded() && on_wall())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,transform.localScale.z);
            }
            else
            
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3 ,6);
            jump_cooldown = 0;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool is_grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);

        return raycastHit.collider != null;
    }
    private bool on_wall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);

        return raycastHit.collider != null;
    }

}
