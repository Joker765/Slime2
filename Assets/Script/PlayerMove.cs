using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool ceiling;

    private bool climb;
    private int jump;
    private float jumpTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ceiling = false;
        climb = false;
        jumpTime = 0f;
        jump = 2;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            jump = 2;
        }
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            climb = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;

        }
        else
        {
            climb = false;
            GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

  
    // Update is called once per frame
    private void Update()
    {
        Vector3 acc = Vector3.zero;     //零向量
        if(climb) this.transform.localScale = new Vector3(0.19f, -0.19f, 1);
        else this.transform.localScale = new Vector3(0.19f, 0.19f, 1);

        //   Vector2 move = Vector2.zero;
        //  move.x = Input.GetAxis("Horizontal");

        float velocityX =0f;

        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            velocityX = 1;
            acc.x = -0.1f;
            if (climb) this.transform.localScale = new Vector3(0.21f, -0.21f, 1);
            else this.transform.localScale = new Vector3(0.21f, 0.21f, 1);
            if (spriteRenderer.flipX == true)  spriteRenderer.flipX = false;
            
        } else
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            velocityX = 1;
            acc.x = 0.1f;
            if (climb) this.transform.localScale = new Vector3(0.21f, -0.21f, 1);
            else this.transform.localScale = new Vector3(0.21f, 0.21f, 1);
            if (spriteRenderer.flipX == false)  spriteRenderer.flipX = true;  
        }

        Vector3 diff;
        diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
        transform.localPosition = diff;

        animator.SetFloat("velocityX", velocityX);

        if (ceiling) Climb();
        else
        {

            if (jump == 0)
            {
                jumpTime += Time.deltaTime;
                if (jumpTime > 4f) jump = 2;
            }
            else  jumpTime = 0f;            

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                if (jump > 0)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
                    jump--;
                }
            
        }

    }

}
