using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBox : MonoBehaviour
{
    private float timer=-1f;

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.tag == "Player")
       {
            timer = 0f;
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0) timer += Time.deltaTime;
        if (timer > 0.8f) GetComponent<Rigidbody2D>().bodyType = 0;
    }
    
}
