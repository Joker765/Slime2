using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    private int damage; //伤害值
    private float moveSpeed; //移动的速度
    public float direction; //移动方向
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        moveSpeed = 3.5f;
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine("destorySelf");
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;
        if (speed == 0)
        {
            speed = moveSpeed * direction;
            Vector2 move = new Vector2(speed, 0);
            rig.velocity = move;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player受伤
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            PlayerState playerState = collision.gameObject.GetComponent<PlayerState>();
            playerState.ChangeHp(-damage);
        }
        if (collision.transform.tag == "ground")
        {
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator destorySelf()
    {
        yield return new WaitForSeconds(15.0f);
        Destroy(gameObject);
    }
}
