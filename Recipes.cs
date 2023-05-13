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

                var recipe = name + " \n" + description + " \n" + instructions + "\n";

                ingredients = (string[])reader.GetValue(3);
                string joinedIngredients = string.Join(", ", ingredients);

                ListViewItem lvName = new ListViewItem(name);
                listView.Items.Add(lvName);
                lvName.SubItems.Add(description);
                lvName.SubItems.Add(instructions);
                lvName.SubItems.Add(joinedIngredients);
            }
        }

        public bool InsertRecipe(ListView listView,String tb1, String tb2, String tb3, String tb4) {
            NpgsqlDataSource dataSource = connection.GetDataSource();
            string text = tb4;
            string[] ingredientsArray = text.Split(' ');
            List<string> ingredientsList  = new List<string>(ingredientsArray);
          
            string postGresArray = "[ '" + string.Join("','",ingredientsList) + "' ]";
            
            NpgsqlCommand command = dataSource.CreateCommand(
                "INSERT INTO public.recipes (recipe_id,name, description, instructions, ingredients) VALUES(" +
                "DEFAULT, " +
                "'" + tb1 + "', " +
                "'" + tb2+ "', " +
                "'" + tb3+ "', " +
                "ARRAY" +  postGresArray  + ");");
            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.RecordsAffected == 1 && 
                (!String.IsNullOrEmpty(tb1)) && 
                 (!String.IsNullOrEmpty(tb2))&&
                 (!String.IsNullOrEmpty(tb3))&&
                  (!String.IsNullOrEmpty(tb4))){
                
                return true;
            }
            else {
                MessageBox.Show("There cannot be empty lines, please fill all boxes");
                return false;
            }
        }
    }
}