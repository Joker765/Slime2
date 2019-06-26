using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigView : MonoBehaviour
{
    public float from=-17;
    public float to=17;

    private float t=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime/4;
        transform.position = new Vector3(Mathf.Lerp(from, to, t-0.2f), transform.position.y,-2);
        if (t > 1.5) Destroy(this.gameObject);
    }
}
