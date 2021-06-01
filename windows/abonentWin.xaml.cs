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
using static MTS.SuppClass;

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
            cbTarif.ItemsSource = tarif.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tarif = entities.tarifs.ToList().
                Where(i => i.idTarif == logedAbonent.idTarif).FirstOrDefault();
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
            Hide();
            SuppClass.mainWindow.ShowDialog();

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
                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                abonentWin AbonentWin = new abonentWin();
                AbonentWin.ShowDialog();
            }
            else MessageBox.Show("Выберите желаемый тариф", "Изменение тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
