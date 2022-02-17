using System;
using Library.ListManagement.services;
using Library.ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;

namespace ListManagement // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var itemService = ItemService.Current;

            Console.WriteLine("Welcome to the List Management App");

            int input = -1;

            while (input != 9)
            {
                PrintMenu();
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("User entered an invalid input. Please try again.");
                    continue;
                }
                // Option #1 - Create a task
                if (input == 1)
                {
                    var nextToDo = new ToDo();
                    string tempInput = "";
                    DateTime dateValue = DateTime.Today;
                    do
                    {
                        Console.WriteLine("Give me a name for the task you want to create: ");  // Make sure that name is not empty
                        tempInput = Console.ReadLine();

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
                    itemService.Save();

                }
                // Option #2 - Creating an appointment
                else if (input == 2)
                {
                    var nextAppointment = new Appointment();
                    string tempInput = "";
                    DateTime dateValue = DateTime.Today;
                    do
                    {
                        Console.WriteLine("Set a name for the appointment you want to create: ");  // Make sure that name is not empty
                        tempInput = Console.ReadLine();

                    } while (!tempInput.Any());
                    (nextAppointment as Appointment).Name = tempInput;
                    Console.WriteLine($"Set a description for the appointment '{nextAppointment.Name}': ");
                    (nextAppointment as Appointment).Description = Console.ReadLine()?.Trim();
                    Console.WriteLine($"Set a start date and time for '{nextAppointment.Name}': "); // Ask for the deadline
                    while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                    {
                        Console.WriteLine($"Please give me a valid deadline for the task '{nextAppointment.Name}': "); // Make sure that the deadline is valid and can be parsed as DateTime
                    }
                    (nextAppointment as Appointment).Start = dateValue;
                    Console.WriteLine($"Set an end date and time for '{nextAppointment.Name}': "); // Ask for the deadline
                    while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                    {
                        Console.WriteLine($"Please give me a valid end date and time for the appointment '{nextAppointment.Name}': "); // Make sure that the deadline is valid and can be parsed as DateTime
                    }
                    (nextAppointment as Appointment).End = dateValue;
                    Console.WriteLine("Who is attending the appointment? (E for Exit)");  // Make sure that name is not empty
                    do
                    {
                        tempInput = Console.ReadLine();
                        if (tempInput == "E")
                        {
                            break;
                        }
                        nextAppointment.Attendees.Add(tempInput);

                    } while (tempInput != "E");
                    Console.WriteLine($"Appointment successfully added. Name: '{nextAppointment.Name}' | Description: '{nextAppointment.Description}'");
                    itemService.Add(nextAppointment as Appointment);
                    itemService.Save();

                }
                // Option #3 - Delete a task
                else if (input == 3)
                {
                    int index;

                    if (!itemService.Items.Any())
                    {
                        Console.WriteLine("The list is empty.");
                        continue;
                    }

                    itemService.Items.ForEach(Console.WriteLine);

                    Console.WriteLine("Please indicate the task you want to delete.");      // Ask user for the index of the task to be deleted
                    while (!int.TryParse(Console.ReadLine(), out index))
                    {
                        Console.WriteLine("Please provide a valid index for the task you want to delete."); // Make sure it's a valid integer
                    }
                    var selectedItem = itemService.Items.FirstOrDefault(i => i.Id == index);
                    if (selectedItem != default(Item))
                    {
                        itemService.Remove(selectedItem);
                        Console.WriteLine("You have successfully deleted the indicated task.");
                    }
                    itemService.Save();

                }
                // Option #3 - Edit a task
                else if (input == 4)
                {
                    int index = 0;
                    string tempInput;
                    if (itemService.Items.Any())
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
                    var editingItem = itemService.Items[index-1];
                    DateTime dateValue;
                    if (editingItem is Appointment apt)
                    {
                        Console.WriteLine("Set the new name:");
                        apt.Name = Console.ReadLine();
                        Console.WriteLine("Set the new description:");
                        apt.Description = Console.ReadLine();
                        Console.WriteLine("Set the new start date:");
                        while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                        {
                            Console.WriteLine($"Please give me a valid end date and time for the appointment: "); // Make sure that the deadline is valid and can be parsed as DateTime
                        }
                        apt.Start = dateValue;
                        Console.WriteLine("Set the new end date:");
                        while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                        {
                            Console.WriteLine($"Please give me a valid end date and time for the appointment: "); // Make sure that the deadline is valid and can be parsed as DateTime
                        }
                        apt.End = dateValue;
                        Console.WriteLine("Set the new attendees");
                        apt.Attendees.Clear();
                        do
                        {
                            tempInput = Console.ReadLine();
                            if (tempInput == "E")
                            {
                                break;
                            }
                            apt.Attendees.Add(tempInput);
                        } while (tempInput != "E");
                        Console.WriteLine($"Appointment {apt.Name} has been updated");
                    } else if (editingItem is ToDo task)
                    {
                        Console.WriteLine("Set the new task name:");
                        task.Name = Console.ReadLine();
                        Console.WriteLine("Set the new description:");
                        task.Description = Console.ReadLine();
                        Console.WriteLine("Set the new deadline:");
                        while (!DateTime.TryParse(Console.ReadLine(), out dateValue))
                        {
                            Console.WriteLine($"Please give me a valid end date and time for the appointment: "); // Make sure that the deadline is valid and can be parsed as DateTime
                        }
                        task.Deadline = dateValue;
                        Console.WriteLine("Set complete or incomplete (C/I)");
                        do
                        {
                            Console.WriteLine("Input C for Complete or I for Incomplete");  // Make sure that name is not empty
                            tempInput = Console.ReadLine();

                        } while (tempInput != "C" || tempInput != "I");
                        if (tempInput == "C")
                        {
                            task.IsCompleted = true;
                        } else if (tempInput == "I")
                        {
                            task.IsCompleted = false;
                        }
                        Console.WriteLine("Task successfully updated");
                    }
                }
                // Option #4 - Mark task as complete
                else if (input == 5)
                {
                    int temp;
                    if (!itemService.FilteredItems.Any())
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
                    while (!int.TryParse(Console.ReadLine(), out temp))
                    {
                        Console.WriteLine("Please give me an integer index of the task you want to mark as completed.");
                    }
                    (itemService.Items[temp - 1] as ToDo).IsCompleted = true;
                    itemService.Save();
                }
                // Option #6 - Display the tasks that are incomplete
                else if (input == 6)
                {
                    itemService.ShowComplete = false;
                    var userSelection = string.Empty;
                    if (itemService.FilteredItems.Count() <= 0)
                    {
                        Console.WriteLine("Your list empty.");
                        continue;
                    }
                    while (userSelection != "E")
                    {
                        foreach (var item in itemService.GetPage())
                        {
                            Console.WriteLine(item);
                        }
                        userSelection = Console.ReadLine();
                        if (userSelection == "N")
                        {
                            itemService.NextPage();
                        }
                        else if (userSelection == "P")
                        {
                            itemService.PreviousPage();
                        }
                    }
                }
                // Option #7 - Display all of the tasks
                else if (input == 7)
                {
                    itemService.ShowComplete = true;
                    var userSelection = string.Empty;
/*                    if (itemService.Items.Count <= 0)
                    {
                        Console.WriteLine("Your list empty.");
                        continue;
                    }*/
                    while (userSelection != "E")
                    {
                        foreach (var item in itemService.GetPage())
                        {
                            Console.WriteLine(item);
                        }
                        userSelection = Console.ReadLine();
                        if (userSelection == "N")
                        {
                            itemService.NextPage();
                        }
                        else if (userSelection == "P")
                        {
                            itemService.PreviousPage();
                        }
                    }

                }
                // Option #8 - Search
                else if (input == 8)
                {
                    /*Add a new menu item that asks the user for a search string and returns only items in the items list that contain that 
                     * search string in their name, description, or list of attendees (for Appointments). HINT: use LINQ to satisfy this requirement most easily.*/
                    /*                    string searchString;
                                        do
                                        {
                                            Console.WriteLine("Give me an input to search for:");  // Make sure that name is not empty
                                            searchString = Console.ReadLine();

                                        } while (!searchString.Any());*/
                    /*                    itemService.ShowComplete = true;
                    */
                    Console.WriteLine("What do you want to search for?");
                    itemService.Query = Console.ReadLine();
                    var userSelection = string.Empty;
                    if (itemService.FilteredItems.Count() <= 0)
                    {
                        Console.WriteLine("Your list empty.");
                        continue;
                    }
                    while (userSelection != "E")
                    {
                        foreach (var item in itemService.GetPage())
                        {
                            Console.WriteLine(item);
                        }
                        userSelection = Console.ReadLine();
                        if (userSelection == "N")
                        {
                            itemService.NextPage();
                        }
                        else if (userSelection == "P")
                        {
                            itemService.PreviousPage();
                        }
                    }
/*                    itemService.ShowComplete = false;
*/                }
                // Option #9 - Quit
                else if (input == 9)
                {
                    // Q - Quit
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("User did not enter a a valid int!");
                }
            }

        }

        // Print the main menu
        public static void PrintMenu()
        {
            Console.WriteLine("Task Management Menu");
            Console.WriteLine("1. Create a new task");
            Console.WriteLine("2. Create a new appointment");
            Console.WriteLine("3. Delete an existing task or appointment");
            Console.WriteLine("4. Edit an existing task");
            Console.WriteLine("5. Complete a task");
            Console.WriteLine("6. List all outstanding (not complete) tasks");
            Console.WriteLine("7. List all tasks");
            Console.WriteLine("8. Search");
            Console.WriteLine("9. Exit");
            Console.Write("Please choose an option from the menu: ");
        }
    }
}