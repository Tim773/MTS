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
using System.Windows.Shapes;
using static MTS.AppData;
using static MTS.ValidationClass;

namespace MTS.windows
{
    /// <summary>
    /// Логика взаимодействия для registrationWin.xaml
    /// </summary>
    public partial class registrationWin : Window
    {
        public registrationWin()
        {
            InitializeComponent();
        }
        public int AdmUse { get; set; } = 0;

        private void cbTarif_Loaded(object sender, RoutedEventArgs e)
        {
            var tarif = entities.tarifs;
            cbTarif.ItemsSource = tarif.ToList().Where(i => i.avaluable == "1");
        }

        private void regSub_Click(object sender, RoutedEventArgs e)
        {
            var login = entities.Auth.ToList().
                        Where(i => i.login == tbLogin.Text).
                        FirstOrDefault();
            var number = entities.abonents.ToList().
                        Where(i => i.number == tbNumber.Text).
                        FirstOrDefault();
            if (tbLname.Text == string.Empty ||
                tbName.Text == string.Empty ||
                tbLogin.Text == string.Empty ||
                pbPassword.Password == string.Empty||
                tbNumber.Text == string.Empty ||               
               pbChekPassword.Password == string.Empty ||
               cbTarif.SelectedItem == null)
            {
                MessageBox.Show("Вы указали не все данные", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (pbPassword.Password != pbChekPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateFIO(tbName.Text, tbLname.Text) == false)
            {
                MessageBox.Show("Имя или фамилия содержат недопустимые символы (В этих полях может присутсвовать только кирилица)", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (tbNumber.Text.Length != 11)
            {
                MessageBox.Show("Количество символов в номере телефона не совпадает с общепринятой маской, проверьте правильность введённого номера", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (number != null)
            {
                MessageBox.Show("Введённый вами номер телефона уже есть в нашей базе данных", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (login != null)
            {
                MessageBox.Show("Введённый вами логин уже занят, придумайте новый логин", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateNumber(tbNumber.Text) == false)
            {
                MessageBox.Show("В поле номера присутствуют недопустимые символы", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    Auth auth = entities.Auth.Add(new Auth
                    {

                        login = tbLogin.Text,
                        password = pbPassword.Password,
                        idRole = 2
                    }
                    );
                    abonents abonents = entities.abonents.Add(new abonents
                    {
                        name = tbName.Text,
                        lName = tbLname.Text,
                        number = tbNumber.Text,
                        idTarif = cbTarif.SelectedIndex + 1,
                        avaluable = "1",
                        login = tbLogin.Text
                    }
                    ); ;

                    if (AdmUse == 0)
                    {
                        pbPassword.Password = string.Empty;
                        tbLogin.Text = string.Empty;
                        pbChekPassword.Password = string.Empty;
                        tbLname.Text = string.Empty;
                        tbName.Text = string.Empty;
                        tbNumber.Text = string.Empty;
                        cbTarif.SelectedIndex = -1;
                        entities.SaveChanges();
                        MessageBox.Show("Регистрация нового абонента прошла успешно", "Регистрация абоента", MessageBoxButton.OK);
                        MainWindow mainWindow = new MainWindow();
                        Close();
                        mainWindow.ShowDialog();
                    }
                    else if (AdmUse == 1)
                    {
                        pbPassword.Password = string.Empty;
                        tbLogin.Text = string.Empty;
                        pbChekPassword.Password = string.Empty;
                        tbLname.Text = string.Empty;
                        tbName.Text = string.Empty;
                        tbNumber.Text = string.Empty;
                        cbTarif.SelectedIndex = -1;
                        entities.SaveChanges();
                        MessageBox.Show("Регистрация нового абонента прошла успешно", "Регистрация абоента", MessageBoxButton.OK);
                        adminWin adminWin = new adminWin();
                        Close();
                        adminWin.ShowDialog();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                   
                }

            }
        }
        private void Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void regCan_Click(object sender, RoutedEventArgs e)
        {
            if (AdmUse == 0)
            {
                pbPassword.Password = string.Empty;
                tbLogin.Text = string.Empty;
                pbChekPassword.Password = string.Empty;
                tbLname.Text = string.Empty;
                tbName.Text = string.Empty;
                tbNumber.Text = string.Empty;
                cbTarif.SelectedIndex = -1;
                MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.ShowDialog();
            }
            else if (AdmUse == 1)
            {
                pbPassword.Password = string.Empty;
                tbLogin.Text = string.Empty;
                pbChekPassword.Password = string.Empty;
                tbLname.Text = string.Empty;
                tbName.Text = string.Empty;
                tbNumber.Text = string.Empty;
                cbTarif.SelectedIndex = -1;
                AdmUse = 0;
                adminWin adminWin = new adminWin();
                Close();
                adminWin.ShowDialog();
            }
        }
        public void AdminUpdate()
        {
            AdmUse = 1;
        }
    }
}
