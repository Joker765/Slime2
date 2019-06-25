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
            collider.transform.position = birthPlace.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
