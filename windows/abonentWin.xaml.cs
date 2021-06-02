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

namespace MTS.windows
{
    /// <summary>
    /// Логика взаимодействия для abonentWin.xaml
    /// </summary>
    public partial class abonentWin : Window
    {
        public abonentWin()
        {
            InitializeComponent();
        }

        private void cbTarif_Loaded(object sender, RoutedEventArgs e)
        {
            var tarif = entities.tarifs;
            cbTarif.ItemsSource = tarif.ToList().Where(i => i.avaluable == "1");
        }
        private void Update()
        {
            var tarif = entities.tarifs.ToList().
                Where(i => i.idTarif == logedAbonent.idTarif && i.avaluable == "1").FirstOrDefault();
            tbFname.Text = logedAbonent.lName;
            tbName.Text = logedAbonent.name;
            tbNumber.Text = logedAbonent.number;
            tbTarif.Text = tarif.nameTarif;
            tbMinuts.Text = Convert.ToString(tarif.minuts);
            tbSMS.Text = Convert.ToString(tarif.sms);
            tbGB.Text = Convert.ToString(tarif.gb);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tarif = entities.tarifs.ToList().
                Where(i => i.idTarif == logedAbonent.idTarif && i.avaluable == "1").FirstOrDefault();
            tbFname.Text = logedAbonent.lName;
            tbName.Text = logedAbonent.name;
            tbNumber.Text = logedAbonent.number;
            tbTarif.Text = tarif.nameTarif;
            tbMinuts.Text = Convert.ToString(tarif.minuts);
            tbSMS.Text = Convert.ToString(tarif.sms);
            tbGB.Text = Convert.ToString(tarif.gb);

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (editWrap.Visibility == Visibility.Collapsed)
            {
                edit.Content = "Скрыть";
                editWrap.Visibility = Visibility.Visible;
            }
            else
            {
                editWrap.Visibility = Visibility.Collapsed;
                edit.Content = "Сменить тариф";
            }
        }

        private void submint_Click(object sender, RoutedEventArgs e)
        {
            if (cbTarif.SelectedIndex != -1)
            {
                var user = entities.abonents.ToList().
                           Where(i => i.id == logedAbonent.id && i.avaluable == "1").
                           FirstOrDefault();
                try
                {
                    user.idTarif = cbTarif.SelectedIndex + 1;
                    entities.SaveChanges();
                    Update();
                    cbTarif.SelectedItem = null;

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else MessageBox.Show("Выберите желаемый тариф", "Изменение тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
