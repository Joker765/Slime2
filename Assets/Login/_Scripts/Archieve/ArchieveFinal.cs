using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;
public class ArchieveFinal : MonoBehaviour
{
    public List<GameObject> allGameObject;
    //游戏存档对象
    public GameObject[] gameObjects;//存放游戏中所有对象
    public AllItems data;
    public Button saveGame;
    //存档相关
    public string dire;//文件夹目录
    public static string account ;//账号
    public string path;
    public float count = 0;

    // Start is called before the first frame update
    void Start()
    {   //  saveGame.onClick.AddListener(Save);
        //readGame.onClick.AddListener(Read);
        data = new AllItems();
        allGameObject = new List<GameObject>();
        path = "./" + account + "/" + account + "_" + count + "_" + "data.json";
        saveGame.onClick.AddListener(SaveGame);

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("开始读档");
            Read();
        }
    }
    //save按钮响应事件
    public void SaveGame()
    {
        Clear();
        print("开始存档");
        Save();
        print("存档完成");
    }
    public void Save()
    {
        GetData();
        string json = JsonMapper.ToJson(data);
        print(json);
        //创建目录
        CreatDir(account);

          //检测当前文件是否存在
        while (File.Exists(path))
           {
               count++;
               path = "./"+account+"/"+account + "_" + count + "_" + "data.json";
           }
           
         if (!File.Exists(path))
           {

               File.Create(path).Close();


           }
      //  FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);//创建写入文件  

        print(Path.GetFullPath(path)+"    "         );

        StreamWriter sw = new StreamWriter(path);
        sw.Write(json);
        sw.Close();

      
    }
    public void Read()
    {
        int scene;
        if (!File.Exists(path))
        {
            print("文件不存在");

        }

        StreamReader sr = new StreamReader(path);

        string json = sr.ReadToEnd();
        print(json + 123);
        data = JsonMapper.ToObject<AllItems>(json);
        sr.Close();
        print("读档完成");
        scene = LoadArch();
        SceneManager.LoadScene(scene);
        print("加载完成");
    }
  public void GetData()
    {

        gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject x in gameObjects)
        {
            data.items.Add(new Item(x.transform.name, x.transform.tag, x.transform.position.x, x.transform.position.y, x.transform.position.z,x.GetComponent<PlayerState>().Hp, x.GetComponent<PlayerState>().Mp,GameController._instance.getCollecter()));
        }
        data.items.Add(new Item("Scene", SceneManager.GetActiveScene().buildIndex));
         gameObjects = GameObject.FindGameObjectsWithTag("Coin");
         foreach (GameObject x in gameObjects)
         {
            data.items.Add(new Item(x.transform.name, x.transform.tag, x.transform.position.x, x.transform.position.y, x.transform.position.z));
         }

    }
    public int LoadArch()
    {
        string name;
        Vector3 v3 = Vector3.zero;
        List<Vector3> coins = new List<Vector3>();
        int scene = 0,collecters = 0;
        float hp = 0, mp = 0;
        //将data中的object存入
        foreach (Item i in data.items)
        {
            if (i.tag == "Player")
            {
                name = i.name;
                v3 = new Vector3((float)i.x, (float)i.y, (float)i.z);
                hp = (float)i.Hp;
                mp = (float)i.Mp;
                collecters = i.collectors;
            }
            if (i.tag == "Scene")
                scene = i.scene;
            if(i.tag == "Coin")
            {
                coins.Add(new Vector3((float)i.x, (float)i.y, (float)i.z));
            }
            DataOfPlayer.getInstance(hp, mp, v3,collecters, coins, true);
        }
        return scene;
    }
    public void Clear()
    {
        //    File.Delete(path);
    }


    public void CreatDir(string path)
    {
        if (false == System.IO.Directory.Exists(path))
        {
            //创建pic文件夹
            System.IO.Directory.CreateDirectory(path);
        }
    }

    public void Read(string x)
    {
        int scene;
        if (!File.Exists(x))
        {
            print("文件不存在");

        }

        StreamReader sr = new StreamReader(x);

        string json = sr.ReadToEnd();
        print(json + 123);
        data = JsonMapper.ToObject<AllItems>(json);
        sr.Close();
        print("du档完成");
        scene = LoadArch();
        SceneManager.LoadScene(scene);
        print("加载完成");
    }

}
