using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 实现功能：
 * 
 * 随机时间 随机方向 hitTime 发射一个冲击波
 * 
 * */
public class DragonAttack : MonoBehaviour
{
    public bool rightMove;
    public Hitenemy1Move hit1m;
    public GameObject mouseParent;//得到龙
    private float hitTime;
    private float hitTimer;//计时器
    public Animator ani;//状态机
    public GameObject fireWave;
    // Start is called before the first frame update
    void Start()
    {
       
          mouseParent = transform.parent.gameObject;//得到父对象
        hit1m = mouseParent.GetComponent<Hitenemy1Move>();//得到移动脚本
        //只有行走的时候才会 发射火焰
        InvokeRepeating("DelayHit", 1, 5);

        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        GetDire();
       // AttackWithBite();
    }
    public void AttackWithBite()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ani.SetTrigger("draBiteAttack");
           // yield return new WaitForSeconds(0f);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ani.SetTrigger("firePrepare");
          //  yield return new WaitForSeconds(0.5f);
            //发射火焰
            Vector2 mouse = transform.position;
            GameObject.Instantiate(fireWave,mouse,Quaternion.identity);

        }
    }

    private void DelayHit()
    {
        if(hit1m.isWalk)
        withRandomFire();


    }
    void withRandomFire()
    {
       
        //right dire
        Vector3 rightDire = new Vector3(1f, 0,0);
        Vector3 leftDire = new Vector3(-1, 0, 0);
  
        if (rightMove)
        {
            Vector2 mouse = transform.position;
            //往右发射
            fireWave.transform.GetComponent<fireWaveContro>().rightMove = true;
            hit1m.ani.SetTrigger("Attack");
            GameObject go=  GameObject.Instantiate(fireWave, mouse, Quaternion.identity) as GameObject;
            //发消息方法无法接收？？？
            // fireWave.transform.SendMessage("Right"); 
       
                
        }
        else
        {
            //往左发射
            Vector2 mouse = transform.position;
            //往右发射
            fireWave.transform.GetComponent<fireWaveContro>().rightMove = false;
            hit1m.ani.SetTrigger("Attack");
            GameObject go= GameObject.Instantiate(fireWave, mouse, Quaternion.identity) as GameObject;
      
       
            
        }
        
        
    }
    void GetDire()
    {
        //得到龙此时的前进方向 按照方向来发射火焰
        rightMove = hit1m.rightMove;
    }
   
}
