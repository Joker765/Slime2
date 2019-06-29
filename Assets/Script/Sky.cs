using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private GameObject birthPlace;

    // Start is called before the first frame update
    void Start()
    {
        birthPlace = GameObject.Find("BirthPlace");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameController._instance.Reborn();
            collider.transform.position = birthPlace.transform.position;
        }
       else if (collider.tag == "bullet")
        {
            Destroy(collider.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
