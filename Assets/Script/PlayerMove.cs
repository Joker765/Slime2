using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool ceiling;

    private Vector2 lastPlace;
    private bool climb;
    private int jump;
    private float jumpTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rg;

    RaycastHit2D hit1;
    RaycastHit2D hit2;

    void Start()
    {
        ceiling = false;
        climb = false;

        lastPlace = GameObject.Find("BirthPlace").transform.position;
        jumpTime = 0f;
        jump = 2;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rg = GetComponent<Rigidbody2D>();

        hit1 = Physics2D.Raycast(lastPlace + new Vector2(-0.3f, 0), new Vector2(0, -1), 0.5f, 1 << 8 | 1 << 9);
        hit2 = Physics2D.Raycast(lastPlace+ new Vector2(0.3f, 0), new Vector2(0, -1), 0.5f, 1 << 8 | 1 << 9);

    }

   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.gameObject.tag == "ground")
        {
            if (hit1.transform != null || hit2.transform != null)
            jump = 2;
        }
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            climb = true;
            rg.gravityScale = 0;

        }
        else
        {
            climb = false;
            rg.gravityScale = 2;
        }
    }

    private void FixedUpdate()
    {
        Vector3 acc = Vector3.zero;     //零向量

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            acc.x = -0.1f;
            if (climb) this.transform.localScale = new Vector3(0.21f, -0.21f, 1);
            else this.transform.localScale = new Vector3(0.21f, 0.21f, 1);
            if (spriteRenderer.flipX == true) spriteRenderer.flipX = false;
        }
        else
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            acc.x = 0.1f;
            if (climb) this.transform.localScale = new Vector3(0.21f, -0.21f, 1);
            else this.transform.localScale = new Vector3(0.21f, 0.21f, 1);
            if (spriteRenderer.flipX == false) spriteRenderer.flipX = true;
        }

        Vector3 diff;
        diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
        transform.localPosition = diff;

        animator.SetFloat("velocityX", Mathf.Abs(acc.x));

        if (ceiling) Climb();
        else
        {
            Jump();
            climb = false;
            rg.gravityScale = 2;
        }

        if (climb) this.transform.localScale = new Vector3(0.19f, -0.19f, 1);
        else this.transform.localScale = new Vector3(0.19f, 0.19f, 1);

        Vector2 Pos = this.transform.position;

        hit1 = Physics2D.Raycast(Pos + new Vector2(-0.3f, 0), new Vector2(0, -1), 0.5f, 1 << 8 | 1<<9 );
        Debug.DrawRay(Pos + new Vector2(-0.3f, 0), new Vector2(0, -0.5f), Color.red);
        hit2 = Physics2D.Raycast(Pos + new Vector2(0.3f, 0), new Vector2(0, -1), 0.5f, 1 << 8 | 1 << 9);
        Debug.DrawRay(Pos + new Vector2(0.3f, 0), new Vector2(0, -0.5f), Color.red);
        if (hit1.transform != null || hit2.transform != null)
        {
            animator.SetBool("jump", false);
        }
        else 
        {
            if (this.transform.position.y - lastPlace.y > 0.01f)
                animator.SetBool("jump", true);    
            else animator.SetBool("jump", false);
        }
        lastPlace = this.transform.position;
    }


    private void Jump()
    {
            if (jump == 0)
            {
                jumpTime += Time.deltaTime;
                if (jumpTime > 3f) jump = 2;
            }
            else jumpTime = 0f;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                if (jump > 0)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
                    jump--;
                }
    }
    
}
