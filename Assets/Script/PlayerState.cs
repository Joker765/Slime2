using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    private float deadTime;
    private float Hp;
    private float Mp;
    private float telescopeTime;
    private GameObject bullet;
    private SpriteRenderer spriteRenderer;
    private bool shieldOn;
    private GameObject viewField;
    private GameObject telescopeN;

    public bool key;
    public bool shield;
    public int telescope;
    public Slider HpSlider;
    public Slider MpSlider;
    public GameObject leftShield;
    public GameObject rightShield;

    public int sceneId=0;

    // Start is called before the first frame update
    void Start()
    {
        bullet = Resources.Load("bullet") as GameObject;
        viewField = GameObject.Find("CM vcam1");
        telescopeN = GameObject.Find("telescopeN");
        spriteRenderer = GetComponent<SpriteRenderer>();
        deadTime = 0;
        telescopeTime = 0;
        Hp = 100;
        Mp = 100;
        key = false;
        shield = false;
        shieldOn = false;
        telescope = 0;
    }


    private void Telescope()
    {
        if (telescope > 0 && Input.GetKeyDown(KeyCode.T))
        {
            telescope--;
            telescopeTime = 6f;
            telescopeN.GetComponent<Text>().text = "x " + telescope;
            viewField.GetComponent<CinemachineVirtualCamera>().m_Lens = new LensSettings(40f, 9f, 0.1f, 5000f, 0, false, 1);
        }
        else if (telescope > 0) telescopeN.GetComponent<Text>().text = "x " + telescope;

        if (telescopeTime > 0)
        {
            telescopeTime -= Time.deltaTime;
            if (telescopeTime <= 0) viewField.GetComponent<CinemachineVirtualCamera>().m_Lens = new LensSettings(40f, 6f, 0.1f, 5000f, 0, false, 1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        HpSlider.value = Hp / 100;
        MpSlider.value = Mp / 100;

        if (Hp == -100)
        {
            deadTime += Time.deltaTime;
            if (deadTime > 2f) PlayerDead();
        }

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

        Telescope();

        if (Input.GetKeyDown(KeyCode.Space) && !shieldOn)
            if (Mp >= 10)
            {
                Mp -= 10;
                Fire();
            }
    }

    public void ChangeHp(float change)
    {
        if (Hp == -100) return;
        if (shieldOn && change<0)
        {
           Hp += change / 4;
        }
        else  Hp +=change;
        if (Hp < 0)
        {
            Hp = -100;
            
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("dead");
        }
    }

    private void PlayerDead()
    {
        SceneManager.LoadScene(sceneId);
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
