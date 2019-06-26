using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator animator;
    private BoxCollider2D coll;
    private GameObject player;
    public GameObject shot;
    public GameObject beam;
    private int  hp;  //血量
    public float direction; //对象面朝方向，朝右为1，左为-1
    private  float moveSpeed;  //移动速度
    private float attackPause;
    private float attackTime; //攻击间隔
    private float attackTimes;  //攻击次数
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        direction = 1.0f;
        moveSpeed = 2.0f; //移动速度
        attackTime = 4.0f;//攻击间隔
        attackTimes = 0;
        attackPause = 0;
        hp = 80;
    }

    // Update is called once per frame
    void Update()
    {
        death();
        attackPause += Time.deltaTime;
        if (!isAttack())
        {
            changeDirection("ground");
            rig.MovePosition(Vector2.Lerp(transform.position, transform.position + new Vector3(direction, 0, 0),moveSpeed * Time.deltaTime));
            transform.localScale = new Vector3(-direction, 1, 1);
        }
        else 
        {
            if (attackPause > attackTime)
            {
                attackTimes += 1;
                //发射子弹
                if (attackTimes <= 2)
                {
                    animator.SetBool("Attack", true);
                    StartCoroutine("beamAttack");
                }
                //发射追踪弹
                else
                {
                    animator.SetBool("LightingAttack", true);
                    StartCoroutine("shotAttack");
                }
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
        foreach (RaycastHit2D i in ray)
        {
            if (i.transform != null && i.collider.tag == ("ground"))
                return false;
        }
        return true;
    }
    //检测水平面的墙体
    private bool checkWall()
    {
        Vector3 angle = new Vector3(direction*Mathf.Sqrt(3), -1f, 0);
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
    private void changeDirection(string thing)
    {
        if (thing == "ground")
        {
            if (checkGround() || checkWall())
                direction *= -1;
        }
        else if(thing == "player")
        {
            float tmp = player.transform.position.x - transform.position.x;
            if(tmp*direction<0)
                direction *= -1;
        }
    }
    //死亡函数
    private void death()
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.IsName("GunnerDeath") && (stateinfo.normalizedTime > 1.0f))
        {
            Destroy(gameObject);
        }
    }
    //受伤函数
    private void Damage(int hurt)
    {
        hp -= hurt;
        if(hp<=0)
        {
            animator.SetBool("Death", true);
        }
    }
    //检测在半径为8的圆内是否存在player
    private bool isAttack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 8.0f);
        if (collider.Length == 0)
        {
            return false;
        }
        foreach (Collider2D col in collider)
        {
            if (col.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    //追踪弹攻击
    IEnumerator shotAttack()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        changeDirection("player");
        transform.localScale = new Vector3(-direction, 1, 1);
        GameObject.Instantiate(shot, transform.position + new Vector3(direction * 1.7f, 0.78f, 0), Quaternion.identity);
        attackTimes = 0;
    }
    //普通攻击
    IEnumerator beamAttack()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameObject go = GameObject.Instantiate(beam, transform.position + new Vector3(direction * 1.9f, 0.83f, 0), Quaternion.identity) as GameObject;
        BeamController beamController = go.GetComponent<BeamController>();
        beamController.direction = direction;
    }
}

