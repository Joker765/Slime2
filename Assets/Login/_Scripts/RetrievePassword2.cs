using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievePassword2 : MonoBehaviour
{
    public Transform back; //返回界面
    public Transform next; //下个界面
    public Text question1; //问题1文本
    public Text question2;  //问题2文本
    public Text confirmText1; //提示一文本
    public Text confirmText2; //提示二文本
    public InputField answer1;
    public InputField answer2;
    public Button backButton;
    public Button confirmButton;
    public string[] infos; //用户信息,0:账号，1:密码，2:问题一序号,3:答案一,4:问题二序号,5:答案二
    // Start is called before the first frame update
    void Start()
    {
        confirmText1.text = "";
        confirmText2.text = "";
        backButton.onClick.AddListener(Back);
        confirmButton.onClick.AddListener(confirmAnswer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //返回按钮响应时间
    private void Back()
    {
        question1.text = "";
        question2.text = "";
        confirmText1.text = "";
        confirmText2.text = "";
        answer1.text = "";
        answer2.text = "";
        this.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }
    //下一步按钮响应事件
    private void confirmAnswer()
    {
        confirmText1.text = "";
        confirmText2.text = "";
        if (infos!=null)
        {
            if (answer1.text == infos[3]&&answer2.text == infos[5])
            {
                //传账号给下一界面
                RetrievePassword3 retrievePassword3 = next.gameObject.GetComponent<RetrievePassword3>();
                retrievePassword3.setAccount(infos[0]);
                answer1.text = "";
                answer2.text = "";
                this.gameObject.SetActive(false);
                next.gameObject.SetActive(true);
            }
            else
            {
                if (answer1.text != infos[3])
                    confirmText1.text = "答案不正确，请重新输入";
                if (answer2.text != infos[5])
                    confirmText2.text = "答案不正确，请重新输入";
            }          
        }
    }
    public void getAnswer(string account)
    {
        infos = start.sqlAccess.selectByUser(account,start.tableName);
        question1.text = start.question1[int.Parse(infos[2])];
        question2.text = start.question2[int.Parse(infos[4])];
    }
    
}
