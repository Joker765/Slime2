using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PwdShow : MonoBehaviour
{
    public Toggle togg;
    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        togg.onValueChanged.AddListener(ToggleClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleClick(bool isShow)
    {
        input.contentType = isShow ? InputField.ContentType.Standard : InputField.ContentType.Password;
        input.Select();
    }
}
