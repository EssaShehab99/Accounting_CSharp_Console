using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApplication1
{
    class Customers
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public Customers(int user_id)
        {
            con.Close();
            Console.Clear();
            chosen(user_id);
        }
        private void chosen(int user_id)
        {
            con.Close();
            Console.Clear();
            int test1 = 0;
            Console.WriteLine("1- Add New customer");
            Console.WriteLine("2- Update customer");
            Console.WriteLine("3- Delete customer");
            Console.WriteLine("4- Show customers");
            Console.WriteLine("5- Back");
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out test1) && test1 <= 5)
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
            switch (test1)
            {
                case 1:
                    addcustomer(user_id);
                    break;
                case 2:
                    editcustomer(user_id);
                    break;
                case 3:
                    deletecustomer(user_id);
                    break;
                case 4:
                    printcustomer(user_id);
                    break;
            }
        }
        private void addcustomer(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter name of customer");
            string namecustomer = Console.ReadLine();
            Console.WriteLine("Enter balance to customer");
            decimal balance = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (decimal.TryParse(Input, out balance))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter balance to customer");
                }


            }
            Valid = false;
            int test = 0;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select customer_name from customers", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (namecustomer == (dr[0]).ToString())
                    {
                        Console.WriteLine("--Customer name already exists--");
                        Console.WriteLine("Press any key to back");
                        Console.ReadKey();
                        test = 1;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error");

            }
            finally
            {
                con.Close();
            }
            if (test == 0)
            {
                test = 1;
                SqlCommand cmd2 = new SqlCommand("insert into customers(customer_name,amount_creditor,amount_debit,date,user_id)values('" + namecustomer + "'," + balance + ",0,'" + DateTime.Now + "'," + user_id + ")", con);
                try
                {
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    Console.WriteLine("New customer was added press any key");
                    Console.ReadKey();
                    con.Close();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Erorr1");
                    Console.ReadKey();
                }
                finally
                {
                    con.Close();
                }
            }
            else
                chosen(user_id);
        }
        private void editcustomer(int user_id)
        {
            con.Close();
            int test1 = 0;
            Console.WriteLine("Enter number of customer");
            int id = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out id))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter number of customer");
                }
            }
            Valid = false;
            Console.WriteLine("1- Update Name customer");
            Console.WriteLine("2- Update balance customer");
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out test1) && test1 <= 3)
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
            switch (test1)
            {
                case 1:
                    Console.WriteLine("Enter name of customer");
                    string newnamecustomer = Console.ReadLine();
                    int test = 0;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select customer_name from customers", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (newnamecustomer == (dr[0]).ToString())
                    {
                        Console.WriteLine("--Customer name already exists--");
                        Console.WriteLine("Press any key to re-enter");
                        Console.ReadKey();
                        test = 1;
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error");

            }
            finally
            {
                con.Close();
            }
            if (test == 0)
            {
                SqlCommand cmd1 = new SqlCommand("update customers set customer_name=N'" + newnamecustomer + "' where id=" + id + "", con);
                try
                {
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("The customer was updated press any key");
                    Console.ReadKey();
                    con.Close();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Erorr2");
                    Console.ReadKey();

                }
                finally
                {
                    con.Close();
                }
            }
            else
                editcustomer(user_id);
                    break;
                case 2:
                    Console.WriteLine("Enter balance of customer");
                    decimal newbalancecustomer = 0;
                    while (Valid == false)
                    {
                        string Input = Console.ReadLine();
                        if (decimal.TryParse(Input, out newbalancecustomer))
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Enter balance to customer");
                        }
                    }
                    Valid = false;
                    SqlCommand cmd2 = new SqlCommand("update customers set amount_creditor=" + newbalancecustomer + " where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("customer updated press any key");
                        Console.ReadKey();
                        con.Close();
                        chosen(user_id);
                    }
                    catch
                    {
                        Console.WriteLine("Erorr3");
                        Console.ReadKey();
                    }
                    finally
                    {
                        con.Close();
                    }
                    break;
            }
        }
        private void deletecustomer(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter number of customer");
            int iddeleted = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out iddeleted))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter number of customer");
                }
            }
            Valid = false;
            Console.WriteLine("Enter yes to continue or no to cancel: ");
            string sure = "";
            while (Valid == false)
            {
                sure = Console.ReadLine();
                if (sure == "yes" || sure == "no")
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter yes to continue or no to cancel: ");
                }
            }
            Valid = false;
            if (sure == "yes")
            {
                SqlCommand cmd4 = new SqlCommand("delete customers where id=" + iddeleted + "", con);
                try
                {
                    con.Open();
                    cmd4.ExecuteNonQuery();
                    Console.WriteLine("Customer deleted press any key");
                    Console.ReadKey();
                    con.Close();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Erorr6");
                    Console.ReadKey();

                }
                finally
                {
                    con.Close();
                }
            }
            else if (sure == "no")
            {
                Console.WriteLine("The operation was canceled press any key");
                Console.ReadKey();
                con.Close();
                chosen(user_id);
            }

        }
        private void printcustomer(int user_id)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select c.id,c.customer_name,isnull(c.amount_creditor,0)-isnull(c.amount_debit,0)'amount',c.date,u.user_name from customers c join users u on(c.user_id=u.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    Console.WriteLine("____________________________________________________________________");
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of Customer: ", dr1["id"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of customer: ", dr1["customer_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Amount of customer: ", dr1["amount"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Date of customer: ", dr1["date"].ToString(), " ", " "));
                    Console.WriteLine("____________________________________________________________________");
                }
                dr1.Close();
                Console.WriteLine("Press yes to export invoice or no to back");
                string exporttext3 = "";
                    while (Valid == false)
                    {
                        exporttext3 = Console.ReadLine();
                        if (exporttext3 == "yes" || exporttext3 == "no")
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Press yes to export invoice or no to back");
                        }
                    }
                    Valid = false;
                    if (exporttext3 == "yes")
                    {
                        con.Close();
                        exportcustomer();
                        chosen(user_id);
                    }
                    else if (exporttext3 == "no")
                    {
                        con.Close();
                        chosen(user_id);
                    }
                Console.ReadKey();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        private void exportcustomer()
        {
            con.Close();
            string fileName = @"E:\Customers.txt";
            try
            {
                StreamWriter writeinvoicr = new StreamWriter(fileName, true);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select c.id,c.customer_name,isnull(c.amount_creditor,0)-isnull(c.amount_debit,0)'amount',c.date,u.user_name from customers c join users u on(c.user_id=u.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    writeinvoicr.WriteLine("____________________________________________________________________");
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of Customer: ", dr1["id"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of customer: ", dr1["customer_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Amount of customer: ", dr1["amount"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Date of customer: ", dr1["date"].ToString(), " ", " "));
                    writeinvoicr.WriteLine("____________________________________________________________________");
                }
                writeinvoicr.Close();
                dr1.Close();
                Console.WriteLine("Press any key to back");
                Console.ReadKey();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

    }
}
