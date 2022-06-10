using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApplication1
{
    class Users
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        bool Valid = false;
        public Users(int user_id)
        {
            chosen(user_id);
        }
        private void chosen(int user_id)
        {
            con.Close();
            Console.Clear();
            Console.WriteLine("1- Add New user");
            Console.WriteLine("2- Update user");
            Console.WriteLine("3- Delete user");
            Console.WriteLine("4- Show user");
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
                    adduser(user_id);
                    break;
                case 2:
                    edituser(user_id);
                    break;
                case 3:
                    deleteuser(user_id);
                    break;
                case 4:
                    printuser(user_id);
                    break;
            }
        }
        private void adduser(int user_id)
        {
            con.Close();
            int test = 0;
            Console.WriteLine("Enter name of user: ");
            string nameuser = Console.ReadLine();
            Console.WriteLine("Enter password of user: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter type of user: ");
            string usertype = "";
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (Input == "admin" || Input == "user")
                {
                    usertype = Input;
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter type of user: ");
                }
            }
            Valid = false;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select user_name from users", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (nameuser == (dr[0]).ToString())
                    {
                        Console.WriteLine("--User name already exists--");
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
                SqlCommand cmd1 = new SqlCommand("insert into users(user_name,user_password,user_type) values(N'" + nameuser + "',N'" + password + "',N'" + usertype + "')", con);
                SqlCommand cmd2 = new SqlCommand("insert into customers(customer_name,amount_creditor,amount_debit,date,user_id)values('" + nameuser + "',0,0,'" + DateTime.Now + "'," + user_id + ")", con);
                try
                {
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    Console.WriteLine("New user was added press any key");
                    Console.ReadKey();
                    chosen(user_id);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                    Console.ReadKey();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                chosen(user_id);
            }
        }
        private void edituser(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter number of usre: ");
            int id = 0;
            int test1 = 0;
            string user_name = "";
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
                    Console.WriteLine("Enter number of user");
                }
            }
            Valid = false;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select user_name from users where id=" + id + "", con);
                if (cmd1.ExecuteScalar().ToString() != "")
                {
                    Console.WriteLine("Name of user is -- " + cmd1.ExecuteScalar().ToString() + " --");
                    user_name = cmd1.ExecuteScalar().ToString();
                }
                else
                {
                    con.Close();
                    chosen(user_id);
                }
            }
            catch
            {
                Console.WriteLine("--User doesn't exists--");
                Console.WriteLine("Press any key to back");
                Console.ReadKey();
                con.Close();
                chosen(user_id);

            }
            finally
            {
                con.Close();
            }
            Console.WriteLine("1- Update username usre: ");
            Console.WriteLine("2- Update password usre");
            Console.WriteLine("3- Update type usre");
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
                    Console.WriteLine("Enter username of usre");
                    string newusernameuser = Console.ReadLine();
                    int test=0;
                    try
                    {
                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select user_name from users", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (newusernameuser == (dr[0]).ToString())
                            {
                                Console.WriteLine("--User name already exists--");
                                Console.WriteLine("Press any key to back");
                                Console.ReadKey();
                                test = 1;
                                con.Close();
                                chosen(user_id);
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
                        SqlCommand cmd1 = new SqlCommand("update users set user_name=N'" + newusernameuser + "' where id=" + id + "", con);
                        SqlCommand cmd2 = new SqlCommand("Update customers set customer_name=N'" + newusernameuser + "'where customer_name=N'" + user_name + "'", con);
                        try
                        {
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            Console.WriteLine("Name usre was updated press any key");
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
                    break;
                case 2:
                    Console.WriteLine("Enter password of usre");
                    string newpassworduser = Console.ReadLine();
                    SqlCommand cmd3 = new SqlCommand("update users set user_password=N'" + newpassworduser + "' where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd3.ExecuteNonQuery();
                        Console.WriteLine("Password usre was updated press any key");
                        Console.ReadKey();
                        con.Close();
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
                case 3:
                      Console.WriteLine("Enter type of user: ");
            string usertype = "";
            while (Valid == false)
            {
                string Input = Console.ReadLine();
                if (Input == "admin" || Input == "user")
                {
                    usertype = Input;
                    Valid = true;
                }
                else
                {
                    Console.WriteLine("--Wrong Value--");
                    Console.WriteLine("Enter type of user: ");
                }
            }
            Valid = false;
                    SqlCommand cmd4 = new SqlCommand("update users set user_type=N'" + usertype + "' where id=" + id + "", con);
                    try
                    {
                        con.Open();
                        cmd4.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Type usre was updated press any key");
                        Console.ReadKey();
                        con.Close();
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
                case 4:
                    chosen(user_id);
                    break;
            }
        }
        private void deleteuser(int user_id)
        {
            con.Close();
            Console.WriteLine("Enter number of usre: ");
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
                    Console.WriteLine("Enter number of user");
                }
            }
            Valid = false;
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select user_name from users where id=" + id + "", con);
                if (cmd1.ExecuteScalar().ToString() != "")
                {
                    Console.WriteLine("Name of user is -- " + cmd1.ExecuteScalar().ToString() + " --");
                }
                else
                {
                    chosen(user_id);
                }
            }
            catch
            {
                Console.WriteLine("--User doesn't exists--");
                Console.WriteLine("Press any key to back");
                Console.ReadKey();
                con.Close();
                chosen(user_id);

            }
            finally
            {
                con.Close();
            }
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
                SqlCommand cmd4 = new SqlCommand("delete users where id=" + id + "", con);
                try
                {
                    con.Open();
                    cmd4.ExecuteNonQuery();
                    Console.WriteLine("User deleted press any key");
                    Console.ReadKey();
                    con.Close();
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
                Console.WriteLine("The operation was canceled press any key");
                Console.ReadKey();
                con.Close();
                chosen(user_id);
            }
         
        }
        private void printuser(int user_id)
        {
            con.Close();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select id,user_name,user_password,user_type from users", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    Console.WriteLine("____________________________________________________________________");
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Number of user: ", dr1["id"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of user: ", dr1["user_name"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Password of user: ", dr1["user_password"].ToString(), " ", " "));
                    Console.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Type of user: ", dr1["user_type"].ToString(), " ", " "));
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
                    exportuser(user_id);
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
        private void exportuser(int user_id)
        {
            con.Close();
            string fileName = @"E:\Users.txt";
            try
            {
                StreamWriter writeinvoicr = new StreamWriter(fileName, true);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select id,user_name,user_password,user_type from users", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    writeinvoicr.WriteLine("____________________________________________________________________");
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Number of user: ", dr1["id"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Name of user: ", dr1["user_name"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Password of user: ", dr1["user_password"].ToString(), " ", " "));
                    writeinvoicr.WriteLine(String.Format("|{0,-19}  {1,-20}  {2,-22} |", "Type of user: ", dr1["user_type"].ToString(), " ", " "));
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
