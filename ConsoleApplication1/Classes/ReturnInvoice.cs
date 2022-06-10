using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class ReturnInvoice
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public ReturnInvoice()
        {
            Console.Clear();
            int i = 0;
            List<int> quantityitems = new List<int>();
            List<int> productid = new List<int>();
            Console.WriteLine("Plesae Enter Number Of Invoice: ");
            int numberinvoice = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out numberinvoice))
                {
                    Valid = true;
                }
            }
            Valid = false;
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
                SqlCommand cmd1 = new SqlCommand("select number_items, product_id from items where Invoice_id=" + numberinvoice + "", con);
                SqlCommand cmd2 = new SqlCommand("select total_price from Invoices where id=" + numberinvoice + "", con);
                SqlCommand cmd5 = new SqlCommand("select customer_id from Invoices where id=" + numberinvoice + "", con);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        quantityitems.Add(int.Parse(dr["number_items"].ToString()));
                        productid.Add(int.Parse(dr["product_id"].ToString()));
                    }
                    dr.Close();
                    con.Close();
                    foreach (int n in productid)
                    {
                        con.Open();
                        SqlCommand cmd3 = new SqlCommand("select quantity from products where id=" + n + "", con);
                        decimal backquantity = decimal.Parse(cmd3.ExecuteScalar().ToString()) + quantityitems[i];
                        SqlCommand cmd4 = new SqlCommand("update products set quantity=" + backquantity + " where id=" + n + "", con);
                        cmd4.ExecuteNonQuery();
                        con.Close();
                        i++;
                    }

                    con.Open();
                    SqlCommand cmd6 = new SqlCommand("select amount_debit from customers where id=" + cmd5.ExecuteScalar() + "", con);
                    decimal backamount = decimal.Parse(cmd6.ExecuteScalar().ToString()) - decimal.Parse(cmd2.ExecuteScalar().ToString());
                    SqlCommand cmd7 = new SqlCommand("update customers set amount_debit=" + backamount + " where id=" + cmd5.ExecuteScalar() + "", con);
                    SqlCommand cmd8 = new SqlCommand("delete Invoices where id=" + numberinvoice + "", con);
                    SqlCommand cmd9 = new SqlCommand("delete items where Invoice_id=" + numberinvoice + "", con);
                    cmd7.ExecuteNonQuery();
                    cmd9.ExecuteNonQuery();
                    cmd8.ExecuteNonQuery();
                    Console.WriteLine("Invoice deleted press any key");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            else if (sure == "no")
            {
            }
        }
    }
}