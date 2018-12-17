using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    class FileService<T> : IDataService<T> where T : Photo, new()
    {
        const string connString = "Host=localhost;Port=5432;Username=postgres;Password=5432;Database=postgres";
        private string ConnectionString { get; set; }

        public void Save(T file)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO photo(path,date_create,user_id) VALUES (@path, @date_create, @user_id)";
                    cmd.Parameters.AddWithValue("@path",file.Path );
                    cmd.Parameters.AddWithValue("@date_create",file.DateCreate);
                    cmd.Parameters.AddWithValue("@user_id",file.UserId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            var result = new List<T>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT path,date_create,user_id FROM photo";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        T entity = new T
                        {
                            UserId = int.Parse(reader[0].ToString()),
                            Path = reader[1].ToString(),
                            DateCreate = Convert.ToDateTime(reader[3].ToString())
                        };
                        result.Add(entity);
                    }
                }

                return result;
            }
        }
    }
}
