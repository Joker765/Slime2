using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerState>().shield = true;
            GameController._instance.showTips("shield");
            Destroy(this.gameObject);
        }

    }
}
