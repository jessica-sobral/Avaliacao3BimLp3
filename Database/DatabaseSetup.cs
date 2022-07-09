using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Database;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateStudentTable();
    }

    private void CreateStudentTable()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students(
                registration varchar(100) not null primary key,
                name varchar(100) not null,
                city varchar(100) not null,
                former boolean
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
}