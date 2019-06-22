using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public Transform birthPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.position = birthPlace.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
