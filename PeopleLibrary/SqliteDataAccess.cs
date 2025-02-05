using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace PeopleLibrary
{
    public class SqliteDataAccess
    {
        public static List<Person> LoadPeople()
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Person>("select * from Person", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SavePerson(Person person)
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                string savePersonSql = "insert into Person (FirstName, LastName, EmailAddress, PhoneNumber) values (@FirstName, @LastName, @EmailAddress, @PhoneNumber)";

                cnn.Execute(savePersonSql, person);
            }
        }

        public static void DeletePerson(int id) 
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Person where Id='{id}'");
            }
        }

        public static void UpdatePerson(Person person)
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Person set FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber where Id = @Id", person);
            }   
        }


        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
