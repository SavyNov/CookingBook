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
    public partial class AddRecipeForm: Form
    {
        Recipes recipes = new Recipes();
        //CookingBook cb = new CookingBook();
        public AddRecipeForm() {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
           if(!recipes.InsertRecipe(CookingBook.listView,textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text)) return;
            CookingBook.instance.RefreshList();
        }
    }
}
