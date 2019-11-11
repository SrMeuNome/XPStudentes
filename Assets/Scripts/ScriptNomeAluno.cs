using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;

public class ScriptNomeAluno : MonoBehaviour
{
    public InputField campoDeEntrada;
    MySqlConnection conexao;
    MySqlCommand comandos = new MySqlCommand();

    string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=;";
    //string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=03201999;";

    public void CadastrarAluno(InputField campoDeEntrada)
    {
        conexao = new MySqlConnection(stringConexao);
        comandos.Connection = conexao;
        comandos.CommandText = "INSERT INTO aluno(nome_aluno) VALUES('" + campoDeEntrada.text + "');";
        
        conexao.Open();
        comandos.ExecuteNonQuery();
        conexao.Close();
    }
}
