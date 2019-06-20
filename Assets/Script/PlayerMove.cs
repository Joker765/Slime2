using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool climb;
    public float climbY;

    private int jump;
    private float jumpTime;


    // Start is called before the first frame update
    void Start()
    {
        climb = false;
        climbY = 0f;
        jumpTime = 0f;
        jump = 2;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        print("zjk");
        if (other.gameObject.tag == "ground")
        {
            jump = 2;
        }
    }

    private void Climb()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(this.transform.position.x, climbY);
          //  GetComponent<Rigidbody2D>().constraints.freezePosition.y = true;
        }
    }


    // Update is called once per frame
    private void Update()
    {
        Vector3 acc = Vector3.zero;     //零向量
        Vector3 diff;
        if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(0.19f, 0.19f, 1);
            acc.x = -0.1f;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(-0.19f, 0.19f, 1);
            acc.x = 0.1f;
        }
        diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
        transform.localPosition = diff;


        if (climb) Climb();
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
