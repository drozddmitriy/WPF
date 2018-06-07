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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string sourse = @"Data Source=ДИМА-ПК\SQLEXPRESS;Initial Catalog=Galery;Integrated Security=True";
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = null;
                string pass = null;
                SqlConnection con = new SqlConnection(sourse);
                con.Open();
                string select = String.Format("select login_, password_ from Login_ where login_ = '{0}' and password_ = '{1}'", text_log.Text, pass_log.Password);
                SqlCommand com = new SqlCommand(select, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    str = reader.GetValue(0).ToString();
                    pass = reader.GetValue(1).ToString();
                }

                if (str == text_log.Text && pass == pass_log.Password) 
                {
                    this.Hide();                                     
                }
                else
                {
                    MessageBox.Show("Пароль или логин не верный!");
                    text_log.Clear();
                    pass_log.Clear();
                }
                con.Close();
            }
            catch (Exception ex) { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration reg = new Registration();
            reg.ShowDialog();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }

    }
}
