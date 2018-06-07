using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;
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

namespace WpfAGalery
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    [Serializable]
    public class immg
    {
        public int rate;
        public string str;

        public immg(int r, string s)
        {
            str = s;
            rate = r;
        }
    }

    public partial class MainWindow : Window
    {
        List<immg> image = new List<immg>();
        int key = 0;
        public MainWindow()
        {
            // десериализация из файла
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("people.txt", FileMode.OpenOrCreate))
            {
                image = (List<immg>)formatter.Deserialize(fs);
            }

            InitializeComponent();
            Login lg = new Login();                      //// при запуске открывает окно логин и ожидает закрытия! 
            lg.ShowDialog();
            img.Source = new BitmapImage(new Uri(image[0].str, UriKind.Relative));
            img.Tag = image[0].str;
            File_Info(image[0].str);
            ratting.Content = image[0].rate.ToString();
        }

        public void File_Info(string str_file)              ////// файл_инфо
        {
            name.Content = "Name:";
            date.Content = "Date:";
            autor.Content = "Author:";
            FileInfo fin = new FileInfo(str_file);
            name.Content = name.Content + " " + fin.Name;
            date.Content = date.Content + " " + fin.LastWriteTime.ToShortDateString();

            FileSecurity fileSecurity = fin.GetAccessControl();                              //// get author)))
            IdentityReference identityReference = fileSecurity.GetOwner(typeof(NTAccount));
            autor.Content = autor.Content + " " + identityReference.Value;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) ///first
        {
            First_page();
        }
        public void First_page()
        {
            key = 0;
            img.Source = new BitmapImage(new Uri(image[0].str, UriKind.Relative));
            img.Tag = image[0].str;
            slider.Value = key;
            File_Info(image[0].str);
            ratting.Content = image[0].rate.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) ///last
        {
            key = image.Count;
            img.Source = new BitmapImage(new Uri(image[image.Count - 1].str, UriKind.Relative));
            img.Tag = image[image.Count - 1].str;
            slider.Value = key;
            File_Info(image[image.Count - 1].str);
            ratting.Content = image[image.Count - 1].rate.ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) ///Previous
        {
            key--;
            if (key >= 0)
            {
                img.Source = new BitmapImage(new Uri(image[key].str, UriKind.Relative));
                img.Tag = image[key].str;
                slider.Value = key;
                File_Info(image[key].str);
                ratting.Content = image[key].rate.ToString();
            }
            else
            {
                key = 0;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) ///Next
        {
            key++;
            if (key < image.Count)
            {
                img.Source = new BitmapImage(new Uri(image[key].str, UriKind.Relative));
                img.Tag = image[key].str;
                slider.Value = key;
                File_Info(image[key].str);
                ratting.Content = image[key].rate.ToString();
            }
            else
            {
                key = image.Count;
            }
        }

        private void slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e) /// событие слайдера
        {
            img.Source = new BitmapImage(new Uri(image[(int)slider.Value].str, UriKind.Relative));
            img.Tag = image[(int)slider.Value].str;
            key = (int)slider.Value;
            File_Info(image[(int)slider.Value].str);
            ratting.Content = image[(int)slider.Value].rate.ToString();
        }

        private void Window_Closed_1(object sender, EventArgs e) /// при закрытии окна закрывает все окна!!!
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("people.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, image);
            }
            foreach (Window w in App.Current.Windows)
                w.Close();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)  /// event radio_buttonS
        {
           RadioButton btn = sender as RadioButton;
           foreach(var v in image)
           {
               if (v.str == img.Tag)
               {
                   v.rate += Convert.ToInt32(btn.Tag);
               }
           }
           MessageBox.Show("Оценка: "+btn.Tag.ToString());
           My_sort();
           btn.IsChecked = false;                          
        }

        public void My_sort()
        {
            image.Sort((a,b) => a.rate.CompareTo(b.rate));
            First_page();
        }
    }
}
