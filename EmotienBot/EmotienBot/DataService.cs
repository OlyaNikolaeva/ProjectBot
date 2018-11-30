using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    class UserService<T> : IDataService<T> where T : Human
    {

        const string connString = "Host=localhost;Port=5432;Username=postgres;Password=2112;Database=postgres";
        private string ConnectionString { get; set; }
        public UserService(string connStr)
        {
            ConnectionString = connStr;
        }

        public UserService()
        {
        }

        public void Save(T human)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO userInfoEmotien(name, age, emotien, sender_id) VALUES (@name, @age, @emotien, @senderid)";
                    cmd.Parameters.AddWithValue("@name", human.Name);
                    cmd.Parameters.AddWithValue("@age", human.Age);
                    cmd.Parameters.AddWithValue("@group", human.Emotien);
                    cmd.Parameters.AddWithValue("@sender_id", human.SenderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, T entity)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE userInfo SET  WHERE id=#id,entity=#entity;";
                    cmd.Parameters.AddWithValue(entity);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new NpgsqlCommand("SELECT *FROM userInfo", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }
        }

        public IEnumerable<T> GetAll()
        {
            var cacheList = new List<T>();
            foreach (var value in cacheList)
            {
                if (value != null)
                    cacheList.Add(value);
            }
            return cacheList;
        }
    }
}
