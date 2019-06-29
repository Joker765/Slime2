using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{ 
    private int collecters;
    private AudioSource audioSource;
    private AudioClip coin;
    private AudioClip get;
    private AudioClip getbottle;
    private AudioClip through;
    private AudioClip reborn;
    private AudioClip nextStage;

    private GameObject coinsNumber;
    private Rect rect;
    private Texture2D d;
    private Texture2D s;
    private Texture2D t;

    private  Sprite sprite;
    

    public GameObject tips;


    public static GameController _instance; 

    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
        {
         _instance = this;
        }
        coinsNumber = GameObject.Find("Coins");
        coin = Resources.Load("Music/coin") as AudioClip;
        get = Resources.Load("Music/get") as AudioClip;
        getbottle = Resources.Load("Music/getbottle") as AudioClip;
        reborn = Resources.Load("Music/reborn") as AudioClip;
        nextStage = Resources.Load("Music/nextStage") as AudioClip;
        audioSource = GetComponent<AudioSource>();
        collecters = 0;

        d = Resources.Load("doorTip") as Texture2D;
        s = Resources.Load("shieldTip") as Texture2D;
        t = Resources.Load("telescopeTip") as Texture2D;
        rect = new Rect(0, 0, 1446, 800);
    }

    public void addCollecter()
    {
        collecters++;
        audioSource.PlayOneShot(coin);
        coinsNumber.GetComponent<Text>().text = "coins: " + collecters;
    }

    public void Reborn()
    {
        audioSource.PlayOneShot(reborn);
    }

    public void GetKey()
    {
        audioSource.PlayOneShot(get);
    }

    public void NextStage()
    {
        audioSource.PlayOneShot(nextStage);
    }


    public void GetBottle()
    {
        audioSource.PlayOneShot(getbottle);
    }

    public void showTips(string name)
    {
        switch (name)
        {
            case "shield":
                audioSource.PlayOneShot(get);
                sprite = Sprite.Create(s, rect, new Vector2(0.5f, 0.5f));
                tips.GetComponent<Image>().sprite = sprite;
                tips.SetActive(true);
                break;
            case "door":
                audioSource.PlayOneShot(through);
                sprite = Sprite.Create(d, rect, new Vector2(0.5f, 0.5f));
                tips.GetComponent<Image>().sprite =sprite;
                tips.SetActive(true);
                break;
            case "telescope":
                audioSource.PlayOneShot(get);
                sprite = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));
                tips.GetComponent<Image>().sprite = sprite;
                tips.SetActive(true);
                break;
        }
    }


}
