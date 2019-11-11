using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MySql.Data;
using MySql.Data.MySqlClient;

public class ScriptAlunoTurma : MonoBehaviour
{
    public Dropdown dpdTurma;
    public Dropdown dpdAluno;
    
    MySqlConnection conexao;
    MySqlCommand cmd = new MySqlCommand();

    MySqlDataReader ler;
    List<string> listaAluno = new List<string>();
    List<string> listaTurma = new List<string>();
    public bool contem = false;

    string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=;";
    //string stringConexao = "Server=localhost;Database=DBXP;Uid=root;Pwd=03201999;"

    int idEquipe;
    bool[] isSelect = new bool [50];

    int c;

    private void Start()
    {
        CarregarAluno(dpdAluno);
        CarregarTurma(dpdTurma);
        dpdAluno.AddOptions(listaAluno);
        dpdTurma.AddOptions(listaTurma);
    }

    private void Update()
    {
        CarregarAluno(dpdAluno);
        CarregarTurma(dpdTurma);
    }

    public void CarregarAluno(Dropdown dpdAluno)
    {
        conexao = new MySqlConnection(stringConexao);
        cmd.Connection = conexao;
        cmd.CommandText = "SELECT nome_aluno FROM aluno;";

        conexao.Open();
        
        ler = cmd.ExecuteReader();

            while(ler.Read())
            {
                if(listaAluno.Count == 0)
                {
                    listaAluno.Add(ler.GetString("nome_aluno"));
                }
                else
                {
                    for(int i = 0; i < listaAluno.Count; i++)
                    {
                        if((listaAluno[i] == ler.GetString("nome_aluno")))
                        {
                            contem = true;
                        }
                    }
                    if(!contem)
                    {
                        listaAluno.Add(ler.GetString("nome_aluno"));
                    }
                }
            }
            contem = false;
        conexao.Close();
        dpdAluno.RefreshShownValue();
    }

    public void CarregarTurma(Dropdown dpdTurma)
    {
        conexao = new MySqlConnection(stringConexao);
        cmd.Connection = conexao;
        cmd.CommandText = "SELECT nome_equipe FROM equipe;";

        conexao.Open();
        
        ler = cmd.ExecuteReader();

            while(ler.Read())
            {
                if(listaTurma.Count == 0)
                {
                    listaTurma.Add(ler.GetString("nome_equipe"));
                }
                else
                {
                    for(int i = 0; i < listaTurma.Count; i++)
                    {
                        if((listaTurma[i] == ler.GetString("nome_equipe")))
                        {
                            contem = true;
                        }
                    }
                    if(!contem)
                    {
                        listaTurma.Add(ler.GetString("nome_equipe"));
                    }
                }
            }
            contem = false;
        conexao.Close();
        dpdTurma.RefreshShownValue();
    }

    public void ExcluirAlunoTurma() //Corrigir
    {
        conexao = new MySqlConnection(stringConexao);
        cmd.Connection = conexao;
        for(int i = 0; i<50; i++)
        {
            if(isSelect[i])
            {
                cmd.CommandText = "Delete From aluno_equipe WHERE idaluno = " + i + ";";
                conexao.Open();
                    cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }

    public void AddAlunoTurma()
    {
        string nomealuno = dpdAluno.options[dpdAluno.value].text;
        string nomeequipe = dpdTurma.options[dpdTurma.value].text;
        int idaluno = 0;
        int idturma = 0;

        conexao = new MySqlConnection(stringConexao);
        cmd.Connection = conexao;

        cmd.CommandText = "SELECT idaluno FROM aluno WHERE nome_aluno = " + "'" + nomealuno + "' ;";
        conexao.Open();
            ler = cmd.ExecuteReader();

            while(ler.Read())
            {
                idaluno = ler.GetInt32("idaluno");
            }
        conexao.Close();

        cmd.CommandText = "SELECT idequipe FROM equipe WHERE nome_equipe = " + "'" + nomeequipe + "' ;";
        conexao.Open();
            ler = cmd.ExecuteReader();

            while(ler.Read())
            {
                idturma = ler.GetInt32("idequipe");
            }
        conexao.Close();

        cmd.CommandText = "INSERT INTO aluno_equipe VALUES(" + idaluno + "," + idturma + ");";
        conexao.Open();
            cmd.ExecuteNonQuery();
        conexao.Close();
    }

    private void OnGUI()
    {
        conexao = new MySqlConnection(stringConexao);
        cmd.Connection = conexao;
        cmd.CommandText = "SELECT idequipe FROM equipe WHERE nome_equipe = " + "'" + dpdTurma.options[dpdTurma.value].text + "';";
        conexao.Open();

        ler = cmd.ExecuteReader();
        while(ler.Read())
        {
            idEquipe = ler.GetInt32("idequipe");
       }
        conexao.Close();

        cmd.CommandText = "SELECT nome_aluno FROM aluno, equipe, aluno_equipe WHERE aluno.idaluno = aluno_equipe.idaluno AND aluno_equipe.idequipe = equipe.idequipe AND aluno_equipe.idequipe = " + idEquipe + ";";

        conexao.Open();
        
        ler = cmd.ExecuteReader();

        GUILayout.BeginArea(new Rect((Screen.width/2)-200, (Screen.height/2)-100, (Screen.width/2)-100, (Screen.height/2)-100));
            GUILayout.BeginScrollView(new Vector2((Screen.width/2)-200, (Screen.height/2)-100));
                while(ler.Read())
                {
                    isSelect[c] = GUILayout.Toggle(isSelect[c], ler.GetString("nome_aluno"));
                    c++;
                }
                c=0;
            GUILayout.EndScrollView();
        GUILayout.EndArea();

        conexao.Close();
    }
}
