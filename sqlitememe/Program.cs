using PeopleLibrary;

namespace sqlitememe
{
    internal class Program
    {
        static List<Person> people = new List<Person>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Press enter to continue");
            Console.ReadLine();

            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?");

                //prompts
                Console.WriteLine("[1] Enter New User");
                Console.WriteLine("[2] Display User Information");
                Console.WriteLine("[3] Delete User");
                Console.WriteLine("[0] Exit");

                int.TryParse(Console.ReadLine(), out int input);
                if (input == 1)
                {
                    CreateNewPerson();
                }
                if (input == 2)
                {
                    DisplayPeopleList();
                }
                if(input == 3)
                {
                    DeletePerson();
                }
                if(input == 0)
                {
                    Console.WriteLine("Goodbye!");
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
                    return;
                }
            }
            
        }

        private static void DisplayPeopleList()
        {
            LoadPeopleList();

            foreach (Person person in people) {
                Console.WriteLine($"\nId:{person.Id}\nName: {person.FullName}\nEmail: {person.EmailAddress}\nPhone Number: {person.PhoneNumber}\n-----------------------");
            }

        }
        private static void CreateNewPerson()
        {
            Console.WriteLine("Please follow the prompts carefully");

            Console.WriteLine("Please your first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please your last name:");
            string lastNamee = Console.ReadLine();

            Console.WriteLine("Please your email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Please your phoneNumber:");
            string phoneNumber = Console.ReadLine();

            Person newPerson =  CreatePerson(firstName, lastNamee, email, phoneNumber);

            //people.Add(newPerson);
            SqliteDataAccess.SavePerson(newPerson);

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
