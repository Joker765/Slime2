using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class EnterArch : MonoBehaviour
{
    public Dropdown d;
 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GetDropDownValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetDropDownValue()
    {
        start.sqlAccess.CloseSql();
        string x = d.options[d.value].text;
        string account= x.Split('_')[0];

        print(d.options[d.value].text);
        print(account + "/" + x);
        ArchieveFinal a = new ArchieveFinal();

        //
       // a.player = GameObject.FindGameObjectWithTag("Player");
         a.Read(account + "/" + x);
    }
   
}
