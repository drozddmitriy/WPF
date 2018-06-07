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
using System.Windows.Shapes;

namespace WpfAGalery
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string sourse = @"Data Source=ДИМА-ПК\SQLEXPRESS;Initial Catalog=Galery;Integrated Security=True";
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (password.Password != password_again.Password)
            {
                MessageBox.Show("Пароль не совпадает!");
                return;
            }

            if(login.Text != "" && password.Password != "" && name.Text != "" && surname.Text != "")
            {
                SqlConnection con = new SqlConnection(sourse);
                con.Open();
                string insert = String.Format("insert Login_ values('{0}','{1}','{2}','{3}')", login.Text, password.Password, name.Text, surname.Text);
                SqlCommand com = new SqlCommand(insert, con);
                com.ExecuteNonQuery();
                con.Close();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!!!");
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)   //// button cancel
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }

        private void Window_Closed_1(object sender, EventArgs e) 
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
    }
}
