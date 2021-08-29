using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            
            var Repo = new DapperProductRepository(conn); // product
            //foreach (var prod in products)
            //{
            //    Console.WriteLine($"ProductID: {prod.ProductID}, Name: {prod.Name}, CategoryID: {prod.CategoryID}"); //product
            //}
           // Repo.CreateProduct("New New StuFF", 37, 1); //product 

            Console.WriteLine("Hello, here are the current departments:");
            Console.WriteLine("Please press enter");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            var products = Repo.GetAllProducts(); //product 

            Print(depos);
            
            Console.WriteLine("Do you want to add a department?");
            var userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            //else if (userResponse.ToLower() == "Create Product")
            //{
            //    Console.WriteLine($"What is the name of your new product?");
            //   var userResponse1 = Console.ReadLine();
            //    Console.WriteLine("What is the price of your new product?");
            //   var userResponse2 = Console.ReadLine(double.Parse()); // need to parse
            //    Console.WriteLine("What is your CategoryID?");
            //    var userResponse3 = Console.ReadLine(); 
            //    Repo.CreateProduct($"{userResponse1}", { userResponse2}, { userResponse3}");
            //    Merch(Repo.GetAllProducts());
            //}
           
            Console.WriteLine("Have a great day!");
            Console.WriteLine("Here is the products section");
            Console.WriteLine("Press enter...");

            Console.ReadLine();
            Merch(products);

        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }
        }

        private static void Merch(IEnumerable<Product> prod)
        {
            foreach (var pro in prod)
            {
                Console.WriteLine($"ProductID: {pro.ProductID} Name: {pro.Name} Price: {pro.Price} CategoryID: {pro.CategoryID} Sale: {pro.OnSale} Level: {pro.StockLevel} ");

            }
        }
    }
}
