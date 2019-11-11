using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using MySql.Data;

public class BarControl : MonoBehaviour
{
    MySqlConnection conexao;
    MySqlCommand comandos = new MySqlCommand();

    public Text txtlvl;
    public Image xpBar;
    public int lvl;
    public float xpAtual;
    public float xpAdd;
    public float maxXpNextLvl;

    string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=;";
    //string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=03201999;";

    // Start is called before the first frame update
    void Start()
    {
        conexao = new MySqlConnection(stringConexao); //Conexão na minha casa
        comandos.Connection = conexao;
        conexao.Open();
        comandos.CommandText = "SELECT xp_equipe FROM equipe WHERE idequipe = 01;";
        xpAdd = Convert.ToInt32(comandos.ExecuteScalar()); //ExecuteScalar serve para pegar um valor do Select
        //O comando ExecuteReader serve para pegar varios valores como em um Array.
        comandos.CommandText = "SELECT lvl_equipe FROM equipe WHERE idequipe = 01;";
        lvl = Convert.ToInt32(comandos.ExecuteScalar());
        conexao.Close();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        maxXpNextLvl = 10*lvl;
        xpAtual = xpAdd/maxXpNextLvl;

        if (xpAtual >=1)
        {
            lvl++;
            xpAdd = 0;
            conexao.Open();
            comandos.CommandText = "UPDATE equipe SET xp_equipe = " + xpAdd + " WHERE idequipe = 01;";
            comandos.Connection = conexao;
            comandos.ExecuteNonQuery();
            comandos.CommandText = "UPDATE equipe SET lvl_equipe = " + lvl + " WHERE idequipe = 01;";
            comandos.Connection = conexao;
            comandos.ExecuteNonQuery();
            conexao.Close();
        }

        if (xpAdd <= 0)
        {
            xpAdd = 0;
            conexao.Open();
            comandos.CommandText = "UPDATE equipe SET xp_equipe = " + xpAdd + " WHERE idequipe = 01;";
            comandos.Connection = conexao;
            comandos.ExecuteNonQuery();
            conexao.Close();
        }

        txtlvl.text = lvl.ToString();
        xpBar.fillAmount = xpAtual;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2-120, Screen.height-70, 100, 50), "Colocar Xp"))
        {
            xpAdd += 10;
            conexao.Open();
            comandos.CommandText = "UPDATE equipe SET xp_equipe = " + xpAdd + " WHERE idequipe = 01;";
            comandos.Connection = conexao;
            comandos.ExecuteNonQuery();
            conexao.Close();
        }
        if (GUI.Button(new Rect(Screen.width/2+20, Screen.height-70, 100, 50), "Tirar Xp"))
        {
            xpAdd -= 10;
            conexao.Open();
            comandos.CommandText = "UPDATE equipe SET xp_equipe = " + xpAdd + " WHERE idequipe = 01;";
            comandos.Connection = conexao;
            comandos.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
