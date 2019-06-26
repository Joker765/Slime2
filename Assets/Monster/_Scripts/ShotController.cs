using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private int damage; //伤害值
    private float speed; //追踪的速度
    private Rigidbody2D rig;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        speed = 2.0f;
        StartCoroutine("destorySelf");
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rig.MovePosition(Vector2.Lerp(transform.position,player.transform.position,speed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player受伤
        if (collision.transform.tag=="Player")
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
    // 10秒后摧毁自身
    IEnumerator destorySelf()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
