using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repositories;

class StudentRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public StudentRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    
    // Insere um estudante na tabela
    public Student Save(Student student)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Execute(@"INSERT INTO Students VALUES(@Registration, @Name, @City, @Former)", student);

        return student;
    }

    // Deleta um estudante na tabela
    public void Delete(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Execute("DELETE FROM Students WHERE (registration = @Registration)", new { Registration = registration });
    }

    // Marca um estudante como formado
    public void MarkAsFormed(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        connection.Execute("UPDATE Students SET former = true WHERE (registration = @Registration)", new { Registration = registration });
    }

    // Retorna todos os estudantes
    public List<Student> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var students = connection.Query<Student>("SELECT * FROM Students").ToList();

        return students;
    }

    // Retorna todos os estudantes de uma cidade (deve ser possível passar o nome incompleto da cidade)
    public List<Student> GetAllStudentByCity(string city)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var students = connection.Query<Student>("SELECT * FROM Students WHERE city LIKE @City", new { City = "%"+city+"%" }).ToList();

        return students;
    }

    // Retorna o número de estudantes agrupados por cidade
    public List<Student> GetAllByCities(string[] cities)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var students = connection.Query<Student>("SELECT * FROM Students WHERE city IN @Cities", new { Cities = cities }).ToList();

        return students;
    }

    // Verifica se o sstudante já está cadastrado por ID
    public bool ExistsById(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var result = connection.ExecuteScalar<bool>("SELECT count(registration) FROM Students WHERE (registration = @Registration)", new { Registration = registration });

        return result;
    }
}