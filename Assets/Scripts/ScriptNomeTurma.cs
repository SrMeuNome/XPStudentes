using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;

public class ScriptNomeTurma : MonoBehaviour
{
    public InputField campoDeEntrada;
    MySqlConnection conexao;
    MySqlCommand comandos = new MySqlCommand();
    string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=;";
    //string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=03201999;";

    public void CadastrarTurma(InputField campoDeEntrada)
    {
        conexao = new MySqlConnection(stringConexao);
        comandos.Connection = conexao;
        comandos.CommandText = "INSERT INTO equipe(nome_equipe, lvl_equipe, xp_equipe) VALUES('" + campoDeEntrada.text + "', 1, 0);";
        
        conexao.Open();
        comandos.ExecuteNonQuery();
        conexao.Close();
    }
}
