using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace ConsoleApplication1
{
    class Products
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public Products(int user_id)
        {
            con.Close();
            chosen(user_id);
        }
        private void chosen(int user_id)
        {
            con.Close();
            Console.Clear();
            int test1;
            Console.WriteLine("1- Add New Product");
            Console.WriteLine("2- Update Product");
            Console.WriteLine("3- Delete Product");
            Console.WriteLine("4- Show Product");
            Console.WriteLine("5- Back");
            test1 = 0;
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
                    addproduct(user_id);
                    break;
                case 2:
                    editproduct(user_id);
                    break;
                case 3:
                    deleteproduct(user_id);
                    break;
                case 4:
                    printproducts(user_id);
                    break;
                case 5:
                    chosen(user_id);
                    break;
            }
        }
        private void addproduct(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter name of product");
            string nameproduct = Console.ReadLine();
            Console.WriteLine("Enter quantity of product");
            int quantity = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out quantity))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter quantity of product: ");
                }
            }
            Valid = false;
            Console.WriteLine("Enter price of product: ");
            decimal price = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (decimal.TryParse(Input, out price))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Plesae Enter Price: ");
                }
            }
            Valid = false;
            Console.WriteLine("Enter expiry date of product");
            DateTime exp = DateTime.Now;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (DateTime.TryParse(Input, out exp))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter expiry date of product");
                }
            }
            Valid = false;
            SqlCommand cmd = new SqlCommand("insert into products(product_name,quantity,price,expiry_date,date,user_id)values('" + nameproduct + "'," + quantity + "," + price + ",'" + exp + "','" + DateTime.Now + "'," + user_id + ")", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("New product was added press any key to back");
                Console.ReadKey();
                chosen(user_id);
            }
            catch
            {
                Console.WriteLine("Erorr1");
            }
            finally
            {
                con.Close();
            }
        }
        private void editproduct(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter number of product");
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
                    Console.WriteLine("Enter number of product");
                }
            }
            Valid = false;
            Console.WriteLine("1- Update Name Product");
            Console.WriteLine("2- Update quantity Product");
            Console.WriteLine("3- Update price Product");
            Console.WriteLine("4- Update Expiry date Product");
            Console.WriteLine("5- Back");
            int test1 = 0;
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

                    Console.WriteLine("Enter name of product");
                    string newnameproduct = Console.ReadLine();
                    SqlCommand cmd1 = new SqlCommand("update products set product_name=N'" + newnameproduct + "' where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Product updated press any key to back");
                        Console.ReadKey();
                        chosen(user_id);
                    }
                    catch
                    {
                        Console.WriteLine("Erorr2");
                    }
                    finally
                    {
                        con.Close();
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter quantity of product");
                    int newquantityproduct = 0;
                    while (Valid == false)
                    {
                        string Input = Console.ReadLine();
                        if (int.TryParse(Input, out newquantityproduct))
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Enter quantity of product");
                        }
                    }
                    Valid = false;
                    SqlCommand cmd2 = new SqlCommand("update products set quantity=" + newquantityproduct + " where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Product updated press any key to back");
                        Console.ReadKey();
                        chosen(user_id);

                    }
                    catch
                    {
                        Console.WriteLine("Erorr3");
                    }
                    finally
                    {
                        con.Close();
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter price of product");
                    decimal newpriceproduct = 0;
                    while (Valid == false)
                    {
                        string Input = Console.ReadLine();
                        if (decimal.TryParse(Input, out newpriceproduct))
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Enter price of product");
                        }
                    }
                    Valid = false;
                    SqlCommand cmd3 = new SqlCommand("update products set price=" + newpriceproduct + " where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd3.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Product updated press any key to back");
                        Console.ReadKey();
                        chosen(user_id);
                    }
                    catch
                    {
                        Console.WriteLine("Erorr4");
                    }
                    finally
                    {
                        con.Close();
                    }
                    break;
                case 4:
                    Console.WriteLine("Enter Expiry date of product");
                    DateTime newexpiryproduct = DateTime.Now;
                    while (Valid == false)
                    {
                        string Input = Console.ReadLine();
                        if (DateTime.TryParse(Input, out newexpiryproduct))
                        {
                            Valid = true;
                        }
                        else
                        {
                            Console.WriteLine("--Wrong Value--");
                            Console.WriteLine("Enter Expiry date of product");
                        }
                    }
                    Valid = false;
                    SqlCommand cmd4 = new SqlCommand("update products set Expiry_date='" + newexpiryproduct + "' where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd4.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Product updated press any key to back");
                        Console.ReadKey();
                        chosen(user_id);
                    }
                    catch
                    {
                        Console.WriteLine("Erorr5");
                    }
                    finally
                    {
                        con.Close();
                    }
                    break;
                case 5:
                    chosen(user_id);
                    break;
            }
        }
        private void deleteproduct(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter number of product");
            int iddeleted = Convert.ToInt16(Console.ReadLine());
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
                    Console.WriteLine("Enter number of product");
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
                SqlCommand cmd4 = new SqlCommand("delete products where id=" + iddeleted + "", con);
                try
                {
                    con.Open();
                    cmd4.ExecuteNonQuery();
                    Console.WriteLine("The product was deleted press any key to back");
                    Console.ReadKey();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Erorr6");
                }
                finally
                {
                    con.Close();
                }
            }
            else if (sure == "no")
            {
                chosen(user_id);
            }
        }
        private void printproducts(int user_id)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select p.id,p.product_name,isnull(p.price,0)'price',p.quantity,p.Expiry_date,p.date,u.user_name from products p join users u on(p.user_id=u.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    Console.WriteLine("____________________________________________________________________");
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of product: ", dr1["id"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Name of product: ", dr1["product_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Price of product: ", dr1["price"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-19} |", "Quantity of product: ", dr1["quantity"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-14} |", "Expiry date of product: ", dr1["Expiry_date"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-15} |", "Date of enterd product: ", dr1["date"].ToString(), " ", " "));
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
                    exportproducts();
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
        private void exportproducts()
        {
            con.Close();
            string fileName = @"E:\Products.txt";
            try
            {
                StreamWriter writeinvoicr = new StreamWriter(fileName, true);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select p.id,p.product_name,isnull(p.price,0)'price',p.quantity,p.Expiry_date,p.date,u.user_name from products p join users u on(p.user_id=u.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    writeinvoicr.WriteLine("____________________________________________________________________");
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of product: ", dr1["id"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Name of product: ", dr1["product_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Price of product: ", dr1["price"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-19} |", "Quantity of product: ", dr1["quantity"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-14} |", "Expiry date of product: ", dr1["Expiry_date"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-15} |", "Date of enterd product: ", dr1["date"].ToString(), " ", " "));
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