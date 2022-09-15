using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Class;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string Text = "Data Source=NIHAD; Initial Catalog=library; Integrated Security=SSPI";
        private List<Author>  _aut = new List<Author>();
        public MainWindow()
        {
      
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            SqlConnection _sqlConnection = new SqlConnection(Text);
            using (_sqlConnection)
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Authors", _sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                        _aut.Add(new Author((int)(reader[0]), reader[1].ToString(), reader[2].ToString()));
                        ListLibrary.Items.Add(new UserControls(_aut[_aut.Count - 1]));
                }
                _sqlConnection.Close();
            }
        }
        private bool Check() => Name_lbl.Text.Length>0&&Surname_lbl.Text.Length>0;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Check()) Loading2(Name_lbl.Text, Surname_lbl.Text);
        }
        private void Loading2(string name,string surname)
        {
            SqlConnection sqlConnection = new SqlConnection(Text);
            using (sqlConnection)
            {
                sqlConnection.Open();
                string? insertString = @$"INSERT INTO Authors (Id, FirstName, LastName)
                        VALUES({_aut[_aut.Count-1].Id+1}, '{name}', '{surname}')";
                _aut.Add(new Author(_aut[_aut.Count - 1].Id+1, name, surname));
                ListLibrary.Items.Add(new UserControls(_aut[_aut.Count - 1]));
                SqlCommand cmd = new SqlCommand(insertString, sqlConnection);
                cmd.ExecuteNonQuery();  sqlConnection.Close();
            }
        }
        private List<Author> SortList(string str)
        {
            List<Author> product = new List<Author>();
            foreach (var item in _aut)
            {

                    if (item.FULL.ToLower().IndexOf(str.ToLower()) != -1 && str != " ")
                    {
                        product.Add(item);
                    }
                  
                
            }
            return product;
        }
        private void AddList(List<Author> aut)
        {
             ListLibrary.Items.Clear();
            foreach (var item in aut)ListLibrary.Items.Add(item);
            
        }
        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            List<Author> auto = new List<Author>();
            if (Search.Text.Length != 0)
            {
                auto = SortList(Search.Text);
                if (auto.Count != 0)
                {
                    AddList(auto);
                }
            }
            else AddList(_aut);
        }
    }
}
