using Microsoft.Identity.Client;
using PeopleLibrary;
using System;

namespace sqlitememe
{
    internal class Program
    {
        // TODO - potential ui clean up
        // TODO - Code refactor?????

        static List<Person> people = new List<Person>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Press enter to continue");
            Console.ReadLine();
            Console.Clear();
            while (true)
            {
                Console.Clear();
                LoadPeopleList();
                Console.WriteLine($"There are {people.Count} users in the database.");
                Console.WriteLine("What would you like to do?");

                //prompts
                Console.WriteLine("[1] Enter New User");
                Console.WriteLine("[2] Display User Information");
                Console.WriteLine("[3] Delete User");
                Console.WriteLine("[4] Update User");
                Console.WriteLine("[0] Exit");

                //int.TryParse(Console.ReadLine(), out int input);
                char input = Console.ReadKey(true).KeyChar;
                
                if (input == '1')
                {
                    Console.Clear();
                    CreateNewPerson();
                }
                if (input == '2')
                {
                    Console.Clear();
                    DisplayPeopleList();
                }
                if(input == '3')
                {
                    Console.Clear();
                    DeletePerson();
                }
                if (input == '4')
                {
                    Console.Clear();
                    UpdatePerson();
                }
                if(input == '0')
                {
                    Console.WriteLine("Goodbye!");
                    Console.WriteLine("Press enter to close.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
        private static void LoadPeopleList()
        {
            // prefab data
            //people.Add(new Person { Id = 1, FirstName = "John", LastName = "Smith", EmailAddress = "JohnSmith@gmail.com", PhoneNumber = "1112223333" });
            //people.Add(new Person { Id = 2, FirstName = "Mary", LastName = "Jane", EmailAddress = "MaryJane@gmail.com", PhoneNumber = "0009998888" });

            //SQL Data
            people = SqliteDataAccess.LoadPeople();
        }

        private static void UpdatePerson()
        {
            while (true)
            {
                Console.Clear();
                DisplayPeopleList();
                Console.WriteLine("\nWhich user's information do you want to update?");
                Console.WriteLine("(Select the id you want to update):");

                int.TryParse(Console.ReadLine(), out int updateInput);

                Person updateUser = people.FirstOrDefault(x => x.Id == updateInput);

                if (updateUser != null) {
                    Console.Clear();
                    Console.WriteLine($"User: {updateUser.Id} Selected.\n");
                    while (true) {

                        Console.WriteLine($"\nId:{updateUser.Id}\nName: {updateUser.FullName}\nEmail: {updateUser.EmailAddress}\nPhone Number: {updateUser.PhoneNumber}\n-----------------------\n");

                        Console.WriteLine("\nWhat do you want to update?\n");

                        //prompts
                        Console.WriteLine("[1] First Name");
                        Console.WriteLine("[2] Last Name");
                        Console.WriteLine("[3] Email address");
                        Console.WriteLine("[4] Phone number");
                        Console.WriteLine("[0] Go Back");

                        int.TryParse(Console.ReadLine(), out int input);

                        if (input == 1)
                        {
                            Console.WriteLine("Please enter a new first name:");
                            string firstName = Console.ReadLine();

                            if (firstName == null || firstName.Any(Char.IsWhiteSpace)) {
                                Console.WriteLine("Action Cancelled");
                                return;
                            }
                            else
                            {
                                updateUser.FirstName = firstName;
                                SqliteDataAccess.UpdatePerson(updateUser);
                            }
                        }
                        if (input == 2)
                        {
                            Console.WriteLine("Please enter a new last name:");
                            string lastName = Console.ReadLine();

                            if (lastName == null || lastName.Any(Char.IsWhiteSpace))
                            {
                                Console.WriteLine("Action Cancelled");
                                return;
                            }
                            else
                            {
                                updateUser.LastName = lastName;
                                SqliteDataAccess.UpdatePerson(updateUser);
                            }
                        }
                        if (input == 3)
                        {
                            Console.WriteLine("Please enter a new email address:");
                            string emailAddress = Console.ReadLine();

                            if (emailAddress == null || emailAddress.Any(Char.IsWhiteSpace))
                            {
                                Console.WriteLine("Action Cancelled");
                                return;
                            }
                            else
                            {
                                updateUser.EmailAddress = emailAddress;
                                SqliteDataAccess.UpdatePerson(updateUser);
                            }
                        }
                        if (input == 4)
                        {
                            Console.WriteLine("Please enter a new phone number:");
                            string phoneNumber = Console.ReadLine();

                            if (phoneNumber == null || phoneNumber.Any(Char.IsWhiteSpace))
                            {
                                Console.WriteLine("Action Cancelled");
                                return;
                            }
                            else
                            {
                                updateUser.PhoneNumber = phoneNumber;
                                SqliteDataAccess.UpdatePerson(updateUser);
                            }
                        }
                        if (input == 0)
                        {
                            return;
                        }
                    }
                }

            }
        }
        private static void DeletePerson()
        {
            while (true)
            {
                LoadPeopleList();

                Console.WriteLine("\nWhich User do you want to delete? (enter user Id, 0 to go back)");

                foreach (Person person in people)
                {
                    Console.WriteLine($"\nId: {person.Id}\nName: {person.FullName}");
                }

                int.TryParse(Console.ReadLine(), out int deleteInput);

                var removeUser = people.FirstOrDefault(x => x.Id == deleteInput);

                if (removeUser != null)
                {
                    //people.Remove(removeUser);
                    SqliteDataAccess.DeletePerson(removeUser.Id);
                    Console.WriteLine($"\n{removeUser.FullName} User Deleted!");
                }
                if (deleteInput == 0)
                {
                    Console.Clear();
                    return;
                }
            }
            
        }

        private static void DisplayPeopleList()
        {
            LoadPeopleList();

            foreach (Person person in people) {
                Console.WriteLine($"\nId:{person.Id}\nName: {person.FullName}\nEmail: {person.EmailAddress}\nPhone Number: {person.PhoneNumber}\n-----------------------\n");
            }
            Console.WriteLine("Press any key to go back.");
            char input = Console.ReadKey(true).KeyChar;

        }
        private static void CreateNewPerson()
        {
            Console.WriteLine("Please follow the prompts carefully");

            Console.WriteLine("\nPlease your first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("\nPlease your last name:");
            string lastNamee = Console.ReadLine();

            Console.WriteLine("\nPlease your email address:");
            string email = Console.ReadLine();

            Console.WriteLine("\nPlease your phoneNumber:");
            string phoneNumber = Console.ReadLine();

            Person newPerson =  CreatePerson(firstName, lastNamee, email, phoneNumber);

            //people.Add(newPerson);
            SqliteDataAccess.SavePerson(newPerson);
            Console.Clear();
            Console.WriteLine("\nNew User Created!");
        }

        private static Person CreatePerson(string firstName, string lastName, string email, string phoneNumber)
        {
            Person person = new Person { 
            FirstName = firstName,
            LastName = lastName,    
            EmailAddress = email,
            PhoneNumber = phoneNumber
            };

            return person;
        }
    }
}
