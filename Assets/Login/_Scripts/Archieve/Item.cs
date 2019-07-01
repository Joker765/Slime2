using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string name;
    public string tag;
    public double x,y,z;//坐标
    public double Hp;
    public double Mp;
    public int collectors;//分数
    public int scene;//关卡

    public Item()
    {

    }
    //硬币用 
    public Item(string name, string tag, double x, double y, double z)
    {
        this.name = name;
        this.tag = tag;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    //玩家用
    public Item(string name, string tag, double x, double y, double z, double hp,double mp,int collectors)
    {
        this.name = name;
        this.tag = tag;
        this.x = x;
        this.y = y;
        this.z = z;
        this.Hp = hp;
        this.Mp = mp;
        this.collectors = collectors;
    }
    //记录场景
    public Item(string tag,int scene)
    {
        this.tag = tag;
        this.scene = scene;
    }
}
