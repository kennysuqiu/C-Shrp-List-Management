using System;
using ListManagement.models;

namespace ListManagement // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var itemService = ItemService.Current;

            var nextToDo = new ToDo();

            Console.WriteLine("Welcome to the List Management App");
            PrintMenu();

            int input = -1;

            if (int.TryParse(Console.ReadLine(), out input))
            {
                while (input != 7)
                {
                    nextToDo = new ToDo();
                    // Option #1 - Create a task
                    // helloooo
                    if (input == 1)
                    {
                        string tempInput = "";
                        DateTime dateValue = DateTime.Today;
                        do
                        {
                            Console.WriteLine("Give me a name for the task you want to create: ");  // Make sure that name is not empty
                            tempInput = Console.ReadLine();
                            //Hello

                        } while (!tempInput.Any());
                        nextToDo.Name = tempInput;
                        Console.WriteLine($"Give me a description for the task '{nextToDo.Name}': ");
                        nextToDo.Description = Console.ReadLine()?.Trim();                                      
                        Console.WriteLine($"Give me a deadline in the format (MM/DD/YY) for the task '{nextToDo.Name}': "); // Ask for the deadline
                        while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                        {
                            Console.WriteLine($"Please give me a valid deadline for the task '{nextToDo.Name}': "); // Make sure that the deadline is valid and can be parsed as DateTime
                        }
                        nextToDo.Deadline = dateValue;
                        Console.WriteLine($"Task successfully added. Name: '{nextToDo.Name}' | Description: '{nextToDo.Description}' | " +
                        $"Deadline: '{nextToDo.Deadline.Month}/{nextToDo.Deadline.Day}/{nextToDo.Deadline.Year}'");
                        itemService.Add(nextToDo);     // Add the task to the To Do List
                    }
                    // Option #2 - Delete a task
                    else if (input == 2)
                    {
                        int index;
                        if (itemService.IsEmpty())
                        {
                            if (itemService.IsEmpty())
                            {
                                Console.WriteLine("The list is empty.");
                            }
                            else
                            {
                                itemService.Items.ForEach(Console.WriteLine);
                            }
                            Console.WriteLine("Please indicate the task you want to delete.");      // Ask user for the index of the task to be deleted
                            while (!int.TryParse(Console.ReadLine(), out index))
                            {
                                Console.WriteLine("Please provide a valid index for the task you want to delete."); // Make sure it's a valid integer
                            }
                            var selectedItem = itemService.Items[index - 1];
                            itemService.Remove(selectedItem);   // Delete the task in the list
                            Console.WriteLine("You have successfully deleted the indicated task.");
                        } else
                        {
                            Console.WriteLine("The list is empty.");
                        }
                        
                    }
                    // Option #3 - Edit a task
                    else if (input == 3)
                    {
                        int index;
                        int option;
                        string redo = "";
                        if (!itemService.IsEmpty())
                        {
                            do
                            {
                                if (itemService.IsEmpty())
                                {
                                    Console.WriteLine("The list is empty.");
                                }
                                else
                                {
                                    itemService.Items.ForEach(Console.WriteLine);
                                }
                                Console.WriteLine("Which task do you want to edit?");   // Ask for the index of the task to be edited
                                while (!int.TryParse(Console.ReadLine(), out index))
                                {
                                    Console.WriteLine("Please give me an integer index of the task you want to edit/update.");
                                }
                                // Print out options on what user wants to edit (name, description, deadline, complete/incomplete, or all)
                                Console.WriteLine("What do you want to edit?");
                                Console.WriteLine("1. Name");
                                Console.WriteLine("2. Description");
                                Console.WriteLine("3. Deadline");
                                Console.WriteLine("4. Complete/Incomplete");
                                Console.WriteLine("5. All of the above");
                                while (!int.TryParse(Console.ReadLine(), out option) || option > 5)
                                {
                                    Console.WriteLine("Please give me a valid option from the menu.");  // Obtain a valid integer index
                                }
                                if (option == 1)    // Choice #1 - Edit/Update Name only
                                {
                                    Console.WriteLine($"The name for task is '{itemService.Items[index - 1].Name}.'");
                                    Console.WriteLine("What would you like to change that to?");
                                    itemService.Items[index - 1].Name = Console.ReadLine();
                                    Console.WriteLine($"You have successfully updated the name of the task to '{itemService.Items[index - 1].Name}.'");
                                }
                                else if (option == 2)   // Choice #2 - Edit/Update Description only
                                {
                                    Console.WriteLine($"The description for task '{itemService.Items[index - 1].Name}' is '{itemService.Items[index - 1].Description}.'");
                                    Console.WriteLine("What would you like to change the description to?");
                                    itemService.Items[index - 1].Description = Console.ReadLine();
                                    Console.WriteLine($"You have successfully updated the description of the task '{itemService.Items[index - 1].Name}.'");
                                }
                                else if (option == 3)   // Choice #3 - Edit/Update deadline only
                                {
                                    DateTime dateValue;
                                    Console.WriteLine($"The deadline for task '{itemService.Items[index - 1].Name}' is '{(itemService.Items[index - 1] as ToDo).Deadline}.'");
                                    Console.WriteLine("What would you like to change the deadline to?");
                                    while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                                    {
                                        Console.WriteLine($"Please give me a valid deadline for the task '{itemService.Items[index - 1].Name}': ");
                                    }
                                    (itemService.Items[index - 1] as ToDo).Deadline = dateValue;
                                    Console.WriteLine($"You have successfully updated the deadline of the task '{itemService.Items[index - 1].Name}.'");
                                }
                                else if (option == 4)   // Choice #4 - Edit/Update the status of the task (complete/incomplete)
                                {
                                    Console.WriteLine($"The task '{itemService.Items[index - 1].Name}' is marked as '{(itemService.Items[index - 1] as ToDo).IsCompleted}.'");
                                    Console.WriteLine("Would you like to change that? (Y/N)");
                                    if (Console.ReadLine() == "Y")
                                    {
                                        (itemService.Items[index - 1] as ToDo).IsCompleted = !(itemService.Items[index - 1] as ToDo).IsCompleted;
                                    }
                                    else if (Console.ReadLine() == "N")
                                    {
                                        Console.WriteLine("No, then we will leave it as is.");
                                    }
                                    Console.WriteLine($"You have successfully updated the status of the task '{itemService.Items[index - 1].Name}.'");
                                }
                                else if (option == 5)   // Choice #5 - Edit/Update all of the items above
                                {
                                    Console.WriteLine($"The name for task is '{itemService.Items[index - 1].Name}.'");
                                    Console.WriteLine("What would you like to change that to?");
                                    itemService.Items[index - 1].Name = Console.ReadLine();
                                    Console.WriteLine($"The description for task '{itemService.Items[index - 1].Name}' is '{itemService.Items[index - 1].Description}.'");
                                    Console.WriteLine("What would you like to change the description to?");
                                    itemService.Items[index - 1].Description = Console.ReadLine();
                                    DateTime dateValue;
                                    Console.WriteLine($"The deadline for task '{itemService.Items[index - 1].Name}' is '{(itemService.Items[index - 1] as ToDo).Deadline}.'");
                                    Console.WriteLine("What would you like to change the deadline to?");
                                    while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                                    {
                                        Console.WriteLine($"Please give me a valid deadline for the task '{itemService.Items[index - 1].Name}': ");
                                    }
                                    (itemService.Items[index - 1] as ToDo).Deadline = dateValue;
                                    Console.WriteLine($"The task '{itemService.Items[index - 1].Name}' is marked as '{(itemService.Items[index - 1] as ToDo).IsCompleted}.'");
                                    Console.WriteLine("Would you like to change that? (Y/N)");
                                    if (Console.ReadLine() == "Y")
                                    {
                                        (itemService.Items[index - 1] as ToDo).IsCompleted = !((itemService.Items[index - 1] as ToDo).IsCompleted);
                                    }
                                    else if (Console.ReadLine() == "N")
                                    {
                                        Console.WriteLine("No, then we will leave it as is.");
                                    }
                                    Console.WriteLine($"You have successfully updated all of task '{itemService.Items[index - 1].Name}' fields");
                                }
                                Console.WriteLine("Would you like to edit something else? (Y/N)");
                                redo = Console.ReadLine();
                            } while (redo.Equals("Y", StringComparison.InvariantCultureIgnoreCase));
                        } else
                        {
                            Console.WriteLine("The list is empty.");
                        }
                    }
                    // Option #4 - Mark task as complete
                    else if (input == 4)
                    {
                        int temp;
                        if (itemService.IsEmpty())
                        {
                            Console.WriteLine("The list is empty.");
                        }
                        else
                        {
                            itemService.Items
                                .Where(i => !((i as ToDo)?.IsCompleted ?? true))
                                .ToList()
                                .ForEach(Console.WriteLine);

                        }

                        Console.WriteLine("Please give me the index of the task you want to mark as completed.");       // Ask the user for the index of the task to be marked as complete
                        while (!int.TryParse(Console.ReadLine(), out temp)) {
                            Console.WriteLine("Please give me an integer index of the task you want to mark as completed.");
                        }
                        (itemService.Items[temp - 1] as ToDo).IsCompleted = true;
                    }
                    // Option #5 - Display the tasks that are incomplete
                    else if (input == 5)
                    {
                        // R - Read/List Uncompleted Tasks
                        if (itemService.IsEmpty())
                        {
                            Console.WriteLine("The list is empty.");
                        }
                        else
                        {
                            itemService.Items
                                .Where(i => !((i as ToDo)?.IsCompleted ?? true))
                                .ToList()
                                .ForEach(Console.WriteLine);
                        }
                    }
                    // Option #6 - Display all of the tasks
                    else if (input == 6)
                    {
                        // R - Read/List all Tasks
                        if (itemService.IsEmpty())
                        {
                            Console.WriteLine("The list is empty.");
                        }
                        else
                        {
                            itemService.Items.ForEach(Console.WriteLine);
                        }
                    }
                    // Option #7 - Quit
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

        // Print the main menu
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

        // Returns true/false if the list is empty
        public static bool IsEmptyList(List<ToDo> ToDoList)
        {
            return !ToDoList.Any();
        }
    }
}