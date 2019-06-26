using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitenemy1Move : MonoBehaviour
{
    private GameObject player;//史莱姆
    //攻击变量
    public float hitPower=0.5f;//攻击力
    public float hp=100;//血量



    //移动变量
    public bool checkR0;//水平检测
    public bool checkL0;
    public bool checkR;//检测右边边界有路
    public bool checkL;//检测左边边界有路
   
    private Collider2D coll;
    public bool isWalk = true;//是否可行走 如果史莱姆在前面则停
    public Animator ani;//状态机
    public bool rightMove=true;//向右可走 false为向左可走
    public Vector2 targetPosition;//enemy1的位置
    public Rigidbody2D rigEnemy1;
    public float speed;//敌人移动速度

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coll = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        rigEnemy1 = GetComponent<Rigidbody2D>();
        targetPosition = this.transform.position;//得到初始位置
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(0.4327073f, 0.4977126f, 1);
        //  TcheckL0();
        // TcheckR0();
        CheckLef0();
       CheckRig0();
       CheckRig();
        CheckLef();
        Axxx();
        die();
    }
    //32 32
    public void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {

        if (isWalk)
        {
            

            if (rightMove == true&&checkR==true)
            {
                CheckRig();
                //canRight();
                //向右走
                transform.localScale = new Vector3(-1f, 1f, 1);
               if (this.transform.tag == "dragon")
              {
                   transform.localScale = new Vector3(1f, 1f, 1);
                }
                MoveRight();
                if (!checkR0) rightMove = false;
            

            }
            else if(rightMove==false&&checkL==true)
            {


                CheckLef();
                transform.localScale = new Vector3(1f, 1f, 1);
                if (this.transform.tag == "dragon")
               {
                    transform.localScale = new Vector3(-1f, 1f, 1);
                }
                MoveLeft();
                
                if (!checkL0) rightMove = true;
            }
        }

    }
    public void MoveRight()
    {
        //   rigEnemy1.velocity = new Vector2(1,0);
        //向右移动
            rigEnemy1.MovePosition(Vector2.Lerp(transform.position, transform.position+new Vector3(1f,0,0), speed * Time.deltaTime));       
    }
    public void MoveLeft()
    {

        // rigEnemy1.velocity = new Vector2(-    1, 0);
        //向左移动 同时检测 左边界是否存在
       
        rigEnemy1.MovePosition(Vector2.Lerp(transform.position, transform.position+new Vector3(-1f,0,0), speed * Time.deltaTime));
    }
    public void Attack()
    {
        //攻击
        if (Input.GetKeyDown(KeyCode.K))
        {
            ani.SetTrigger("Attack");
        }
    }
    //检测射线 test用
    public void Check()
    {
        Vector3 right = new Vector3(1f,-1f,0);
        Vector3 left = new Vector3(-1f, -1f,0);
        coll.enabled = false;
        //hit = Physics2D.Linecast(transform.position, transform.position + right);
        // hit2= Physics2D.Linecast(transform.position, transform.position +left);
   
      //  coll.enabled = true;
        // print("length:"+hit.distance+"//"+hit2.distance);
        //print(hit.transform.position);
        //print("right "+hit.transform.tag);
        // print("left "+hit2.transform.tag);

        int hitNumber;
        int hitNumber1;
        RaycastHit2D[] ray=new RaycastHit2D[5];
        RaycastHit2D[] ray1 = new RaycastHit2D[5];
        hitNumber = Physics2D.LinecastNonAlloc(transform.position, transform.position + right, ray, 1 << LayerMask.NameToLayer("Default"));
        hitNumber1 = Physics2D.LinecastNonAlloc(transform.position, transform.position + left, ray1, 1 << LayerMask.NameToLayer("Default"));
        coll.enabled = true;
        for (int i = 0; i < hitNumber; i++)
        {
          //  Debug.DrawLine(gameObject.transform.position, ray[i].transform.position, Color.red);
            print("right:ray{" + i + "}.name" + ray[i].transform.name);
        }
        for (int i = 0; i < hitNumber1; i++)
        {
          //  Debug.DrawLine(gameObject.transform.position, ray1[i].transform.position, Color.red);
            print("left:ray{" + i + "}.name" + ray1[i].transform.name);
        }


    }
    //45度检测
    public void CheckRig()
    {
        //  hit = Physics2D.Linecast(targetPosition, targetPosition + veCheck);
        Vector3 right = new Vector3(1f, -1f, 0);
        coll.enabled = false;
        int hitNumber;
        RaycastHit2D[] ray = new RaycastHit2D[1];
        hitNumber = Physics2D.LinecastNonAlloc(transform.position, transform.position + right, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        if (ray[0].transform != null)
        {
            if (ray[0].transform.tag != "ground") { checkR = false; rightMove = false; }
            else checkR = true;
        }
        else { checkR = false; rightMove = false; }

        }
    public void CheckLef()//检测左边 边界是否可走
    {
        Vector3 left = new Vector3(-1f, -1f, 0);
        coll.enabled = false;
        int hitNumber;
        RaycastHit2D[] ray = new RaycastHit2D[100];
        hitNumber = Physics2D.LinecastNonAlloc(transform.position, transform.position + left, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        if (ray[0].transform != null)
        {
            if (ray[0].transform.tag != "ground") { checkL = false; rightMove = true; }
            else checkL = true;
        }
        else { checkL = false; rightMove = true; }
        }

    //0度检测 
    public void CheckRig0()
    {
        Vector3 right = new Vector3(0.5f, 0f, 0);
        coll.enabled = false;
        int hitNumber;
        RaycastHit2D[] ray = new RaycastHit2D[1];
        hitNumber = Physics2D.LinecastNonAlloc(transform.position, transform.position + right, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        if (ray[0].transform == null)
        {
            checkR0 = true;
        }
        else checkR0 = false;
    }
    public void CheckLef0()
    {
        Vector3 left = new Vector3(-0.5f, 0f, 0);
        coll.enabled = false;
        int hitNumber;
        RaycastHit2D[] ray = new RaycastHit2D[1];
        hitNumber = Physics2D.LinecastNonAlloc(transform.position, transform.position + left, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        if (ray[0].transform == null )
        {
            checkL0 = true;
        }
        else checkL0 = false;
    }

  
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.transform.tag=="Enemy"&&collision.gameObject.tag == "Player")
        {
            isWalk = false;
            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1);
            }
            else transform.localScale = new Vector3(1f, 1f, 1);

            ani.SetTrigger("Attack");
            collision.transform.SendMessage("ChangeHp", -hitPower);
        }
        else if(this.transform.tag == "dragon" && collision.gameObject.tag == "Player")
        {//素材导致 龙怪物需要转向
            isWalk = false;
            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector3(1f, 1f, 1);
            }
            else transform.localScale = new Vector3(-1f, 1f, 1);

            ani.SetTrigger("Attack");
            collision.transform.SendMessage("ChangeHp",-hitPower);
        }
    }

    public void Axxx()
    {
        //替代 碰撞体退出函数 ？
        if (Mathf.Abs(player.transform.position.x - transform.position.x) >= 1&&isWalk==false)
        {
            isWalk = true;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        die();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "bullet")
        {
            TakeDamage(40);
            Destroy(other.gameObject);
        }
    }

    private void die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
 
}
