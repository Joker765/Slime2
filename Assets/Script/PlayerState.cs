﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private float Hp;
    private float Mp;
    public GameObject fist;

    public bool key;
    public Slider HpSlider;
    public Slider MpSlider;

    // Start is called before the first frame update
    void Start()
    {
       
        Hp = 80;
        Mp = 100;
        key = false;
    }

    // Update is called once per frame
    void Update()
    {
        HpSlider.value = Hp / 100;
        MpSlider.value = Mp / 100;

        Fire();
    }

    public void ChangeHp(int change)
    {
        Hp +=change;
    }


    private void Fire()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fist.SetActive(true);
        }
    }
}
