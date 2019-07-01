using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SqlAccess 
{
    //连接类对象
    private static MySqlConnection mySqlConnection;
    //IP地址
    public static string host;
    //端口号
    public static string port;
    //用户名
    public static string userName;
    //密码
    public static string password;
    //数据库名称
    public static string databaseName;
    //构造方法
    public SqlAccess(string _host,string _userName,string _password,string _port,string _databaseName)
    {
        host = _host;
        userName = _userName;
        password = _password;
        port = _port;
        databaseName = _databaseName;
        OpenSql();
    }
    //连接数据库
    public void OpenSql()
    {
        try
        {
            string mySqlString = string.Format("server={0};user id={1};password={2};port={3};database={4};charset=utf8",
               host, userName, password, port, databaseName);
            mySqlConnection = new MySqlConnection(mySqlString);
            mySqlConnection.Open();

        }
        catch (Exception e)
        {
            throw new Exception("服务器连接失败，请重新检查MySql服务是否打开。" + e.Message.ToString());
        }
    }
    //关闭数据库
    public void CloseSql()
    {
        if (mySqlConnection != null)
        {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;
        }
    }
   // 插入数据
    public bool Insert(string[] infos,string tableName)
    {
        if (mySqlConnection.State == ConnectionState.Open)
        {
            if (infos.Length >=6)
            {
                string mysqlString = string.Format("insert into {0}(User_name,password,Security1,Answer1,Security2,Answer2) values('{1}','{2}',{3},'{4}',{5},'{6}')",
                                                      tableName, infos[0], infos[1],infos[2],infos[3],infos[4],infos[5]);
                MySqlCommand mycmd = new MySqlCommand(mysqlString, mySqlConnection);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }

            else
                return false;
        }
        return false;
    }
    //通过账号获得用户信息
    public string[] selectByUser(string account,string tableName)
    {
        string[] infos = new string[6];
        string mysqlString = string.Format("select * from {0} where User_name = '{1}'",tableName,account);
        if (mySqlConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
               MySqlDataAdapter mySqlAdapter = new MySqlDataAdapter(mysqlString, mySqlConnection);
                mySqlAdapter.Fill(ds);
                if (ds.Tables[0].Rows.Count!=0)
                {
                  DataRow dataRow = ds.Tables[0].Rows[0];
                    infos[0] = dataRow["User_name"] as string;
                    infos[1] = dataRow["Password"] as string;
                    infos[2] = (dataRow["Security1"]).ToString();
                    infos[3] = dataRow["Answer1"] as string;
                    infos[4] = dataRow["Security2"].ToString();
                    infos[5] = dataRow["Answer2"] as string;                 
                    return infos;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + mysqlString + "/n" + e.Message.ToString());
            }
            finally
            {
            }
        }
        return null;
    }
    //修改信息
    public bool updateByUser(string account,string content,string columnName,string tableName)
    {
        string mysqlString = string.Format("update {0} set {1}= '{2}' where  User_name = '{3}'", tableName, columnName,content,account);
        if (mySqlConnection.State == ConnectionState.Open)
        {
            MySqlCommand mycmd = new MySqlCommand(mysqlString, mySqlConnection);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            return false;
        }
        return false;
    }
 }
