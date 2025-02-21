using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{

    class Menu
    {
        private List<string> validOptions = ["1", "2", "3", "4", "5", "6"];

        private IProjectService _projectService;
        private IUserService _userService;
        private User? user = null;

        public Menu(IProjectService service, IUserService userService)
        {
            _projectService = service;
            _userService = userService;
        }


        public bool running = true;

        public void Run()
        {
            string selectedOption = PrintOptions();
            HandleOption(selectedOption);
        }
        private string PrintOptions()
        {
            if (user != null) {
                Console.WriteLine($"You are currently logged in as: {user.Username}");
            } else
            {
                Console.WriteLine("You are not logged in.");
            }
                Console.WriteLine("Hello Noob. Please select an option below: ");

            Console.WriteLine("1. List Projects");
            Console.WriteLine("2. Create New Project");
            Console.WriteLine("3. Edit Project Status");
            Console.WriteLine("4. Choose User");
            Console.WriteLine("5. Register User");
            Console.WriteLine("6. Exit");

            string selectedOption = Console.ReadLine() ?? "6";

            if (!this.validOptions.Contains(selectedOption))
            {
                Console.WriteLine("You selected an incorrect option. Please try again");
                this.PrintOptions();
            }

            return selectedOption;
        }

        private void HandleOption(string option)
        {
            switch(option)
            {
                case "1":
                    ListProjects();
                    break;
                case "2":
                    CreateProject();
                    break;
                case "3":
                    EditProjectStatus();
                    break;
                case "4":
                    ChooseUser();
                    break;
                case "5":
                    RegisterUser();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    break;
            }

        }

        private async void ListProjects()
        {

            var projects = await _projectService.GetAll();
            if (projects.Count == 0)
            {
                Console.WriteLine("No available projects, you idiot.");
                return;
            }

            Console.WriteLine("Current projects: ");
            projects.ForEach(project =>
            {
                Console.WriteLine($"Id: {project.Id} | Name: {project.Name} | Customer: {project.Customer} | Status: {project.Status}");

            });
        }

        private async void CreateProject()
        {
            if (this.user == null)
            {
                Console.WriteLine("You must be logged in to create a project.");
                ClickToContinue();
                return;
            }

            Console.WriteLine("Please enter project name: ");
            string? name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Invalid name. Try again.");
                ClickToContinue();
                return;
            }

            Console.WriteLine("Please enter customer name: ");
            string? customer = Console.ReadLine();
            if (customer == null)
            {
                Console.WriteLine("Invalid customer name. Try again.");
                ClickToContinue();
                return;
            }

            await _projectService.Add(name, customer, this.user);
        }

        private async void EditProjectStatus()
        {
            if (this.user == null)
            {
                Console.WriteLine("You must be logged in to edit a project.");
                ClickToContinue();
                return;
            }

            Console.WriteLine("Select the project you want to edit:");
            ListProjects();
            string projectInput = Console.ReadLine() ?? "";
            var success = int.TryParse(projectInput, out int projectId);
            if (success == false)
            {
                Console.WriteLine("Invalid input. Try again.");
                ClickToContinue();
                return;
            }

            Console.WriteLine("What would you like the new status to be?");
            string status = Console.ReadLine() ?? "";
            if (status == "")
            {
                Console.WriteLine("Invalid input. Try again.");
                ClickToContinue();
                return;
            }

            await _projectService.UpdateStatus(projectId, status);
        }

        private async void RegisterUser()
        {
            Console.WriteLine("Enter username: ");
            string? username = Console.ReadLine();

            if (username == null || username == "")
            {
                Console.WriteLine("Invalid username. Please submit a proper name");
                ClickToContinue();
                return;
            }

            var created = await _userService.Create(username);
            if (!created)
            {
                Console.WriteLine("Unable to create user, try again");
                ClickToContinue();
                return;
            }

            Console.WriteLine($"User {username} created.");
            ClickToContinue();
        }

        private async void ChooseUser()
        {
            var users = _userService.GetUsers();
            if (users.Count ==0 )
            {
                Console.WriteLine("There are no registered users. Please create one.");
                ClickToContinue();
                return;
            }

            Console.WriteLine("Users : ");
            users.ForEach(user =>
            {
                Console.WriteLine($"Id: {user.Id} | Name: {user.Username}");

            });

            Console.WriteLine("Which user do you want to login as?");
            string input = Console.ReadLine() ?? "";
            int parsed;
            if (!int.TryParse(input,out parsed))
            {
                Console.WriteLine("Invalid Option");
                ClickToContinue();
                return;
            }

            var user = await _userService.GetUser(parsed);
            if (user == null)
            {
                Console.WriteLine("Could not select the user. Try again");
                ClickToContinue();
                return;
            }

            this.user = user;
            Console.WriteLine($"Succesfully logged in as {user.Username}");
            ClickToContinue();
        }

        private static void ClickToContinue()
        {
            Console.WriteLine("Click any key to continue");
            Console.ReadKey();
        }
    }
}
