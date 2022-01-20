using System;
using ListManagement.models;

namespace ListManagement // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ToDoList = new List<ToDo>();        // Initializing the list as not null
            var nextToDo = new ToDo();

            Console.WriteLine("Welcome to the List Management App");
            PrintMenu();

            int input = -1;

            if (int.TryParse(Console.ReadLine(), out input))
            {
                while (input != 7)
                {
                    // ToDoList = null;
                    // ToDoList?.Clear();  // Clears the list and handles error exception
                    // ToDoList = ToDoList ?? new List<ToDo>();    // Null coalescing operator C#

                    nextToDo = new ToDo();
                    if (input == 1)
                    {
                        string tempInput = "";
                        DateTime dateValue = DateTime.Today;
                        do
                        {
                            Console.WriteLine("Give me a name for the task you want to create: ");
                            tempInput = Console.ReadLine();

                        } while (!tempInput.Any());
                        nextToDo.Name = tempInput;
                        Console.WriteLine($"Give me a description for the task '{nextToDo.Name}': ");
                        nextToDo.Description = Console.ReadLine();
                        Console.WriteLine($"Give me a deadline in the format (MM/DD/YY) for the task '{nextToDo.Name}': ");
                        while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                        {
                            Console.WriteLine($"Please give me a valid deadline for the task '{nextToDo.Name}': ");
                        }
                        nextToDo.Deadline = dateValue;
                        Console.WriteLine($"Task successfully added. Name: '{nextToDo.Name}' | Description: '{nextToDo.Description}' | " +
                        $"Deadline: '{nextToDo.Deadline.Month}/{nextToDo.Deadline.Day}/{nextToDo.Deadline.Year}'");
                        ToDoList.Add(nextToDo);
                    }
                    else if (input == 2)
                    {
                        // D - Delete/Remove
                        int index;
                        PrintAllTasks(ToDoList);
                        Console.WriteLine("Please indicate the task you want to delete.");
                        while (!int.TryParse(Console.ReadLine(), out index) || index > ToDoList.Count) {
                            Console.WriteLine("Please provide a valid index for the task you want to delete.");
                        }
                        ToDoList.RemoveAt(index - 1);
                        Console.WriteLine("You have successfully deleted the indicated task.");
                    }
                    else if (input == 3)
                    {
                        int index;
                        int option;
                        string redo = "";
                        do
                        {
                            PrintAllTasks(ToDoList);
                            Console.WriteLine("Which task do you want to edit?");
                            while (!int.TryParse(Console.ReadLine(), out index) || index > ToDoList.Count())
                            {
                                Console.WriteLine("Please give me an integer index of the task you want to edit/update.");
                            }
                            Console.WriteLine("What do you want to edit?");
                            Console.WriteLine("1. Name");
                            Console.WriteLine("2. Description");
                            Console.WriteLine("3. Deadline");
                            Console.WriteLine("4. Complete/Uncomplete");
                            Console.WriteLine("5. All of them");
                            while (!int.TryParse(Console.ReadLine(), out option) || option > 5)
                            {
                                Console.WriteLine("Please give me a valid option from the menu.");
                            }
                            if (option == 1)
                            {
                                Console.WriteLine($"The name for task is '{ToDoList[index - 1].Name}.'");
                                Console.WriteLine("What would you like to change that to?");
                                ToDoList[index - 1].Name = Console.ReadLine();
                                Console.WriteLine($"You have successfully updated the name of the task to '{ToDoList[index - 1].Name}.'");
                            }
                            else if (option == 2)
                            {
                                Console.WriteLine($"The description for task '{ToDoList[index - 1].Name}' is '{ToDoList[index - 1].Description}.'");
                                Console.WriteLine("What would you like to change the description to?");
                                ToDoList[index - 1].Description = Console.ReadLine();
                                Console.WriteLine($"You have successfully updated the description of the task '{ToDoList[index - 1].Name}.'");
                            }
                            else if (option == 3)
                            {
                                DateTime dateValue;
                                Console.WriteLine($"The deadline for task '{ToDoList[index - 1].Name}' is '{ToDoList[index - 1].Deadline}.'");
                                Console.WriteLine("What would you like to change the deadline to?");
                                while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                                {
                                    Console.WriteLine($"Please give me a valid deadline for the task '{ToDoList[index - 1].Name}': ");
                                }
                                ToDoList[index - 1].Deadline = dateValue;
                                Console.WriteLine($"You have successfully updated the deadline of the task '{ToDoList[index - 1].Name}.'");
                            }
                            else if (option == 4)
                            {
                                Console.WriteLine($"The task '{ToDoList[index - 1].Name}' is marked as '{ToDoList[index - 1].IsCompleted}.'");
                                Console.WriteLine("Would you like to change that? (Y/N)");
                                if (Console.ReadLine() == "Y")
                                {
                                    ToDoList[index - 1].IsCompleted = !ToDoList[index - 1].IsCompleted;
                                }
                                else if (Console.ReadLine() == "N")
                                {
                                    Console.WriteLine("No, then we will leave it as is.");
                                }
                                Console.WriteLine($"You have successfully updated the status of the task '{ToDoList[index - 1].Name}.'");
                            }
                            else if (option == 5)
                            {
                                Console.WriteLine($"The name for task is '{ToDoList[index - 1].Name}.'");
                                Console.WriteLine("What would you like to change that to?");
                                ToDoList[index - 1].Name = Console.ReadLine();
                                Console.WriteLine($"The description for task '{ToDoList[index - 1].Name}' is '{ToDoList[index - 1].Description}.'");
                                Console.WriteLine("What would you like to change the description to?");
                                ToDoList[index - 1].Description = Console.ReadLine();
                                DateTime dateValue;
                                Console.WriteLine($"The deadline for task '{ToDoList[index - 1].Name}' is '{ToDoList[index - 1].Deadline}.'");
                                Console.WriteLine("What would you like to change the deadline to?");
                                while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                                {
                                    Console.WriteLine($"Please give me a valid deadline for the task '{ToDoList[index - 1].Name}': ");
                                }
                                ToDoList[index - 1].Deadline = dateValue;
                                Console.WriteLine($"The task '{ToDoList[index - 1].Name}' is marked as '{ToDoList[index - 1].IsCompleted}.'");
                                Console.WriteLine("Would you like to change that? (Y/N)");
                                if (Console.ReadLine() == "Y")
                                {
                                    ToDoList[index - 1].IsCompleted = !ToDoList[index - 1].IsCompleted;
                                }
                                else if (Console.ReadLine() == "N")
                                {
                                    Console.WriteLine("No, then we will leave it as is.");
                                }
                                Console.WriteLine($"You have successfully updated all of task '{ToDoList[index - 1].Name}' fields");
                            }
                            Console.WriteLine("Would you like to edit something else? (Y/N)");
                            redo = Console.ReadLine();
                        } while (redo.Equals("Y", StringComparison.InvariantCultureIgnoreCase));

                        // U - Update/Edit
                    }
                    else if (input == 4)
                    {
                        int temp;
                        PrintAllTasks(ToDoList);
                        Console.WriteLine("Please give me the index of the task you want to mark as completed.");
                        while (!int.TryParse(Console.ReadLine(), out temp) || temp > ToDoList.Count() || ToDoList[temp-1].IsCompleted == true) {
                            Console.WriteLine("Please give me an integer index of the task you want to mark as completed.");
                        }
                        ToDoList[temp-1].IsCompleted = true;
                        PrintAllTasks(ToDoList);

                    }
                    else if (input == 5)
                    {
                        // R - Read/List Uncompleted Tasks
                        PrintNonCompletedTasks(ToDoList);
                    }
                    else if (input == 6)
                    {
                        // R - Read/List all Tasks
                        PrintAllTasks(ToDoList);
                    }
                    else if (input == 7)
                    {
                        // Q - Quit
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("User did not enter a a valid int!");

                    }
                    PrintMenu();
                    input = int.Parse(Console.ReadLine());

                }
            }
            else
            {
                Console.WriteLine("User did not specify a a valid int!");
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine("Task Management Menu");
            Console.WriteLine("1. Create a new task");
            Console.WriteLine("2. Delete an existing task");
            Console.WriteLine("3. Edit an existing task");
            Console.WriteLine("4. Complete a task");
            Console.WriteLine("5. List all outstanding(not complete) tasks");
            Console.WriteLine("6. List all tasks");
            Console.WriteLine("7. Exit");
            Console.Write("Please choose an option from the menu: ");
        }

        public static void PrintAllTasks(List<ToDo> ToDoList)
        {
            if (!ToDoList.Any())
            {
                Console.WriteLine("The list is empty");
            }
            else
            {
                for (int index = 0; index < ToDoList.Count(); index++)
                {
                    Console.WriteLine((index + 1) + ") " + ToDoList[index].ToString());
                }
            }
        }

        public static void PrintNonCompletedTasks(List<ToDo> ToDoList)
        {
            int hasTasksCompletedCount = 0;
            bool firstIndex = false;
            if (!ToDoList.Any())
            {
                Console.WriteLine("The list is empty");
            }
            else
            {
                for (int index = 0; index < ToDoList.Count(); index++)
                {
                    if (!ToDoList[index].IsCompleted)
                    {
                        Console.WriteLine((index + 1) + ") " + ToDoList[index].ToString());
                        hasTasksCompletedCount++;
                    }
                }
            }
            if (hasTasksCompletedCount != ToDoList.Count())
            {
                Console.WriteLine("You have completed all tasks.");
            }
        }
    }
}