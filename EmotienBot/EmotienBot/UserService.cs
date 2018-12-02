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

        public void Save(T human)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO users(name, lasname, date_current, sender_id) VALUES (@name, @lasname, @date_current, @sender_id)";
                    cmd.Parameters.AddWithValue("@name", human.Name);
                    cmd.Parameters.AddWithValue("@lastname", human.LastName);
                    cmd.Parameters.AddWithValue("@date_current", human.Date);
                    cmd.Parameters.AddWithValue("@sender_id", human.SenderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GiveAway(int id, T entity)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT *FROM users";
                    
                    string nameUser = cmd.Parameters["@name"].Value.ToString();
                    string lastName = cmd.Parameters["@lastname"].Value.ToString();
                    string dateCurrent = cmd.Parameters["@date_current"].Value.ToString();
                    string senderId = cmd.Parameters["@sender_id"].Value.ToString();
                }
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
