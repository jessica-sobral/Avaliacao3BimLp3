/*
Student - Registration, Name, City, Former

dotnet run -- Student New 1 "João da Silva" "São Paulo"
dotnet run -- Student Delete 1
dotnet run -- Student MarkAsFormed 1
dotnet run -- Student List
dotnet run -- Student ListFormed
dotnet run -- Student ListByCity "Sã"
dotnet run -- Student ListByCities "São Paulo" "Guarulhos"
dotnet run -- Student Report CountByCities
dotnet run -- Student Report CountByFormed

dotnet add package Microsoft.Data.Sqlite
dotnet add package dapper
*/

using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var studentRepository = new StudentRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    if(modelAction == "New")
    {
        string registration = args[2];
        string name = args[3];
        string city = args[4];

        var student = new Student(registration, name, city, false);

        if(!(studentRepository.ExistsById(registration)))
        {
            studentRepository.Save(student);
            Console.WriteLine($"Estudante {student.Name} cadastrado com sucesso.");
        } else {
            Console.WriteLine($"Estudante com Id {student.Registration} já existe.");
        }
    }

    if(modelAction == "Delete")
    {
        var registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso.");
        } else {
            Console.WriteLine($"Estudante {registration} não encontrado.");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        string registration = args[2];

        if(studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido como formado.");
        } else {
            Console.WriteLine($"Estudante {registration} não encontrado.");
        }
    }

    if(modelAction == "List")
    {
        if(studentRepository.GetAll().Count() > 0)
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAll())
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                } else {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não formado");
                }
            }
        } else {
            Console.WriteLine("Nenhum estudante cadastrado.");
        }
    }

    if(modelAction == "ListByCity")
    {
        string city = args[2];

        if(studentRepository.GetAllStudentByCity(city).Count() > 0)
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllStudentByCity(city))
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                } else {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não formado");
                }
            }
        } else {
            Console.WriteLine("Nenhum estudante cadastrado.");
        }
    }

    if(modelAction == "ListByCities")
    {
        string[] cities = new string[args.Length-2];
        for(int i = 2; i < args.Length; i++)
        {
            cities[i-2] = args[i];
        }

        if(studentRepository.GetAllByCities(cities).Count() > 0)
        {
            Console.WriteLine("Student List");
            foreach (var student in studentRepository.GetAllByCities(cities))
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Formado");
                } else {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, Não formado");
                }
            }
        } else {
            Console.WriteLine("Nenhum estudante cadastrado.");
        }
        
    }
}