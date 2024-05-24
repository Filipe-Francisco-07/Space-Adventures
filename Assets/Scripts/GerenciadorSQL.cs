/*using UnityEngine;
using System.Collections;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql;

public class GerenciadorSQL : MonoBehaviour
{
    private string connectionString;
    private MySqlConnection connection;
    private string server = "localhost";
    private string database = "SpaceAdventures";
    private string username = "root";
    private string password = "root";

    void Start()
    {

        connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";

        ConnectToDatabase();
    }

    void ConnectToDatabase()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("Conexão com o banco de dados estabelecida!");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Erro ao conectar ao banco de dados: " + ex.Message);
        }
    }

    void CloseConnection()
    {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
            connection.Close();
            Debug.Log("Conexão com o banco de dados fechada!");
        }
    }

    // Exemplo de função para executar uma consulta SQL
    void ExecuteQuery(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Erro ao executar consulta SQL: " + ex.Message);
        }
    }

    void OnDestroy()
    {

        CloseConnection();
    }
}*/