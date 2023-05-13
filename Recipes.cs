using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql;

namespace CookingBook
{
    internal class Recipes
    {
        readonly Connection connection = new Connection();
        private string name;
        private string description;
        private string instructions;
        private string[] ingredients;

        /* public void GetRecipesFromDb(ListBox lb1) {
             NpgsqlDataSource dataSource = connection.GetDataSource();
             NpgsqlCommand command = dataSource.CreateCommand(
                 "SELECT name from public.recipes");
             NpgsqlDataReader reader = command.ExecuteReader();
 
 
             while (reader.Read()) {
                var value = reader.GetString(0);
                lb1.Items.Add(value);
             }
         }*/

        public void GetRecipesFromDb(ListView listView) {
            NpgsqlDataSource dataSource = connection.GetDataSource();
            NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT name,description,instructions,ingredients from public.recipes");
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                name = reader.GetString(0);
                description = reader.GetString(1);
                instructions = reader.GetString(2);

                ingredients = (string[])reader.GetValue(3);
                string joinedIngredients = string.Join(", ", ingredients);

                ListViewItem lvName = new ListViewItem(name);
                listView.Items.Add(lvName);
                lvName.SubItems.Add(description);
                lvName.SubItems.Add(instructions);
                lvName.SubItems.Add(joinedIngredients);
            }
        }

        public bool InsertRecipe(ListView listView, String tb1, String tb2, String tb3, String tb4) {
            NpgsqlDataSource dataSource = connection.GetDataSource();
            string text = tb4;
            char[] separator = {'\r', ',' };
            string[] ingredientsArray = text.Split(separator);
            List<string> ingredientsList = new List<string>(ingredientsArray);

            string postGresArray = "[ '" + string.Join("','", ingredientsList) + "' ]";
            if ((!String.IsNullOrEmpty(tb1)) &&
                (!String.IsNullOrEmpty(tb2)) &&
                (!String.IsNullOrEmpty(tb3)) &&
                (!String.IsNullOrEmpty(tb4))) {

                NpgsqlCommand command = dataSource.CreateCommand(
                    "INSERT INTO public.recipes (recipe_id,name, description, instructions, ingredients) VALUES(" +
                    "DEFAULT, " +
                    "'" + tb1 + "', " +
                    "'" + tb2 + "', " +
                    "'" + tb3 + "', " +
                    "ARRAY" + postGresArray + ");");
                NpgsqlDataReader reader = command.ExecuteReader();
                MessageBox.Show("Recipe was added successfully");
                return true;

            }
            else {
                MessageBox.Show("Files cannot be emtpy, please fill them");
                return false;

            }
        }

        public void SearchRecipes(String tb1,ListView listView) {
            NpgsqlDataSource dataSource = connection.GetDataSource();
            string text = tb1;
            string[] ingredientsArray = text.Split(' ');
            List<string> ingredientsList = new List<string>(ingredientsArray);

            string postGresArray = "'"+string.Join("','", ingredientsList)+"' ";
            if (!String.IsNullOrEmpty(tb1)) {
                NpgsqlCommand command = dataSource.CreateCommand(
                    "SELECT name, description, instructions, ingredients " +
                    "FROM public.recipes " +
                    "WHERE " + postGresArray + " = ANY (ingredients)" );
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    name=reader.GetString(0);
                    description=reader.GetString(1);
                    instructions=reader.GetString(2);

                    ingredients=(string[])reader.GetValue(3);
                    string joinedIngredients = string.Join(", ", ingredients);

                    ListViewItem lvName = new ListViewItem(name);
                    listView.Items.Add(lvName);
                    lvName.SubItems.Add(description);
                    lvName.SubItems.Add(instructions);
                    lvName.SubItems.Add(joinedIngredients);
                }
            }
            else {
                MessageBox.Show("No such ingredient");
            }
        }
    }
}