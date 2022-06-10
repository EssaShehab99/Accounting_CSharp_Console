using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ConsoleApplication1
{
    
    class returnid
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\accountingsystem\ConsoleApplication1\ConsoleApplication1\Database1.mdf;Integrated Security=True");
        public int id(string tbname)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX(id) FROM "+tbname+"", con);
                con.Open();
                int idreturned = Convert.ToInt16(cmd.ExecuteScalar().ToString());
                return idreturned;
            }
            catch
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
