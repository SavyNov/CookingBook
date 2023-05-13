using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookingBook
{
    internal class UserValidation
    {
        readonly Connection connection = new Connection();

        public bool CheckUser(string username, string password) {
            NpgsqlDataSource dataSource = connection.GetDataSource();
            NpgsqlCommand command = dataSource.CreateCommand("SELECT id, username, password FROM public.users"+
                " WHERE username = '"+username+"'"+
                " AND "+
                "password = '"+password+"';"); 
            NpgsqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows) {
                return true;
            }
            else {
                MessageBox.Show("Incorrect user, try again", "Error", MessageBoxButtons.OKCancel);
                return false;         
            }
        }
    }
}