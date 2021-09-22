using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using Traineeship.Models;

namespace Traineeship.Database
{
    public class DBConnector
    {
        string connection = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = E:\Projects\Traineeship\Traineeship\Database\AppDatabase.accdb";

        public bool InsertUser(string login, string password)
        {
            try
            {
                string query = $"INSERT INTO Users VALUES ('{login}', '{password}')";
                OleDbConnection con = new OleDbConnection(connection);
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SelectUser(string login, string password)
        {
            string query = $"SELECT * FROM Users WHERE login = '{login}' AND password = '{password}'";
            bool state;
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            state =  reader.HasRows;
            con.Close();
            return state;
        }
        public void InsertTopic(TopicModel topic)
        {
            string query = $"INSERT INTO Topics(TopicName, ParentId, Owner) VALUES ('{topic.Name}','{topic.ParentId}','{topic.Owner}')";
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<TopicModel> SelectTopics(int id = 0)
        {
            string query;
            List<TopicModel> topics = new List<TopicModel>(); 
            if (id == 0)
                query = "SELECT * FROM Topics";
            else
                query = $"SELECT * FROM Topics WHERE id={id}";
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                topics.Add(new TopicModel(Convert.ToInt32(reader["Id"]), reader["TopicName"].ToString(), Convert.ToInt32(reader["ParentId"]), reader["Owner"].ToString()));
            }
            con.Close();
            return topics;
        }
        public void EditTopic(int topicId, string newName)
        {
            string query = $"UPDATE Topics SET TopicName = '{newName}' WHERE Id = {topicId}";
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteTopic(int topicId)
        {
            string query = $"DELETE * FROM Topics WHERE Id = {topicId}";
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
