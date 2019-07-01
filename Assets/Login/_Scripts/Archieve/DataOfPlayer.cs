using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public float Hp; 
    public float Mp;
    public Vector3 playerPosition;
    public int collectors;
}

public class DataOfPlayer 
{
    private static DataOfPlayer dataOfPlayer = null;
    public PlayerData playerData;
    public List<Vector3> coins = new List<Vector3>();
    public bool isArchieve;
    public static DataOfPlayer getInstance(float ahp,float amp,Vector3 position, int collectors,List<Vector3> coinPosition,bool archieve)
    {
        if (archieve == true)
        {
            dataOfPlayer = new DataOfPlayer();
            dataOfPlayer.setData(ahp,amp,position,collectors,archieve);
            dataOfPlayer.setCoins(coinPosition);
        }
        return dataOfPlayer;
    }
    public  void setArchieve(bool isArchieve)
    {
        if(dataOfPlayer!=null)
        {
            dataOfPlayer.setData(isArchieve);
        }
    }
    public void  setData(float ahp, float amp, Vector3 position,int collectors,  bool archieve)
    {
        playerData.Hp = ahp;
        playerData.Mp = amp;
        playerData.playerPosition = position;
        playerData.collectors = collectors;
        isArchieve = archieve;
    }
    public void setCoins(List<Vector3> coinPosition)
    {
        foreach(Vector3 i in coinPosition)
        {
            coins.Add(i);
        }
    }
    public void setData(bool archieve)
    {
        isArchieve = archieve;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
