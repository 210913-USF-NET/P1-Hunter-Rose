using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Model = Models;

namespace UI
{
    public class StoreMenu
    {
        private IBL _bl;    
        private StoreService _storeService;

        private CustomerService _customerService;

        private InventoryService _inventoryService;

        public StoreMenu(IBL bl, StoreService storeService, CustomerService customerService, InventoryService inventoryService):this()
        {
            _bl = bl;
            _storeService = storeService;
            _customerService = customerService;
            _inventoryService = inventoryService;
        }
        private DBRepo _repo;

        public StoreMenu()
        {
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<RestaurantDBContext> options = new DbContextOptionsBuilder<RestaurantDBContext>()
            .UseSqlServer(connectionString).Options;
            RestaurantDBContext context = new RestaurantDBContext(options);
            _repo = new DBRepo(context);
        }

        
        public void StoreMenu1()
        {
            selectanotheroption:
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\n");
            Console.WriteLine("[0] Shop");
            Console.WriteLine("[1] View store order history");
            Console.WriteLine("[2] Return to Main Menu");
            Console.WriteLine("[3] view customer order history");
            Console.WriteLine("[4] View store inventory");
            Console.WriteLine("[x] Exit");
            string input = Console.ReadLine();
            switch(input)
            {
            case "0":
            List<Models.Stores> listOfStores = _repo.StoreLocation();
            Models.Stores selectedStore = _storeService.SelectAStore("Which store is closest to you?", listOfStores);
            Models.CurrentContext.CurrentStoreId = selectedStore.Id;
                     PlaceAnOrder();
                     break;

                case "1":
                pleasepickstore:
                Console.WriteLine("Please enter your admin username");
                string name3 = Console.ReadLine();
                Models.Customer searchCustomer3 = new Models.Customer();
                searchCustomer3.Name = name3;
                Models.Customer foundcustomer3 = _repo.SearchCustomer(name3);
                if(searchCustomer3.Name == "AdminLogin")
                {
                        Models.CurrentContext.CurrentCustomerId = foundcustomer3.Id;
                        
                        Console.WriteLine($"You now have manager powers");
                }
                else
                {
                    Console.WriteLine("You do not have access to the order history of the store");
                    goto selectanotheroption;
                }
                List<Models.Stores> listOfStores2 = _repo.StoreLocation();
                Models.Stores selectedStore2 = _storeService.SelectAStore("Which store order history you like to check?", listOfStores2);
                Console.WriteLine("\n");
                Console.WriteLine($"You picked {selectedStore2.Name}");
                Models.CurrentContext.CurrentStoreId = selectedStore2.Id;
                int storeHomie = (int) selectedStore2.Id;
                if(selectedStore2.Id == 1)
                {
                    List<Models.OrderDetails> listOfOrders = _repo.OrderHistory();
                    Console.WriteLine($"Here is the order history for {listOfStores2[storeHomie-1].Name} at location {listOfStores2[storeHomie-1].Location}");
                    Console.WriteLine("\n");
                    for(int i = 0; i<listOfOrders.Count; i++)
                    {
                        Console.WriteLine(listOfOrders[i]);
                    }
                    goto selectanotheroption;
                }
                else if(selectedStore2.Id ==2)
                {
                    List<Models.OrderDetails> listOfOrders = _repo.OrderHistory();
                    Console.WriteLine($"Here is the order history for {listOfStores2[storeHomie-1].Name} at location {listOfStores2[storeHomie-1].Location}");
                    for(int i = 0; i<listOfOrders.Count; i++)
                    {
                        Console.WriteLine(listOfOrders[i]);
                    }
                    goto selectanotheroption;
                }
                else
                {
                    Console.WriteLine("incorrect input");
                    goto pleasepickstore;
                }
                

                case "2":
                new MainMenu().start();
                break;

                case "3":
                Console.WriteLine("Please enter your admin username");

                string name2 = Console.ReadLine();
                Models.Customer searchCustomer1 = new Models.Customer();
                searchCustomer1.Name = name2;
                Models.Customer foundcustomer1 = _repo.SearchCustomer(name2);
                if(searchCustomer1.Name == "AdminLogin")
                {
                        Models.CurrentContext.CurrentCustomerId = foundcustomer1.Id;
                        Console.WriteLine($"You now have manager powers");
                        Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("You do not have access to the order history of our customers");
                    goto selectanotheroption;
                }
                List<Models.Customer> customerList = _repo.ListOfCustomers();
                Models.Customer selectedCustomer = _customerService.SelectACustomer("Which customer's order history you like to check?", customerList);
                Models.CurrentContext.CurrentCustomerId = selectedCustomer.Id;
                int customerBuddy = selectedCustomer.Id;  
                List<Models.OrderDetails> listOfCustomerOrders = _repo.CustomerOrderHistory();
                Console.WriteLine($"Here is the order history for {customerList[customerBuddy-1].Name}");
                Console.WriteLine("\n");
                    for(int i = 0; i<listOfCustomerOrders.Count; i++)
                    {
                        Console.WriteLine(listOfCustomerOrders[i]);
                    }
                    if(listOfCustomerOrders.Count == 0)
                    {
                        Console.WriteLine("This customer has not placed an order yet :(");
                        goto selectanotheroption;
                    }
                    goto selectanotheroption;
                

                case "4":
                home:
                Console.WriteLine("Please enter your admin username");
                string name1 = Console.ReadLine();
                Models.Customer searchCustomer = new Models.Customer();
                searchCustomer.Name = name1;
                Models.Customer foundcustomer = _repo.SearchCustomer(name1);
                if(searchCustomer.Name == "AdminLogin")
                {
                        Models.CurrentContext.CurrentCustomerId = foundcustomer.Id;
                        Console.WriteLine($"You now have manager powers");
                        Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("You do not have access to the stores inventory");
                    goto selectanotheroption;
                }
                List<Models.Stores> listOfStores1 = _repo.StoreLocation();
                Console.WriteLine("\n");
                Models.Stores selectedStore1 = _storeService.SelectAStore("Which store inventory would you like to check?", listOfStores1);
                Models.CurrentContext.CurrentStoreId = selectedStore1.Id;
                int loco = selectedStore1.Id;
                var result = listOfStores1[loco - 1].Location;
                if(selectedStore1.Id == 1)
                {
                    Console.WriteLine($"Welcome to the {result} Location");
                    Models.Inventory quantity = new Models.Inventory();
                    quantity.StoresId = Models.CurrentContext.CurrentStoreId;
                    List<Models.Product> listOfProducts = _repo.ListOfProducts();
                    for(int i = 0; i<listOfProducts.Count; i++)
                    {
                        Console.WriteLine(listOfProducts[i]);
                    }
                    List<Models.Inventory> newcheck = _repo.GetInventory();
                    Models.Inventory selectedInventory = _inventoryService.SelectAnInventory("Please select an inventory", newcheck);
                    Models.CurrentContext.CurrentInventoryId = selectedInventory.Id;
                    restock:
                    Console.WriteLine("Restock? [0] Yes, [1] No");
                        string restock = Console.ReadLine();
                        if(restock == "0")
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine($"You are refilling {listOfProducts[(int)selectedInventory.ProductId-1].Name}. There are {selectedInventory.Quantity} left in stock");
                            Console.WriteLine("Please input how many you would like to add, or if you need to subtract some from the stock input -(however many)");
                            string stringrefill = Console.ReadLine();
                            int refill;  
                            if (!int.TryParse(stringrefill, out refill)) //if not a whole number input give an error
                            {
                                Console.WriteLine("Invalid input. Please input a number.");
                                goto restock;
                            }
                            int? final = (int)selectedInventory.Quantity + refill;
                            selectedInventory.Quantity = final;
                            selectedInventory.Date = DateTime.Now;
                            // Model.Inventory finalInventory = new Model.Inventory((int)selectedInventory.Quantity, (int) Models.CurrentContext.CurrentStoreId, (int) Models.CurrentContext.CurrentLineItemProductId, date);
                            _repo.UpdateInventory(selectedInventory);
                            Console.WriteLine($"{listOfProducts[(int)selectedInventory.ProductId-1].Name} now has {selectedInventory.Quantity} left in stock.");
                            goto selectanotheroption;
                        }
                        else if (restock == "1")
                        {
                            goto selectanotheroption;
                        }
                        else{
                            Console.WriteLine("Invalid input");
                            goto restock;
                        }
                   
                    }
                else if (selectedStore1.Id == 2)
                {
                    Console.WriteLine($"Welcome to the {result} Location");
                    Models.Inventory quantity = new Models.Inventory();
                    quantity.StoresId = Models.CurrentContext.CurrentStoreId;
                    List<Models.Product> listOfProducts = _repo.ListOfProducts();
                    for(int i = 0; i<listOfProducts.Count; i++)
                    {
                        Console.WriteLine(listOfProducts[i]);
                    }
                   List<Models.Inventory> newcheck = _repo.GetInventory();
                    Models.Inventory selectedInventory = _inventoryService.SelectAnInventory("Please select an inventory", newcheck);
                    Models.CurrentContext.CurrentInventoryId = selectedInventory.Id;
                    restock:
                    Console.WriteLine("Restock? [0] Yes, [1] No");
                        string restock = Console.ReadLine();
                        if(restock == "0")
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine($"You are refilling {listOfProducts[(int)selectedInventory.ProductId-1].Name}. There are {selectedInventory.Quantity} left in stock");
                            Console.WriteLine("Please input how many you would like to add, or if you need to subtract some from the stock input -(however many)");
                            string stringrefill = Console.ReadLine();
                            int refill;  
                            if (!int.TryParse(stringrefill, out refill)) //if not a whole number input give an error
                            {
                                Console.WriteLine("Invalid input. Please input a number.");
                                goto restock;
                            }
                            int? final = (int)selectedInventory.Quantity + refill;
                            selectedInventory.Quantity = final;
                            selectedInventory.Date = DateTime.Now;
                            // Model.Inventory finalInventory = new Model.Inventory((int)selectedInventory.Quantity, (int) Models.CurrentContext.CurrentStoreId, (int) Models.CurrentContext.CurrentLineItemProductId, date);
                            _repo.UpdateInventory(selectedInventory);
                            Console.WriteLine($"{listOfProducts[(int)selectedInventory.ProductId-1].Name} now has {selectedInventory.Quantity} left in stock.");
                            goto selectanotheroption;
                        }
                        else if (restock == "1")
                        {
                            goto selectanotheroption;
                        }
                        else{
                            Console.WriteLine("Invalid input");
                            goto restock;
                        }
                    }
                
                else{
                    Console.WriteLine("invalid input");
                    goto home;
                }
                case "x":
                break;

                default:
                Console.WriteLine("invalid input");
                goto selectanotheroption;
            }
        }
        public void PlaceAnOrder()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Here is the list of the current products we have");
            List<Models.Product> listOfProducts = _repo.ListOfProducts();
            for(int i = 0; i<listOfProducts.Count; i++)
            {
                Console.WriteLine(listOfProducts[i]);
            }
            cartoptions:
            Console.WriteLine("\n");
            Console.WriteLine("Would you like to purchase anything today?");
            Console.WriteLine("[0] Yes");
            Console.WriteLine("[1] No, please take me back to the store menu");
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                Models.OrderDetails newOrder = new Model.OrderDetails();
                newOrder.CustomerId = Models.CurrentContext.CurrentCustomerId;
                newOrder.StoreId = Models.CurrentContext.CurrentStoreId;
                newOrder.Date = DateTime.Now;
                newOrder = _repo.CreateNewOrder(newOrder);
                Models.CurrentContext.CurrentOrderId = newOrder.Id;
                
                cartInventory();
                break;

                case "1":
                StoreMenu1();
                break;

                default:
                Console.WriteLine("invalid input");
                goto cartoptions;
            }
        }
        public void cartInventory()
        {
                buyProduct:
                Console.WriteLine("What would you like to add to your cart today?");
                List<Models.Product> listOfProducts = _repo.ListOfProducts();
                for(int i = 0; i<listOfProducts.Count; i++)
                {
                Console.WriteLine(listOfProducts[i]);
                }
                purchasing:
                string stringcartinput = Console.ReadLine();
                int cartinput;
                if(!int.TryParse(stringcartinput, out cartinput))
                {
                    Console.WriteLine("invalid input");
                    goto buyProduct;
                }
                else if(cartinput > listOfProducts.Count || cartinput < 0)
                {
                    Console.WriteLine("invalid input");
                    goto buyProduct;
                }
                quantity:
                Models.LineItem newItem = new Models.LineItem();
                newItem.OrderitemsId = Models.CurrentContext.CurrentOrderId;
                newItem.ProductId = cartinput;
                Models.CurrentContext.CurrentLineItemProductId = newItem.ProductId;
                Console.WriteLine("How many would you like to purchase?");
                string stringcartinput1 = Console.ReadLine();
                int cartinput1;
                if(!int.TryParse(stringcartinput1, out cartinput1))
                {
                    Console.WriteLine("invalid input. Please try again.");
                    goto quantity;
                }
                else if (cartinput1 < 0)
                {
                    Console.WriteLine("quantity can not be negative");
                    goto quantity;
                }
                newItem.Quantity = cartinput1;
                Models.CurrentContext.CurrentLineItemQuantityId = newItem.Quantity;
                newItem = _repo.CheckOutList(newItem);
                Models.CurrentContext.CurrentLineItemId = newItem.Id;
                Console.WriteLine(newItem);
                Console.WriteLine("Would you like to checkout?");
                Console.WriteLine("[0] Yes");
                Console.WriteLine("[1] No, continue shopping");
                answer:
                string answer = Console.ReadLine();
                if(answer=="0")
                {
                List<Models.Inventory> remainingInventory = _repo.GetInventory();  
                   Models.Inventory selectedInventory = _inventoryService.SelectAnInventory1((int)Models.CurrentContext.CurrentLineItemProductId-1, remainingInventory);
                //    Models.CurrentContext.CurrentInventoryId = selectedInventory.Id;
                //    selectedInventory.StoresId = Models.CurrentContext.CurrentStoreId;
                //    selectedInventory.ProductId = Models.CurrentContext.CurrentLineItemProductId;
                   selectedInventory.Quantity = selectedInventory.Quantity - newItem.Quantity;
                //    selectedInventory.Date = DateTime.Now;
                   Console.WriteLine("\n");
                   if (selectedInventory.Quantity < 0)
                   {
                       Console.WriteLine("We do not have that amount in stock. Please select a different amount to purchase. Above is what we have in stock.");
                       goto purchasing;
                   }
                    _repo.UpdateInventory(selectedInventory);
                   Models.Product total = new Models.Product();
                   total.Id = Models.CurrentContext.CurrentLineItemProductId;
                   int something = (int) total.Id;
                   int? result = listOfProducts[something-1].Price * newItem.Quantity;
                   Console.WriteLine($"You have purchased {newItem.Quantity} {listOfProducts[something-1].Name}(s). Your total comes to ${result}");
                   Console.WriteLine($"There are now {selectedInventory.Quantity} {listOfProducts[something-1].Name}(s) left at this location.");
                   StoreMenu1();
                }
                else if(answer == "1")
                {
                    goto buyProduct;
                }
                else
                {
                    Console.WriteLine("incorrect input");
                    Console.WriteLine("Would you like to checkout?");
                    Console.WriteLine("[0] Yes");
                    Console.WriteLine("[1] No, continue shopping");
                    goto answer;
                }
                }
        }
    }
