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
using static MTS.windows.adminWin;

namespace MTS.windows
{
    /// <summary>
    /// Логика взаимодействия для EditWin.xaml
    /// </summary>
    public partial class EditWin : Window
    {
        abonents selectedAbonents;

        public EditWin(abonents abonents)
        {
            InitializeComponent();
            selectedAbonents = abonents;
            tbName.Text = abonents.name;
            tbLname.Text = abonents.lName;
            tbNumber.Text = abonents.number;
            cbTarif.SelectedItem = abonents.idTarif;
            cbTarif.SelectedIndex = abonents.idTarif -1;

        }
        private void editSub_Click(object sender, RoutedEventArgs e)
        {           
            if (tbLname.Text == null ||
                tbName.Text == null ||
                tbNumber.Text == null ||
               cbTarif.SelectedItem == null)
            {
                MessageBox.Show("Вы указали не все данные", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateFIO(tbName.Text, tbLname.Text) == false)
            {
                MessageBox.Show("Имя или фамилия содержат недопустимые символы (В этих полях может присутсвовать только кирилица)", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (tbNumber.Text.Length != 11)
            {
                MessageBox.Show("Количество символов в номере телефона не совпадает с общепринятой маской, проверьте правильность введённого номера", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }           
            else if (ValidateNumber(tbNumber.Text) == false)
            {
                MessageBox.Show("В поле номера присутствуют недопустимые символы", "Регистрация абоента", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {                
                   selectedAbonents.name = tbName.Text;
                   selectedAbonents.lName = tbLname.Text;
                   selectedAbonents.number = tbNumber.Text;
                   selectedAbonents.idTarif = cbTarif.SelectedIndex + 1;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    throw;
                }
                tbLname.Text = string.Empty;
                tbName.Text = string.Empty;
                tbNumber.Text = string.Empty;
                cbTarif.SelectedIndex = -1;
                entities.SaveChanges();               
                MessageBox.Show("Обновление данных абонента прошло успешно", "Редактирование абоента", MessageBoxButton.OK);
                Close();
            }
        }

        private void editCan_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbTarif_Loaded(object sender, RoutedEventArgs e)
        {
            var tarif = entities.tarifs;
            cbTarif.ItemsSource = tarif.ToList();
        }
    }
}
