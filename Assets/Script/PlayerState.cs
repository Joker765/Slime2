using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerState : MonoBehaviour
{
    private float Hp;
    private float Mp;
    private float telescopeTime;
    private GameObject bullet;
    private SpriteRenderer spriteRenderer;
    private bool shieldOn;
    private GameObject viewField;

    public bool key;
    public bool shield;
    public bool telescope;
    public Slider HpSlider;
    public Slider MpSlider;
    public GameObject leftShield;
    public GameObject rightShield;

    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load("bullet") as GameObject;
        viewField = GameObject.Find("CM vcam1");
        spriteRenderer = GetComponent<SpriteRenderer>();
        telescopeTime = 0f;
        Hp = 100;
        Mp = 100;
        key = false;
        shield = false;
        shieldOn = false;
        telescope = false;
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

        if(telescope&& Input.GetKeyDown(KeyCode.T))
        {
            telescope = false;
            viewField.GetComponent<CinemachineVirtualCamera>().m_Lens = new LensSettings(40f, 9f, 0.1f, 5000f, 0, false, 1);
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
        if (Hp < 0) Hp = 0;
    }

    public void ChangeMp(float change)
    {
          Mp += change;
    }

    private void Fire()
    {
        if (spriteRenderer.flipX == false)
            GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
        else
        GameObject.Instantiate(bullet,this.transform.position,this.transform.rotation);
        
    }
}
