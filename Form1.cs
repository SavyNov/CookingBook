using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookingBook
{
    public partial class Form1: Form
    {

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string username = textBox1.Text;
            string password = textBox2.Text;
            UserValidation validation = new UserValidation();
            if (!validation.CheckUser(username, password)) return;
            //cookingBook.WindowState = FormWindowState.Maximized;
            CookingBook cookingBook = new CookingBook();
            cookingBook.Show();
            this.Hide();
        }
    }
}
