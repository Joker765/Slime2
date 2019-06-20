using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        if (collider.tag == "Player")
        {
           // collider.GetComponent<PlayerMove>().climb = true;
            collider.GetComponent<PlayerMove>().climbY = collider.transform.position.y+0.3f;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        collider.GetComponent<PlayerMove>().climb = true;
        //collider.GetComponent<PlayerMove>().climbY = collider.transform.position.y;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerMove>().climb = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
