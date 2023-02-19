//using Microsoft.Data.Sqlite; //Se configura de otra forma
using System.Data.SQLite;  //Este nuget incluye EF6 y por lo tanto baja muchas otras librerias, por eso se ha descargado System.Data.SQLite.Core

namespace SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una conexión a la base de datos
            using (SQLiteConnection con = new SQLiteConnection("Data Source=database.db"))
            {
                con.Open();

                // Crea la tabla 'Users'
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "CREATE TABLE Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Age INTEGER NOT NULL)";
                    cmd.ExecuteNonQuery();
                }

                // Inserta algunos datos en la tabla
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
                    cmd.Parameters.AddWithValue("@name", "Alice");
                    cmd.Parameters.AddWithValue("@age", 20);
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@name", "Bob");
                    cmd.Parameters.AddWithValue("@age", 25);
                    cmd.ExecuteNonQuery();
                }

                // Consulta los datos de la tabla
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "SELECT * FROM Users";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Id"]}: {reader["Name"]} ({reader["Age"]})");
                        }
                    }
                }
            }
        }
    }
}
