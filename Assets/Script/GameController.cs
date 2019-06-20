using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{ 
    private int collecters;

    public Text collecterNumber;

    public static GameController _instance; 

    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
        {
         _instance = this;
        }
       
        collecters = 0;
    }

    public void addCollecter()
    {
        collecters++;
        collecterNumber.text = "toys number: " + collecters;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
