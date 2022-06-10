using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Main
    {
        public Main(int user_id,string user_type,string user_name)
        {
            while (true)
            {
                Console.Clear();
                int test = 0;
                Console.WriteLine("1- Register a customer request");
                Console.WriteLine("2- Print invoices");
                Console.WriteLine("3- Returns Register a Customer Request");
                if (user_type == "admin")
                {
                    Console.WriteLine("4- Manage Products");
                    Console.WriteLine("5- Manage Customers");
                    Console.WriteLine("6- Manage Users");
                }

                bool Valid = false;
                while (Valid == false)
                {
                    string Input = Console.ReadLine();
                    if (int.TryParse(Input, out test))
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("--Wrong Value--");
                        Console.WriteLine("Enter your chosen: ");

                    }


                }
                if (user_type == "admin")
                {
                    switch (test)
                    {
                        case 1:
                            Invoices ivc = new Invoices(user_id, user_type, user_name);
                            break;
                        case 2:
                            PrintInvoices prntinvice = new PrintInvoices(user_id);

                            break;
                        case 3:
                            ReturnInvoice reivc = new ReturnInvoice();
                            break;
                        case 4:
                            Products maprdct = new Products(user_id);
                            break;
                        case 5:
                            Customers cstmr = new Customers(user_id);
                            break;
                        case 6:
                            Users usr = new Users(user_id);
                            break;
                    }
                }
                else
                {
                    switch (test)
                    {
                        case 1:
                            Invoices ivc = new Invoices(user_id, user_type, user_name);
                            break;
                        case 2:
                            PrintInvoices prntinvice = new PrintInvoices(user_id);

                            break;
                        case 3:
                            ReturnInvoice reivc = new ReturnInvoice();
                            break;
                    }
                }
            }
        }
    }
}
