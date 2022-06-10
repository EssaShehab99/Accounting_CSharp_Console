using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApplication1
{
    class PrintInvoices
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public PrintInvoices(int user_id)
        {
            chosen(user_id);
        }

        private void chosen(int user_id)
        {
            Console.Clear();
            int test=0;
            List<int> idinvoice = new List<int>();
            Console.WriteLine("1- Print last invoice");
            Console.WriteLine("2- Print in number invoice");
            Console.WriteLine("3- Print all invoices");
            Console.WriteLine("4- Back");
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out test) && test <= 4)
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter your chosen: ");

                }


            }
            Valid = false;
            switch (test)
            {
                case 1:
                    Console.Clear();
                    returnid re = new returnid();
                    PrintInvoice(re.id("Invoices"), user_id);
                    Console.WriteLine("Press yes to export invoice");
                    string exporttext = Console.ReadLine();
                    if (exporttext == "yes")
                    {
                        exportInvoice(re.id("Invoices"), user_id);
                        Console.WriteLine("Oky");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Enter number of invoice: ");
                    int test2 = int.Parse(Console.ReadLine());
                    PrintInvoice(test2, user_id);
                    Console.WriteLine("Press yes to export invoice");
                    string exporttext2 = Console.ReadLine();
                    if (exporttext2 == "yes")
                    {
                        exportInvoice(test2, user_id);
                        Console.WriteLine("Oky");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    Console.Clear();

                    SqlCommand cmd1 = new SqlCommand("select id from Invoices", con);
                    try
                    {
                        con.Open();
                        SqlDataReader dr2 = cmd1.ExecuteReader();
                        while (dr2.Read())
                        {

                            idinvoice.Add(Convert.ToInt16(dr2["id"].ToString()));
                        }
                        dr2.Close();
                        con.Close();

                        foreach (int n in idinvoice)
                        {
                            PrintInvoice(n, user_id);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                    Console.WriteLine("Press yes to export invoice");
                    string exporttext3 = Console.ReadLine();
                    if (exporttext3 == "yes")
                    {

                        SqlCommand cmd2 = new SqlCommand("select id from Invoices", con);
                        try
                        {
                            con.Open();
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {

                                idinvoice.Add(Convert.ToInt16(dr2["id"].ToString()));
                            }
                            dr2.Close();
                            con.Close();

                            foreach (int n in idinvoice)
                            {
                                exportInvoice(n, user_id);
                            }


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            con.Close();
                        }
                        Console.WriteLine("Oky");
                        Console.ReadKey();
                    }
                    break;
            }
        }

        private void PrintInvoice(int id, int user_id)
        {
            decimal total = 0;
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select c.customer_name,i.date,i.id from customers c join Invoices i on(c.id=i.customer_id) where i.id=" + id + "", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                Console.WriteLine("____________________________________________________________________");
                Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Number of Invoice: ", dr1["id"].ToString(), " ", " "));
                Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of customer: ", dr1["customer_name"].ToString(), " ", " "));
                Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Date of invoice: ", dr1["date"].ToString(), " ", " "));

                dr1.Close();
                con.Close();

            }
            Console.WriteLine("|------------------------------------------------------------------|");
            Console.WriteLine(String.Format("|{0,-14} | {1,-13} | {2,-15} | {3,-15}|", "Item", "Number", "Price", "Total price"));
            Console.WriteLine("|------------------------------------------------------------------|");

            con.Open();
            SqlCommand cmd2 = new SqlCommand("select p.product_name,i.number_items,p.price,p.price*i.number_items 'total'from items i join products p on(i.product_id=p.id) where i.Invoice_id=" + id + "", con);
            SqlCommand cmd3 = new SqlCommand("select user_name from users where id=" + user_id + "", con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            int i = 1;
            while (dr2.Read())
            {
                Console.WriteLine(String.Format("|{0,-14} | {1,-13} | {2,-15} | {3,-15}|", dr2["product_name"].ToString(), dr2["number_items"].ToString(), dr2["price"].ToString(), dr2["total"].ToString()));
                total += Convert.ToDecimal(dr2["total"].ToString());
                i++;
            }
            dr1.Close();
            con.Close();
            con.Open();
            Console.WriteLine("|------------------------------------------------------------------|");
            Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-13} |", "The total price of invoice: ", total, " ", " "));
            Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "The user is: ", cmd3.ExecuteScalar().ToString(), " ", " "));
            Console.WriteLine("|__________________________________________________________________|");
            con.Close();

        }

        private void exportInvoice(int id, int user_id)
        {
            string fileName = @"E:\Mahesh.txt";
            try
            {
                StreamWriter writeinvoicr = new StreamWriter(fileName, true);
                decimal total = 0;
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select c.customer_name,i.date,i.id from customers c join Invoices i on(c.id=i.customer_id) where i.id=" + id + "", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    writeinvoicr.WriteLine("____________________________________________________________________");
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Number of Invoice: ", dr1["id"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of customer: ", dr1["customer_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Date of invoice: ", dr1["date"].ToString(), " ", " "));
                    dr1.Close();
                    con.Close();
                }
                writeinvoicr.WriteLine("|------------------------------------------------------------------|");
                writeinvoicr.WriteLine(String.Format("|{0,-14} | {1,-13} | {2,-15} | {3,-15}|", "Item", "Number", "Price", "Total price"));
                writeinvoicr.WriteLine("|------------------------------------------------------------------|");
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select p.product_name,i.number_items,p.price,p.price*i.number_items 'total'from items i join products p on(i.product_id=p.id) where i.Invoice_id=" + id + "", con);
                SqlCommand cmd3 = new SqlCommand("select user_name from users where id=" + user_id + "", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                int i = 1;
                while (dr2.Read())
                {
                    Console.WriteLine(String.Format("|{0,-14} | {1,-13} | {2,-15} | {3,-15}|", dr2["product_name"].ToString(), dr2["number_items"].ToString(), dr2["price"].ToString(), dr2["total"].ToString()));
                    total += Convert.ToDecimal(dr2["total"].ToString());
                    i++;
                }
                dr1.Close();
                con.Close();
                con.Open();
                writeinvoicr.WriteLine("|------------------------------------------------------------------|");
                writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-13} |", "The total price of invoice: ", total, " ", " "));
                writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "The user is: ", cmd3.ExecuteScalar().ToString(), " ", " "));
                writeinvoicr.WriteLine("|__________________________________________________________________|");
                con.Close();
                writeinvoicr.Close();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}