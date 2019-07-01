using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSecurity : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public InputField input1;
    public InputField input2;
    public Button back;
    public Button register;
    public Transform Register;
    public Transform login;
    public string account;
    public string password;
    // Start is called before the first frame update
    void Start()
    {
        setDropdown(dropdown1, start.question1);
        setDropdown(dropdown2, start.question2);
        back.onClick.AddListener(backToRegister);
        register.onClick.AddListener(recordInfos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void backToRegister()
    {
        this.gameObject.SetActive(false);
        Register.gameObject.SetActive(true);
    }
    //将用户数据插入数据库
    private void recordInfos()
    {
        string[] infos = { account, password, dropdown1.value.ToString(), input1.text, dropdown2.value.ToString(), input2.text };
        start.sqlAccess.Insert(infos, start.tableName);
        this.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
    }
    //设置下拉框内容
    private void setDropdown(Dropdown dropdown,string[] str)
    {
        dropdown.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < str.Length; i++)
        {
            temoData = new Dropdown.OptionData();
            temoData.text = str[i];
            dropdown.options.Add(temoData);
        }
        dropdown.captionText.text = "请选择密保问题";
    }
    public void setUser(string aaccount,string apassword)
    {
        account = aaccount;
        password = apassword;
    }
}
