using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private float Hp;
    private float Mp;
    private GameObject bullet;
    private SpriteRenderer spriteRenderer;
    private bool shieldOn;

    public bool key;
    public bool shield;
    public Slider HpSlider;
    public Slider MpSlider;
    public GameObject leftShield;
    public GameObject rightShield;

    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load("bullet") as GameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Hp = 100;
        Mp = 100;
        key = false;
        shield = false;
        shieldOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        HpSlider.value = Hp / 100;
        MpSlider.value = Mp / 100;

        if (shield && Input.GetKeyDown(KeyCode.E)) shieldOn = !shieldOn;

        if(shieldOn)
            if (spriteRenderer.flipX == false)
            {
                leftShield.SetActive(true);
                rightShield.SetActive(false);
            }
            else
            {
                rightShield.SetActive(true);
                leftShield.SetActive(false);
            }
        else
        {
            leftShield.SetActive(false);
            rightShield.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !shieldOn)
            if (Mp >= 10)
            {
                Mp -= 10;
                Fire();
            }
    }

    public void ChangeHp(float change)
    {
        if (shieldOn && change<0)
        {
           Hp += change / 4;
        }
        else  Hp +=change;
    }


    private void Fire()
    {
        if (spriteRenderer.flipX == false)
            GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
        else
        GameObject.Instantiate(bullet,this.transform.position,this.transform.rotation);
        
    }
}
