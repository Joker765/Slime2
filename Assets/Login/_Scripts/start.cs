using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class start : MonoBehaviour
{
    public static string  tableName; //表名
    public static string[] question1 = { "你父亲的姓名", "你母亲的姓名", "你爱人的姓名" };//密保问题一
    public static string[] question2 = { "你毕业与哪所初中", "你毕业与那所高中", "你就业与哪所公司" };//密保问题二
    public Transform login;
    public Transform res;
    public Transform gpan;
    public Transform security;
    public Transform retrieve1;
    public Transform retrieve2;
    public Transform retrieve3;
    public static SqlAccess sqlAccess;
    public  string host;    //IP地址
    public  string port;    //端口号
    public  string userName;    //用户名
    public  string password;    //密码
    public  string databaseName;    //数据库名称
    // Start is called before the first frame update
    void Start()
    {
        login.gameObject.SetActive(true);
        res.gameObject.SetActive(false);
        gpan.gameObject.SetActive(false);
        security.gameObject.SetActive(false);
        retrieve1.gameObject.SetActive(false);
        retrieve2.gameObject.SetActive(false);
        retrieve3.gameObject.SetActive(false);
        tableName = "user";
        sqlAccess = new SqlAccess(host, userName, password, port, databaseName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
