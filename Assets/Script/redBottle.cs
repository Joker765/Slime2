using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBottle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerState>().ChangeHp(40);
            GameController._instance.GetBottle();
            Destroy(this.gameObject);

        }
    }
}
