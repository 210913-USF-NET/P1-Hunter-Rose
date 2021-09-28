using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DL;
using Microsoft.EntityFrameworkCore;
using DL.Entities;
using System.IO;
using Models;
using BL;

namespace UI
{
    public class MainMenu
    {
        public void start()
        {
            Signin:
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<RestaurantDBContext> options = new DbContextOptionsBuilder<RestaurantDBContext>().UseSqlServer(connectionString).Options;
            RestaurantDBContext context = new RestaurantDBContext(options);
            Console.WriteLine("Hello, Please sign in.");
            Console.WriteLine("[0] Sign in: ");
            Console.WriteLine("[1] Or if you don't have a username, sign up: ");
            Console.WriteLine("[x] Exit");
            string option = Console.ReadLine();
            DBRepo repo = new DBRepo(context);
            switch(option)
            {
                case "0":
                Console.WriteLine("Please enter your username(name): ");
                string name1 = Console.ReadLine();
                Models.Customer searchCustomer = new Models.Customer();
                searchCustomer.Name = name1;
                Models.Customer foundcustomer = repo.SearchCustomer(name1);
                if(foundcustomer != null)
                {
                        CurrentContext.CurrentCustomerId = foundcustomer.Id;
                        Console.WriteLine($"Hi {name1}, Welcome to our store!");
                        new StoreMenu(new BBBBL(new DBRepo(context)),new StoreService(),new CustomerService(), new InventoryService()).StoreMenu1();

                }
                else
                {
                    Console.WriteLine("We couldn't find that username");
                    goto Signin;
                }
                break;

                case "1":
                Console.WriteLine("Please enter your name (this will be your username): ");
                string username = Console.ReadLine();
                Models.Customer newCustomer = new Models.Customer();
                newCustomer.Name = username;
                repo.createCustomer(newCustomer);
                goto case "0";

                case "x":
                break;

                default:
                Console.WriteLine("invalid input");
                start();
                break;
            }

        }
    }
}