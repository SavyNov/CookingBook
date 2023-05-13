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
    public partial class CookingBook: Form
    {
        Recipes recipes = new Recipes();
        public static ListView listView;
        private AddRecipeForm addRecipeForm;
        public static CookingBook instance;
        public CookingBook() {
            InitializeComponent();
            listView=listView1;
            instance = this;
        }

        private void CookingBook_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private void CookingBook_Load(object sender, EventArgs e) {

            recipes.GetRecipesFromDb(listView);
        }

        private void button3_Click(object sender, EventArgs e) {
            addRecipeForm = new AddRecipeForm();
            addRecipeForm.Show();
            
        }

        public void RefreshList() {
            listView.BeginUpdate();
            listView.Items.Clear();
            recipes.GetRecipesFromDb(listView);
            listView.EndUpdate();
            listView.Refresh();
        }


        private void button2_Click(object sender, EventArgs e) {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            recipes.GetRecipesFromDb(listView1);
            listView1.EndUpdate();
            listView1.Refresh();
        }
    }
}
