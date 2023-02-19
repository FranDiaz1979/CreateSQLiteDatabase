//using Microsoft.Data.Sqlite; //Se configura de otra forma
using System.Data.SQLite;  //Este nuget incluye EF6 y por lo tanto baja muchas otras librerias, por eso se ha descargado System.Data.SQLite.Core


// Crea una conexión a la base de datos

using SQLiteConnection con = new("Data Source=database.db");
con.Open();

// Crea la tabla 'Users'

using SQLiteCommand cmd = new(con);
cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Age INTEGER NOT NULL)";
cmd.ExecuteNonQuery();

// Inserta algunos datos en la tabla

cmd.CommandText = "Delete from Users";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
cmd.Parameters.AddWithValue("@name", "Alice");
cmd.Parameters.AddWithValue("@age", 20);
cmd.ExecuteNonQuery();
cmd.Parameters.Clear();

cmd.Parameters.AddWithValue("@name", "Bob");
cmd.Parameters.AddWithValue("@age", 25);
cmd.ExecuteNonQuery();

// Consulta los datos de la tabla

cmd.CommandText = "SELECT * FROM Users";
using SQLiteDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"{reader["Id"]}: {reader["Name"]} ({reader["Age"]})");
}


