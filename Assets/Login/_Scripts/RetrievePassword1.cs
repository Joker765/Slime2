using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievePassword1 : MonoBehaviour
{
    public Transform login; //登录界面
    public Transform next; //下一步界面
    public Button backButton; //返回按钮
    public Button confirmButton; //下一步按钮
    public Text confirmAccount; //确认账户提示信息
    public InputField account; //账号输入框
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(Back);
        confirmButton.onClick.AddListener(nextStep);
        confirmAccount.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Back()
    {
        this.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
        confirmAccount.text = "";
        account.text = "";
    }
    private void nextStep()
    {
        string str = account.text;
        if(str == "")
        {
            confirmAccount.text = "账户不能为空";
        }
        else 
        {
            if (start.sqlAccess.selectByUser(str, start.tableName) == null)
                confirmAccount.text = "账号不存在";
            else
            {
                //传用户名到下一步
                confirmAccount.text = "";
                RetrievePassword2 retrievePassword2 = next.gameObject.GetComponent<RetrievePassword2>();
                retrievePassword2.getAnswer(account.text);
                this.gameObject.SetActive(false);
                next.gameObject.SetActive(true);
                account.text = "";
            }
        }
    }
}
