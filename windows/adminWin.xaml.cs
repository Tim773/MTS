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
    /// Логика взаимодействия для adminWin.xaml
    /// </summary>
    public partial class adminWin : Window
    {
        public adminWin()
        {
            InitializeComponent();
            Update();
        }
        public int Page { get; set; } = 0;
        public int RowAll { get; set; } = 0;
        public void Update()
        {
            var datasourse = entities.abonents.ToList();           
            RowAll = datasourse.Count();
            datasourse = datasourse.Skip(Page * 10).Take(9).ToList();
            Abonents.ItemsSource = datasourse;
        }
        private void BackList_Click(object sender, RoutedEventArgs e)
        {
            if (Page > 0)
            {
                Page--;
                Update();
            }
            else
            {
                MessageBox.Show("Открыта первая страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }

        private void NextList_Click(object sender, RoutedEventArgs e)
        {
            if ((10*Page)<RowAll)
            {
            Page++;
            Update();
            }
            else
            {
                MessageBox.Show("Открыта последняя страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }

        private void Abonents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Abonents.SelectedItem is abonents abonents)
            {
                EditWin editWin = new EditWin(abonents);
                editWin.ShowDialog();
                Update();
            }
        }

        private void Abonents_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Tarifs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

