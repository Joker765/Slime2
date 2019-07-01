using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopBG : MonoBehaviour
{
    private Object bg;
    private GameObject[] loop= new GameObject[2];
    private int id;

    // Start is called before the first frame update
    void Awake()
    {
        id = 0;
        bg = Resources.Load("menuBg");
        loop[0]= GameObject.Instantiate(bg,new Vector2(-8,0.1f),Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (loop[id].transform.position.x > 30.3f)
        {
            Destroy(loop[id]);
            id = 1 - id;
        }

        loop[id].transform.Translate(new Vector2(0.02f, 0));
        if (loop[id].transform.position.x > 8)
        {
            if(loop[1-id]==null)
            loop[1 - id] = GameObject.Instantiate(bg, new Vector2(-30.39f + loop[id].transform.position.x - 8, 0.1f), Quaternion.identity) as GameObject;
            else
            loop[1 - id].transform.Translate(new Vector2(0.02f, 0));
        }
    }

    public void LoadLogin()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
