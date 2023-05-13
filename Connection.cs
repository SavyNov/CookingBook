using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookingBook
{
    internal class Connection {
        public static string connectionString = "host=localhost;DataBase=CookingBookDB;Username=postgres;Password=postgres";
        NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

        public Connection() { }

        public NpgsqlDataSource GetDataSource() {
            return dataSource;
        }
    }
}   