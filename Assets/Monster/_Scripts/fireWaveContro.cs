using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireWaveContro : MonoBehaviour
{
    public bool rightMove ;
    public GameObject player;
    private float power = 12.0f;//攻击力
    private float speed = 5.0f;//移动速度
    private float distance = 2.0f;//攻击移动距离
    private Rigidbody2D rigidbodyFire;
    private float lifetime = 2.0f;//生存时间

    
    // Start is called before the first frame update
    void Start()
    {

        rigidbodyFire = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Move();
    }
    private  void Move()
    {
        if (rightMove)
            rigidbodyFire.MovePosition(Vector2.Lerp(transform.position, transform.position + new Vector3(distance, 0, 0), speed * Time.deltaTime));
        else
        {
            rigidbodyFire.MovePosition(Vector2.Lerp(transform.position, transform.position - new Vector3(distance, 0, 0), speed * Time.deltaTime));
            this.transform.localScale = new Vector3(-1.0f, 1f, 1f);
        }
       Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            player.SendMessage("ChangeHp", -power);
            Destroy(gameObject);
        }
        Destroy(this);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.SendMessage("ChangeHp", -power);
            Destroy(gameObject);
        }
    }

    public void Right()
    {
        rightMove = true;
    }
    public void Left()
    {
        rightMove = false;
    }
}
    

