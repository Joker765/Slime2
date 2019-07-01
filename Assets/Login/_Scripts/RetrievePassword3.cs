using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievePassword3 : MonoBehaviour
{
    public Transform back; //上一步界面
    public Transform login; //登录界面
    public Text account;  //账户文本
    public Text confirmText; //两次密码输入提示信息
    public InputField password;//密码输入框
    public InputField confirmPassword; //确认密码输入框 
    public Button backButton; //返回按钮
    public Button confirmButton; //完成按钮
    // Start is called before the first frame update
    void Start()
    {
        confirmText.text = "";
        backButton.onClick.AddListener(Back);
        confirmButton.onClick.AddListener(updatePassword);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //返回按钮响应时间
    private void Back()
    {
        confirmText.text = "";
        password.text = "";
        confirmPassword.text = "";
        this.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }
    //完成按钮响应事件
    private void updatePassword()
    {
        //修改密码
        confirmText.text = "";
        if (password.text != confirmPassword.text)
            confirmText.text = "两次密码不一样";
        else if(password.text == "")
            confirmText.text = "密码不可为空";
        else
        {
            if(start.sqlAccess.updateByUser(account.text, password.text, "password", start.tableName))
            {
                this.gameObject.SetActive(false);
                login.gameObject.SetActive(true);
            }
            else
                confirmText.text = "修改失败，请重试";
        }
    }
    public void setAccount(string aaccount)
    {
        account.text = aaccount;
    }
}
