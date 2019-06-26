using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float bottomIn=0;
    public float topIn=1;
    public float speed =1f;

    private bool up = true;
    private Vector2 top;
    private Vector2 bottom;


    void Awake()
    {
        top = new Vector2(this.transform.position.x, topIn);
        bottom = new Vector2(this.transform.position.x, bottomIn);
    }

    void Update()
    {
        if(up)
        transform.Translate((top-bottom).normalized*speed*Time.deltaTime);
        else transform.Translate((bottom-top).normalized * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, top) < 0.1f) up = false;
        else if (Vector2.Distance(transform.position, bottom) < 0.1f) up = true;
    }
}
