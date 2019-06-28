using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOne : MonoBehaviour
{
    private AudioSource audioSource;

    public Transform toDoor;
    public float angleSpeed=10f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.forward, Time.deltaTime * angleSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerState>().key)
            {
                audioSource.Play();
                collision.transform.position = toDoor.position;
            }
            else
            {
                GameController._instance.showTips("door");
            }
        }

    }
}
