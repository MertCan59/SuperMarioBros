using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    private bool shelled;
    private bool pushed;
    public float shellSpeed = 12f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower)
            {
                Hit();
            }else if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }else
            {
                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision2)
    {
        if (shelled &&collision2.CompareTag("Player"))
        {
            if(!pushed)
            {
                Vector2 direction=new Vector2(transform.position.x-collision2.transform.position.x,0f);
                PushShell(direction);
            }
            else
            {
                Player player=collision2.gameObject.GetComponent<Player>();

                if (player.starpower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
               
            }
        }
        else if (!shelled && collision2.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        
    }
    private void PushShell(Vector2 direction)
    {
        pushed = true;  
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement=GetComponent<EntityMovement>(); 
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled= true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
}
