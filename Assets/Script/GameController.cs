using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject coins;
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
        collecters = Coins.StageCoinsNum;
        coinsNumber.GetComponent<Text>().text = "coins: " + collecters;


        d = Resources.Load("doorTip") as Texture2D;
        s = Resources.Load("shieldTip") as Texture2D;
        t = Resources.Load("telescopeTip") as Texture2D;
        rect = new Rect(0, 0, 1446, 800);
        archieveEnter();
    }

    public void addCollecter()
    {
        collecters++;
        audioSource.PlayOneShot(coin);
        coinsNumber.GetComponent<Text>().text = "coins: " + collecters;
    }

    public int getCollecter()
    {
        return collecters;
    }
    public void setCollecter(int col)
    {
        collecters = col;
        coinsNumber.GetComponent<Text>().text = "coins: " + collecters;
    }

    private void archieveEnter()
    {
        DataOfPlayer dataOfPlayer = DataOfPlayer.getInstance(0, 0, Vector3.zero, 0, null, false);
        if (dataOfPlayer != null && dataOfPlayer.isArchieve)
        {
            //设置人物位置
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerState>().Hp = dataOfPlayer.playerData.Hp;
            player.GetComponent<PlayerState>().Mp = dataOfPlayer.playerData.Mp;
            player.transform.position = dataOfPlayer.playerData.playerPosition;
            setCollecter(dataOfPlayer.playerData.collectors);//设置硬币已经得到数
            //重新设置硬币
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject i in gameObjects)
                Destroy(i);
            foreach (Vector3 i in dataOfPlayer.coins)
            {
                Instantiate(coins, i, Quaternion.identity);
            }
            dataOfPlayer.setArchieve(false);
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
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
        Coins.StageCoinsNum = collecters;
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
