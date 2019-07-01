using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginButton : MonoBehaviour
{
    public Text confirmText; //提示信息文本
    public InputField password; //密码输入框
    public InputField account; //账户输入框
    private string accountStr; 
    private string passwordStr;
    public Button loginButton; //登录按钮
    public Button forgetPassword; //忘记密码按钮
    public Button registerButton; //注册按钮
    public Transform Gpan; //game界面
    public Transform Retrieve; //找回密码首界面
    public Transform Register; //登录界面

    // Start is called before the first frame update
    void Start()
    {
        accountStr = "";
        passwordStr = "";
        confirmText.text = "";
        loginButton.onClick.AddListener(Login);
        forgetPassword.onClick.AddListener(retrieve);
        registerButton.onClick.AddListener(GoToRegister);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Login()
    {
        //检测密码账号 登录
        //如果登入
        accountStr = account.text;
        passwordStr = password.text;
        string[] infos = start.sqlAccess.selectByUser(accountStr, start.tableName);
        if (infos != null)
        {
            if (infos[0] == accountStr && infos[1] == passwordStr)
            {
                Arch_DropDown.account = accountStr;
                this.gameObject.SetActive(false);//
                Gpan.gameObject.SetActive(true);//
                password.text = "";
                account.text = "";

                //accountStr 账号名
                ArchieveFinal.account = accountStr;//传递给命名文件
                //传递给显示存档的脚本
                print(Arch_DropDown.account);
                Arch_DropDown r = Gpan.gameObject.GetComponent<Arch_DropDown>();
                r.Execute();
                
            }
            else
            {
                confirmText.text = "密码错误";
                password.text = "";
            }
        }
        else
        {
            confirmText.text = "用户名不存在";
            account.text = "";
            password.text = "";
        }
    }
    //忘记密码按钮监听事件
    private void retrieve()
    {
        account.text = "";
        password.text = "";
        confirmText.text = "";
        this.gameObject.SetActive(false);
        Retrieve.gameObject.SetActive(true);
    }
    //注册界面
    private void GoToRegister()
    {
        account.text = "";
        password.text = "";
        confirmText.text = "";
        this.gameObject.SetActive(false);
        Register.gameObject.SetActive(true);
    }
}
