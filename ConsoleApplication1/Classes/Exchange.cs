using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace ConsoleApplication1
{
    class Exchange
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public Exchange(int user_id)
        {
            chosen(user_id);
        }
        private void chosen(int user_id)
        {
            con.Close();
            Console.Clear();
            int test1 = 0;
            Console.WriteLine("1- Add New exchange");
            Console.WriteLine("2- Delete exchange");
            Console.WriteLine("3- Show exchange");
            Console.WriteLine("4- Back");
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out test1) && test1 <= 4)
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
                    exchange(user_id);
                    break;
                case 2:
                    deleteexchange(user_id);
                    break;
                case 3:
                    printexchange(user_id);
                    break;
            }
        }
        private void exchange(int user_id)
        {
            Console.WriteLine("Enter number account sender: ");
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
                    Console.WriteLine("Enter number account sender: ");
                }
            }
            Valid = false;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select customer_name from customers where id=" + id + "", con);
                if (cmd1.ExecuteScalar().ToString() != "")
                {
                    Console.WriteLine("Name of account is -- " + cmd1.ExecuteScalar().ToString() + " --");
                }
                else
                {
                    chosen(user_id);
                }
            }
            catch
            {
                Console.WriteLine("--account doesn't exists--");
                Console.WriteLine("Press any key to back");
                Console.ReadKey();
                con.Close();
                chosen(user_id);

            }
            finally
            {
                con.Close();
            }
            Console.WriteLine("Enter number account receiver: ");
            int id2 = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out id2))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter number account receiver: ");
                }
            }
            Valid = false;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select customer_name from customers where id=" + id2 + "", con);
                if (cmd1.ExecuteScalar().ToString() != "")
                {
                    Console.WriteLine("Name of account is -- " + cmd1.ExecuteScalar().ToString() + " --");
                }
                else
                {
                    chosen(user_id);
                }
            }
            catch
            {
                Console.WriteLine("--account doesn't exists--");
                Console.WriteLine("Press any key to back");
                Console.ReadKey();
                con.Close();
                chosen(user_id);

            }
            finally
            {
                con.Close();
            }
            Console.WriteLine("Enter amount of money: ");
            decimal amount = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (decimal.TryParse(Input, out amount))
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
            Console.WriteLine("Enter the details: ");
            string details = Console.ReadLine();
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
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("select isnull(amount_debit,0) from customers where id=" + id + "", con);
                    SqlCommand cmd2 = new SqlCommand("select isnull(amount_creditor,0) from customers where id=" + id2 + "", con);
                    decimal amountdebit = decimal.Parse(cmd1.ExecuteScalar().ToString()) + amount;
                    decimal amountcreditor = decimal.Parse(cmd2.ExecuteScalar().ToString()) + amount;
                    SqlCommand cmd3 = new SqlCommand("update customers set amount_debit=" + amountdebit + " where id=" + id + "", con);
                    SqlCommand cmd4 = new SqlCommand("update customers set amount_creditor=" + amountcreditor + " where id=" + id2 + "", con);
                    SqlCommand cmd5 = new SqlCommand("insert into exchange(account_from,account_to,amount,details,user_id,date) values(" + id + "," + id2 + "," + amount + ",N'" + details + "'," + user_id + ",'" + DateTime.Now + "')", con);
                    cmd3.ExecuteNonQuery();
                    cmd4.ExecuteNonQuery();
                    cmd5.ExecuteNonQuery();
                    Console.WriteLine("New exchange was added press any key");
                    Console.ReadKey();
                    con.Close();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Error");
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
        private void deleteexchange(int user_id)
        {
            Console.WriteLine("Enter number operation: ");
            int idex = 0;
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out idex))
                {
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter number operation: ");
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
                try
                {
                    backdata(idex);
                    SqlCommand cmd1 = new SqlCommand("delete exchange where id =" + idex + "", con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    Console.WriteLine("Exchange was deleted press any key to back");
                    Console.ReadKey();
                    con.Close();
                    chosen(user_id);
                }
                catch
                {
                    Console.WriteLine("Error");
                }
                finally
                {
                    con.Open();
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
        private void printexchange(int user_id)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select e.id,e.date,u.user_name,c1.customer_name'account_from',c2.customer_name'account_to',e.amount,e.details from exchange e join users u on(e.user_id=u.id) join customers c1 on(e.account_from=c1.id) join customers c2 on(e.account_to=c2.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    Console.WriteLine("____________________________________________________________________");
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of operation: ", dr1["id"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-18} |", "Name of account sender: ", dr1["account_from"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-16} |", "Name of account receiver: ", dr1["account_to"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-23} |", "Amount exchanged: ", dr1["amount"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-20} |", "Details of operation: ", dr1["details"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-23} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-16} |", "Date of enterd product: ", dr1["date"].ToString(), " ", " "));
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
                    exportexchange();
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
        private void exportexchange()
        {
            con.Close();
            string fileName = @"E:\Exchange.txt";
            try
            {
                StreamWriter writeinvoicr = new StreamWriter(fileName, true);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select e.id,e.date,u.user_name,c1.customer_name'account_from',c2.customer_name'account_to',e.amount,e.details from exchange e join users u on(e.user_id=u.id) join customers c1 on(e.account_from=c1.id) join customers c2 on(e.account_to=c2.id)", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    writeinvoicr.WriteLine("____________________________________________________________________");
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-21} |", "Number of operation: ", dr1["id"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-18} |", "Name of account sender: ", dr1["account_from"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-16} |", "Name of account receiver: ", dr1["account_to"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-23} |", "Amount exchanged: ", dr1["amount"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-20} |", "Details of operation: ", dr1["details"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-23} |", "User enterd: ", dr1["user_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-16} |", "Date of enterd product: ", dr1["date"].ToString(), " ", " "));
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
        private void backdata(int id)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select account_from from exchange where id=" + id +"", con);
                SqlCommand cmd2 = new SqlCommand("select account_to from exchange where id=" + id + "", con);
                SqlCommand cmd3 = new SqlCommand("select amount from exchange where id=" + id + "", con);
                SqlCommand cmd4 = new SqlCommand("select isnull(amount_debit,0) from customers where id=" + cmd1.ExecuteScalar().ToString() + "", con);
                SqlCommand cmd5 = new SqlCommand("select isnull(amount_creditor,0) from customers where id=" + cmd2.ExecuteScalar().ToString() + "", con);
                decimal amountdebit = decimal.Parse(cmd4.ExecuteScalar().ToString()) - Convert.ToDecimal(cmd3.ExecuteScalar().ToString());
                decimal amountcreditor = decimal.Parse(cmd5.ExecuteScalar().ToString()) - Convert.ToDecimal(cmd3.ExecuteScalar().ToString());
                Console.WriteLine(amountdebit);
                Console.WriteLine(amountcreditor);
                SqlCommand cmd6 = new SqlCommand("update customers set amount_debit=" + amountdebit + " where id=" + cmd1.ExecuteScalar().ToString() + "", con);
                SqlCommand cmd7 = new SqlCommand("update customers set amount_creditor=" + amountcreditor + " where id=" + cmd2.ExecuteScalar().ToString() + "", con);
                cmd6.ExecuteNonQuery();
                cmd7.ExecuteNonQuery();

            }
            catch
            {
                Console.WriteLine("Error");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
