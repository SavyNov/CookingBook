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
            RefreshList();
        }

        private void button1_Click(object sender, EventArgs e) {
            listView.BeginUpdate();
            listView.Items.Clear();
            recipes.SearchRecipes(textBox1.Text, listView);
            listView.EndUpdate();
            textBox1.Text=String.Empty;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e) {
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item!=null) {
                MessageBox.Show("The selected Item Name is: "+item.Text);
            }
            else {
                this.listView1.SelectedItems.Clear();
                MessageBox.Show("No Item is selected");
            }
        }
    }
}
