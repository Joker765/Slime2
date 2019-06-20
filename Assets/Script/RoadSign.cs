using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadSign : MonoBehaviour
{
    public int sceneId=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        print("zjk???");
        if (collider.tag == "Player")
        {
            SceneManager.LoadScene(sceneId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
