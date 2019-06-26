using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator animator;
    private BoxCollider2D coll;
    private GameObject player;
    private int hp;  //血量
    private int damage; //伤害
    public float direction; //对象面朝方向，朝右为1，左为-1
    public float moveSpeed;  //移动速度
    private float runSpeed;  //奔跑速度
    private float realSpeed;
    private float attackPause;
    private float attackTime; //攻击间隔
    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        direction = 1.0f;
        moveSpeed = 3.0f; //移动速度
        runSpeed = 4.0f; //奔跑速度
        attackTime = 2.0f;//攻击间隔
        attackPause = 0;
        hp = 40;
        damage = 10;
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        death();
        attackPause += Time.deltaTime;
        if(!isAttack)
        {
            //移动
            changeDirection();
            isRun();
            rig.MovePosition(Vector2.Lerp(transform.position, transform.position + new Vector3(direction, 0, 0), realSpeed * Time.deltaTime));
            transform.localScale = new Vector3(direction, 1, 1);
        }
        else
        {
            //攻击player
            if (attackPause > attackTime)
            {
                animator.SetBool("Attack", true);
                StartCoroutine("attack");
                attackPause = 0;
            }
        }
    }
    //检测45度角的地面
    private bool checkGround()
    {
        Vector3 angle = new Vector3(direction, -1f, 0);
        coll.enabled = false;
        RaycastHit2D[] ray = new RaycastHit2D[5];
        Physics2D.LinecastNonAlloc(transform.position, transform.position + angle, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        foreach(RaycastHit2D i in ray)
        {
            if (i.transform != null && i.collider.tag == ("ground"))
                return false;
        }
        return true;
    }
    //检测水平面的墙体
    private bool checkWall()
    {
        Vector3 angle = new Vector3(direction * Mathf.Sqrt(3), -1f, 0);
        angle.Normalize();
        angle = angle * 1.5f;
        coll.enabled = false;
        RaycastHit2D[] ray = new RaycastHit2D[5];
        Physics2D.LinecastNonAlloc(transform.position, transform.position + angle, ray, 1 << LayerMask.NameToLayer("ground"));
        coll.enabled = true;
        foreach (RaycastHit2D i in ray)
        {
            if (i.transform != null && i.collider.tag == ("ground"))
                return true;
        }
        angle = new Vector3(direction * Mathf.Sqrt(3), 1f, 0);
        angle.Normalize();
        angle = angle * 1.5f;
        coll.enabled = false;
        Physics2D.LinecastNonAlloc(transform.position, transform.position + angle, ray, 1 << LayerMask.NameToLayer("Default"));
        coll.enabled = true;
        foreach (RaycastHit2D i in ray)
        {
            if (i.transform != null && i.collider.tag == ("ground"))
                return true;
        }
        return false;
    }
    //检测是否改变方向
    private void changeDirection()
    {
        if (checkGround() || checkWall())
            direction *= -1;       
    }
    private void isRun()
    {
        animator.SetBool("Attack", false);
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 8.0f);
        if(collider.Length==0)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
            realSpeed = moveSpeed;
        }
        foreach(Collider2D col in collider)
        {
            if(col.tag=="Player")
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
                realSpeed = runSpeed;
                return;
            }
        }
        animator.SetBool("Walk", true);
        animator.SetBool("Run", false);
        realSpeed = moveSpeed;
    }
    //死亡函数
    private void death()
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.IsName("ChomperDeath") && (stateinfo.normalizedTime > 1.0f))
        {
            Destroy(gameObject);
        }
    }
    //受伤函数
    private void Damage(int hurt)
    {
        hp -= hurt;
        if (hp <= 0)
        {
            animator.SetBool("Death", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && transform.localScale.x * collision.transform.localScale.x < 0)
        {
            isAttack = true;
        }
    }
    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.5f);
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2.3f)
        {
            PlayerState playerState = player.GetComponent<PlayerState>();
            playerState.ChangeHp(-damage);
        }
        isAttack = false;
    }
}
