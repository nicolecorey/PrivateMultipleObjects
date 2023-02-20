using System;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Transactions;

namespace Inheritance
{
    
    class Students
    {
        private int _Id;
        private string _Name;
        private int _gradYear;

        public Students()
        {
            _Id = 0;
            _Name = string.Empty;
            _gradYear = 0;
        }
        public Students(int id, string name, int gradYear)
        {
            _Id = id;
            _Name = name;
            _gradYear = gradYear;
        }
        public int getID() { return _Id; }
        public string getName() { return _Name; }
        public int getgradYear() { return _gradYear; }
        public void setID(int id) { _Id = id; }
        public void setName(string Name) { _Name = Name; }
        public void setgradYear(int gradYear) { _gradYear = gradYear; }

        public virtual void addChange()
        {
            Console.Write("ID:");
            setID(int.Parse(Console.ReadLine()));
            Console.Write("Name:");
            setName(Console.ReadLine());
            Console.Write("Graduation Year:");
            setgradYear(int.Parse(Console.ReadLine()));
        }
        public virtual void print()
        {
            Console.WriteLine();
            Console.WriteLine($"ID: {getID()}");
            Console.WriteLine($"Name: {getName()}");
            Console.WriteLine($"Graduation Year: {getgradYear()}");
        }
    }
    class Teachers : Students
    {
        private string _Subject;
        private string _Location;

        public Teachers()
            : base()
        {
            _Subject = string.Empty;
            _Location = string.Empty;
        }
        public Teachers(int id, string name, int gradYear, string subject, string location)
            : base(id, name, gradYear)
        {
            _Subject = subject;
            _Location = location;
        }
        public void setSubject(string subject) { _Subject = subject; }
        public void setLocation(string location) { _Location = location; }
        public string getSubject() { return _Subject; }
        public string getLocation() { return _Location; }
        public override void addChange()
        {
            base.addChange();
            Console.Write("Subject taught:");
            setSubject(Console.ReadLine());
            Console.Write("Location:");
            setLocation(Console.ReadLine());
        }
        public override void print()
        {
            base.print();
            Console.WriteLine($"Subject: {getSubject()}");
            Console.WriteLine($"Location: {getLocation()}");
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("How many students do you want to enter?");
            int maxStudents;
            while (!int.TryParse(Console.ReadLine(), out maxStudents))
                Console.WriteLine("Please enter a whole number");
           
            Students[] students = new Students[maxStudents];

            Console.WriteLine("How many Teachers do you want to enter?");
            int maxTeachers;
            while (!int.TryParse(Console.ReadLine(), out maxTeachers))
                Console.WriteLine("Please enter a whole number");
           
            Teachers[] teachers = new Teachers[maxTeachers];

            int choice, rec, type;
            int studentCounter = 0, teacherCounter = 0;
            choice = Menu();
            while (choice != 4)
            {
                Console.WriteLine("Enter 1 for Teacher or 2 for Student");
                while (!int.TryParse(Console.ReadLine(), out type))
                    Console.WriteLine("1 for Teacher or 2 for Student");
                try
                {
                    switch (choice)
                    {
                        case 1: 
                            if (type == 1) 
                            {
                                if (teacherCounter <= maxTeachers)
                                {
                                    teachers[teacherCounter] = new Teachers(); 
                                    teachers[teacherCounter].addChange();
                                    teacherCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of Teachers has been reached.");

                            }
                            else 
                            {
                                if (studentCounter <= maxStudents)
                                {
                                    students[studentCounter] = new Students(); 
                                    students[studentCounter].addChange();
                                    studentCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of students has been reached.");
                            }

                            break;
                        case 2: //Change
                            Console.Write("Enter the ID number you want to change: ");
                            while (!int.TryParse(Console.ReadLine(), out rec))
                                Console.Write("Enter the ID number you want to change: ");
                            rec--;  
                            if (type == 1) 
                            {
                                while (rec > teacherCounter - 1 || rec < 0)
                                {
                                    Console.Write("Your number must be in range, please try again.");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the ID number you want to change: ");
                                    rec--;
                                }
                                teachers[rec].addChange();
                            }
                            else 
                            {
                                while (rec > studentCounter - 1 || rec < 0)
                                {
                                    Console.Write("Your number must be in range, please try again.");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the ID number you want to change: ");
                                    rec--;
                                }
                                students[rec].addChange();
                            }
                            break;
                        case 3: 
                            if (type == 1) 
                            {
                                for (int i = 0; i < teacherCounter; i++)
                                    teachers[i].print();
                            }
                            else 
                            {
                                for (int i = 0; i < studentCounter; i++)
                                    students[i].print();
                            }
                            break;
                        default:
                            Console.WriteLine("Sorry, that is not an option. Please try again");
                            break;
                    }
                }


                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                choice = Menu();

            }
        }


        private static int Menu()
        {
            Console.WriteLine("Please choose a selection.");
            Console.WriteLine("1:Add  2:Change  3:Display  4:End Session");
            int selection = 0;
            while (selection < 1 || selection > 4)
                while (!int.TryParse(Console.ReadLine(), out selection))
                    Console.WriteLine("1:Add  2:Change  3:Display  4:End Session");
            return selection;
        }
    }
}