using FreshWorksDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshWorksDataStoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            Console.WriteLine("Welcome to FreshWorks Data Store");
            do
            {
                Console.WriteLine("1. Create\n2. Read\n3. Delete\n4. Print All\n5. Exit\nEnter your choice:");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter the key");
                            var key = Console.ReadLine();
                            Console.WriteLine("Enter the value");
                            var value = Console.ReadLine();
                            FWDataStore fds = new FWDataStore();
                            fds.Create(key, value);
                            Console.WriteLine("Records get created successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter the key");
                            var key = Console.ReadLine();
                            FWDataStore fds = new FWDataStore();
                            var value = fds.Read(key);
                            Console.WriteLine(value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Enter the key");
                            var key = Console.ReadLine();
                            FWDataStore fds = new FWDataStore();
                            fds.Delete(key);
                            Console.WriteLine("Record gets deleted successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            FWDataStore fds = new FWDataStore();
                            var records = fds.GetAll();
                            if (records != null && records.Count != 0)
                            {
                                foreach (var record in records)
                                    Console.WriteLine("Key: {0}, Value: {1}", record.Key, record.Value);
                            }
                            else
                                Console.WriteLine("Data store is empty");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            } while (choice != 5);
            if (choice == 5)
            {
                Environment.Exit(0);
            }
        }
    }
}
