using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{ 
    public GameObject tips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerState>().shield = true;
            tips.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
