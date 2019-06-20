using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestroy : MonoBehaviour
{
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3f) this.gameObject.SetActive(false);
    }
}
