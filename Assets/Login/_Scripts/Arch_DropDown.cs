using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Arch_DropDown : MonoBehaviour
{
    List<string> fileNames;
    public  static string account;//账号
    public Dropdown dropdown1;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //设置下拉框内容
    public  void setDropdown(Dropdown dropdown, List<string> str)
    {
        dropdown.options.Clear();
        Dropdown.OptionData temoData;
        if (str.Count != 0)
        {
            for (int i = 0; i < str.Count; i++)
            {
                temoData = new Dropdown.OptionData();
                temoData.text = str[i];
                dropdown.options.Add(temoData);
            }
        }
        dropdown.captionText.text = "选择存档文件";
    }


    public void GetFileNames()
    {
        if (Directory.Exists(account))
        {
            DirectoryInfo folder = new DirectoryInfo(account);
            foreach (FileInfo file in folder.GetFiles("*.json"))
            {
                fileNames.Add(file.Name);
            }
        }
    }
    public  void Execute()
    {
        fileNames = new List<string>();
        GetFileNames();
        setDropdown(dropdown1, fileNames);
    }
}
