using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Login
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        public Login()
        {
            bool config = true;
            while (config)
            {
                Console.Clear();
                Console.WriteLine("Enter User Name: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine();
                SqlCommand cmd = new SqlCommand("select * from users where user_name=N'" + username + "'and user_password=N'" + password + "'", con);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int user_id = Convert.ToInt16(dr["id"].ToString());
                        string user_type = dr["user_type"].ToString();
                        Main m =new Main(user_id, user_type, username);
                        config = false;
                    }
                    else
                    {
                        Console.WriteLine("Username Or Password Wrong !");
                    }
                }
                catch
                {
                    Console.WriteLine("Erorr1");
                }
                finally
                {
                    con.Close();
                }
                con.Close();
            }
        }
    }
}
