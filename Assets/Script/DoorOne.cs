using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOne : MonoBehaviour
{
    private int count=0;

    public Transform toDoor;
    public GameObject tips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerState>().key)
            {
                collision.transform.position = toDoor.position;
                Destroy(this);
            }
            else
            {
                print(count++);
                tips.SetActive(true);
            }
        }

    }
}
