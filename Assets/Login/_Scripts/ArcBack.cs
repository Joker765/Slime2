using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcBack : MonoBehaviour
{
    public Transform pan;
    public Button ret;
    public Button enterArc;
    public Button newArc;
    // Start is called before the first frame update
    void Start()
    {
        ret.onClick.AddListener(Clicked);
        newArc.onClick.AddListener(gameStart);
    }


    void Clicked()
    {
        this.gameObject.SetActive(false);
        pan.gameObject.SetActive(true);
    }
    private void gameStart()
    {
        start.sqlAccess.CloseSql();
        SceneManager.LoadScene(2);
        
    
    }
}