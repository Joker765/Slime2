using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("zjkkkk");
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerState>().key = true;
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
