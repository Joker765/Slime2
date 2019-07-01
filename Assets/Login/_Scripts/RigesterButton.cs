using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
public class RigesterButton : MonoBehaviour
{
    public InputField account;  //账户输入框
    public InputField password; //密码输入框
    public InputField confirPassword; //确认密码输入框
    public Button next; //下一步按钮
    public Button back; //返回按钮
    public Transform security; //下个设定密保界面
    public Transform LoginPan;//登录界面
    public Text tpt;//两次密码提示框
    public Text confirmAccount; //用户名是否不正确提示框
  //  private string virtulStr;
    private string accountStr;
    private string passwordStr;
    private string confirPasswordStr;
    // Start is called before the first frame update
    void Start()
    {
        tpt.text = "";
        confirmAccount.text = "";
        accountStr = "";
        passwordStr = "";
        confirPasswordStr = "";
      next.onClick.AddListener(Clicked);
        back.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //返回按钮响应事件
    private void Back()
    {
        confirmAccount.text = "";
        tpt.text = "";
        account.text = "";
        password.text = "";
        confirPassword.text = ""; 
        this.gameObject.SetActive(false);
        LoginPan.gameObject.SetActive(true);
    }
    //注册
    private void Clicked()
    {
        accountStr = account.text;
        passwordStr = password.text;
        confirPasswordStr = confirPassword.text;
        confirmAccount.text = "";
        tpt.text = "";
        //检测用户名是否重复
        if (start.sqlAccess.selectByUser(accountStr, start.tableName) != null)
        {
            confirmAccount.text = "账号已存在";
        }
        else if(accountStr=="")
        {
            confirmAccount.text = "账号不可为空";
        }
        else
        {
            //检测密码情况
            if (passwordStr != confirPasswordStr)
            {
                tpt.text = "两次密码不一样";
            }
            else if (passwordStr == "" && confirPasswordStr == "")
            {
                tpt.text = "密码不可为空";
            }
            else
            {
                confirmAccount.text = "";
                tpt.text = "";
                //转到添加密保问题窗口
                SetSecurity setSecurity = security.gameObject.GetComponent<SetSecurity>();
                setSecurity.setUser(accountStr, passwordStr);
                this.gameObject.SetActive(false);
                security.gameObject.SetActive(true);
                confirmAccount.text = "";
                tpt.text = "";
                account.text = "";
                password.text = "";
                confirPassword.text = "";
            }
        }
    }
   
}
