using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag== "Player")
        {
             other.gameObject.GetComponent<PlayerMove>().ceiling = true;
      
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerMove>().ceiling = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
