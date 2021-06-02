using System;
using System.Collections.Generic;
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
using MTS.windows;
using static MTS.AppData;

namespace MTS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void registr_Click(object sender, RoutedEventArgs e)
        {
            pbPassword.Password = string.Empty;
            tbLogin.Text = string.Empty;
            registrationWin registrationWin = new registrationWin();
            Close();
            registrationWin.ShowDialog();

        }

        private void inter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbLogin.Text == string.Empty ||
                pbPassword.Password == string.Empty)
                {
                    MessageBox.Show("Заполните все поля!", "Авторизация пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var user = entities.Auth.ToList().
                        Where(i => i.login == tbLogin.Text && i.password == pbPassword.Password).
                        FirstOrDefault();
                    var userAv = entities.abonents.ToList().
                        Where(i => i.login == tbLogin.Text && i.avaluable == "1").
                        FirstOrDefault();

                    if (user != null && userAv != null && user.idRole == 2)
                    {
                        logedAbonent = userAv;
                        abonentWin abonentWin = new abonentWin();
                        Close();
                        abonentWin.ShowDialog();
                        pbPassword.Password = string.Empty;
                        tbLogin.Text = string.Empty;
                    }
                    else if (user != null && user.idRole == 1)
                    {
                        adminWin adminWin = new adminWin();
                        Close();
                        adminWin.ShowDialog();
                        pbPassword.Password = string.Empty;
                        tbLogin.Text = string.Empty;
                    }
                    else MessageBox.Show("Пользователь не найден, проверьте правилность введённых данных!", "Авторизация пользователя", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
                
            }
        }       
        private void Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
