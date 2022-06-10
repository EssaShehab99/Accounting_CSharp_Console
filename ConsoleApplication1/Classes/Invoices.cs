using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Invoices
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        List<int> numberitem = new List<int>();
        List<int> quantityitem = new List<int>();
        List<decimal> priceitem = new List<decimal>();
        int i3 = 0;
        public Invoices(int user_id, string user_type, string user_name)
        {
            invoice(user_id, user_type,user_name);
        }
        private void additemtstolist(int user_id)
        {
            bool test1 = true;
            int number = 0;
            while (test1)
            {
                Console.WriteLine("Plesae Enter Number Of Item: ");
                while (Valid == false)
                {
                    string Input = Console.ReadLine();
                    if (int.TryParse(Input, out number))
                    {
                        Valid = true;
                    }
                }
                Valid = false;
                try
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT product_name FROM products where id=" + number + "", con);
                    con.Open();
                    if (cmd1.ExecuteScalar().ToString() != "")
                    {
                        numberitem.Add(number);
                        Console.WriteLine("Name of product is -- " + cmd1.ExecuteScalar().ToString() + " --");
                        test1 = false;
                    }
                }
                catch
                {
                    Console.WriteLine("product number is't correct");
                }
                finally
                {
                    con.Close();
                }
            }
            test1 = true;
            while (test1)
            {
                Console.WriteLine("Plesae Enter Price Of Item: ");
                while (Valid == false)
                {
                    string Input = Console.ReadLine();
                    if (int.TryParse(Input, out number))
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("--Wrong Value--");
                        Console.WriteLine("Plesae Enter Price Of Item: ");

                    }
                }
                Valid = false;
                try
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT price FROM products where id=" + numberitem[i3] + "", con);
                    con.Close();
                    con.Open();
                    if (decimal.Parse(cmd1.ExecuteScalar().ToString()) > number)
                    {
                        Console.WriteLine("Price of product is't correct");
                    }
                    else
                    {
                        priceitem.Add(number);
                        test1 = false;
                    }


                }
                catch
                {
                    Console.WriteLine("Erorr00");
                }
                finally
                {
                    con.Close();
                }
            }
            test1 = true;
            while (test1)
            {
                Console.WriteLine("Plesae Enter Quantity Of Item: ");
                while (Valid == false)
                {
                    string Input = Console.ReadLine();
                    if (int.TryParse(Input, out number))
                    {
                        Valid = true;
                    }
                    else
                    {
                        Console.WriteLine("--Wrong Value--");
                        Console.WriteLine("Plesae Enter Quantity Of Item: ");

                    }
                }
                Valid = false;
                try
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT quantity FROM products where id=" + numberitem[i3] + "", con);
                    con.Open();
                    if (int.Parse(cmd1.ExecuteScalar().ToString()) < number)
                    {
                        Console.WriteLine("Quantity of product is't enough");
                    }
                    else
                    {
                        quantityitem.Add(number);
                        test1 = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Erorr00");
                }
                finally
                {
                    con.Close();
                }
            }

        }
        private void invoice(int user_id, string user_type, string user_name)
        {
            Console.Clear();
            returnid re = new returnid();
           
            decimal total_price = 0;
            decimal amountdebit = 0, quantity = 0;
            int idcustomeritem = 0;

            bool test = true;
            bool test2 = true;
            int i3 = 0;
            DateTime dt = DateTime.Now;

            while (test)
            {
                additemtstolist(user_id);
                i3++;
                Console.WriteLine("Enter add to re-add or any key to cancel : ");
                string readd = Console.ReadLine();
                if (readd != "add")
                {
                    test = false;
                }
                
            }
            if (user_type == "admin")
            {
                while (test2)
                {
                    Console.WriteLine("Enter number of customer: ");
                    while (Valid == false)
                    {
                        string Input = Console.ReadLine();
                        if (int.TryParse(Input, out idcustomeritem))
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Enter number of customer: ");
                            Customers t = new Customers(user_id);


                        }
                    }
                    Valid = false;
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand("SELECT customer_name, isnull(amount_debit,0) FROM customers where id=" + idcustomeritem + "", con);
                        con.Open();
                        SqlDataReader dr2 = cmd1.ExecuteReader();
                        if (dr2.Read())
                        {
                            Console.WriteLine("Name of customer is -- " + dr2.GetValue(0).ToString() + " --");
                            amountdebit = Convert.ToDecimal(dr2.GetValue(1).ToString());
                            test2 = false;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Customer number is't correct");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                try
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT id, isnull(amount_debit,0) FROM customers where customer_name='" + user_name + "'", con);
                    con.Open();
                    SqlDataReader dr2 = cmd1.ExecuteReader();
                    if (dr2.Read())
                    {
                        Console.WriteLine("Name of account is -- " + user_name + " --");
                        idcustomeritem = int.Parse(dr2.GetValue(0).ToString());
                        amountdebit = Convert.ToDecimal(dr2.GetValue(1).ToString());
                        test2 = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Customer number is't correct");
                }
                finally
                {
                    con.Close();
                }
            }

            i3=0;
            foreach (decimal n in priceitem)
            {
                total_price += (quantityitem[i3] * n);
                i3++;
            }
            amountdebit += total_price;
            Console.WriteLine("Enter yes to continue or no to cancel");
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
                    Console.WriteLine("Enter yes to continue or no to cancel");
                }
            }
            Valid = false;
            if (sure == "yes")
            {
                SqlCommand cmd2 = new SqlCommand("insert into Invoices(customer_id,total_price,date,user_id)values(" + idcustomeritem + "," + total_price + ",'" + dt + "'," + user_id + ")", con);
                SqlCommand cmd4 = new SqlCommand("update customers set amount_debit=" + amountdebit + " where id=" + idcustomeritem + "", con);
                con.Open();
                cmd2.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                con.Close();
                i3=0;
                foreach (int n in numberitem)
                {
                    con.Open();
                    SqlCommand cmd5 = new SqlCommand("SELECT quantity FROM products where id=" + numberitem[i3] + "", con);
                    quantity = Convert.ToDecimal(cmd5.ExecuteScalar().ToString()) - quantityitem[i3];
                    SqlCommand cmd6 = new SqlCommand("update products set quantity=" + quantity + " where id=" + numberitem[i3] + "", con);
                    SqlCommand cmd3 = new SqlCommand("insert into items(product_id,Invoice_id,number_items,price_item)values(" + n + "," + re.id("Invoices") + "," + quantityitem[i3] + "," + priceitem[i3] + ")", con);
                    cmd6.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    i3++;
                }

            }
            else if (sure == "no")
            {
            }
        }
    }
}