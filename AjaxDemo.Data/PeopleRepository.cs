﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjaxDemo.Data
{
    public class PeopleRepository
    {
        private string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var list = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]

                });
            }

            return list;
        }

        public void Add(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES (@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public Person GetPersonById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            return new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]

            };
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @first, LastName = @last, Age = @age WHERE Id = @id";
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

