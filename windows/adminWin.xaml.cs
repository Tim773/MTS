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
        public int RowAllTarif { get; set; } = 0;
        public void Update()
        {
            var tarifsours = entities.tarifs.ToList().Where(i => i.avaluable == "1");
            var datasourse = entities.abonents.ToList().Where(i => i.avaluable == "1");
            RowAllTarif = tarifsours.Count();
            RowAll = datasourse.Count();
            datasourse = datasourse.Skip(Page * 10).Take(10).ToList();
            tarifsours = tarifsours.Skip(Page * 10).Take(10).ToList();
            Abonents.ItemsSource = datasourse;
            Tarifs.ItemsSource = tarifsours;
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
            if (AbonentsRadio.IsChecked == true)
            {
                Page++;
                if ((10 * Page) < RowAll)
                {
                    Update();
                }
                else
                {
                    Page--;
                    Update();
                    MessageBox.Show("Открыта последняя страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
                };
            }
            else if (TarifRadio.IsChecked == true)
            {
                Page++;
                if ((10 * Page) < RowAllTarif)
                {
                    Update();
                }
                else
                {
                    Page--;
                    Update();
                    MessageBox.Show("Открыта последняя страница", "Выборка", MessageBoxButton.OK, MessageBoxImage.Information);
                };
            }
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

        private void Tarifs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Tarifs.SelectedItem is tarifs ttarifs)
            {
                editTarifWin editTarifWin = new editTarifWin(ttarifs);
                editTarifWin.ShowDialog();
                Update();
            }
        }

        private void ClientsRadio_Click(object sender, RoutedEventArgs e)
        {
            if (AbonentsRadio.IsChecked == true)
            {
                Page = 0;
                Abonents.Visibility = Visibility.Visible;
                Tarifs.Visibility = Visibility.Collapsed;
                btnDel.Content = "Удалить абонента";
                btnAdd.Content = "Добавить абонента";
                FamSearch.Visibility = Visibility.Visible;
                PatrSearch.Visibility = Visibility.Visible;
                Update();
            }
            else if (AbonentsRadio.IsChecked == false)
            {
                Page = 0;
                Abonents.Visibility = Visibility.Visible;
                Tarifs.Visibility = Visibility.Collapsed;
                btnDel.Content = "Удалить абонента";
                btnAdd.Content = "Добавить абонента";
                FamSearch.Visibility = Visibility.Visible;
                PatrSearch.Visibility = Visibility.Visible;
                Update();

            }
        }

        private void TarifRadio_Click(object sender, RoutedEventArgs e)
        {
            if (TarifRadio.IsChecked == true)
            {
                Page = 0;
                Abonents.Visibility = Visibility.Collapsed;
                Tarifs.Visibility = Visibility.Visible;
                btnDel.Content = "Удалить тариф";
                btnAdd.Content = "Добавить тариф";                
                FamSearch.Visibility = Visibility.Collapsed;                
                PatrSearch.Visibility = Visibility.Collapsed;
                Update();
            }
            else if (TarifRadio.IsChecked == false)
            {
                Page = 0;
                Abonents.Visibility = Visibility.Collapsed;
                Tarifs.Visibility = Visibility.Visible;
                btnDel.Content = "Удалить тариф";
                btnAdd.Content = "Добавить тариф";
                FamSearch.Visibility = Visibility.Collapsed;
                PatrSearch.Visibility = Visibility.Collapsed;
                Update();
            }
        }
        private void Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AbonentsRadio.IsChecked == true)
                {
                    var itm = Abonents.SelectedItem;
                    if (itm == null)
                    {
                        MessageBox.Show("Выберите запись из таблицы", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information); ;
                    }
                    else if (MessageBox.Show("Строка абонента будет удалена из таблицы. Желаете продолжить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int del = (Abonents.SelectedItem as abonents).id;

                        abonents abonents = entities.abonents.Where(i => i.id == del).FirstOrDefault();
                        abonents.avaluable = "0";
                        entities.SaveChanges();
                        Update();
                    }
                    else return;

                }
                else if (TarifRadio.IsChecked == true)
                {
                    var itm = Tarifs.SelectedItem;
                    if (itm == null)
                    {
                        MessageBox.Show("Выберите запись из таблицы", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (MessageBox.Show("Строка тарифа будет удалена из таблицы. Желаете продолжить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int del = (Tarifs.SelectedItem as tarifs).idTarif;

                        tarifs tarifs = entities.tarifs.Where(i => i.idTarif == del).FirstOrDefault();
                        tarifs.avaluable = "0";
                        entities.SaveChanges();
                        Update();


                    }
                    else return;
                }
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Что-то пошло не так!");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AbonentsRadio.IsChecked == true)
            {
                registrationWin registrationWin = new registrationWin();
                registrationWin.AdminUpdate();
                Close();
                registrationWin.ShowDialog();
            }
            else if (TarifRadio.IsChecked == true)
            {
                addTarifWin addTarifWin = new addTarifWin();
                Close();
                addTarifWin.ShowDialog();
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (MessageBox.Show("Вы действительно хотите выйти из учётной записи?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
                mainWindow.ShowDialog();
            }
        }
        private void ResetSearch(object sende, RoutedEventArgs e)
        {
            FamSearch1.Text = "";
            ImySearch1.Text = "";
            PatrSearch1.Text = "";
            Update();
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            if (AbonentsRadio.IsChecked == true)
            {
                var tabb = entities.abonents.ToList().Where(i => i.avaluable == "1");
                if (FamSearch1.Text.Length == 0 && ImySearch1.Text.Length == 0)
                {
                    Abonents.ItemsSource = tabb.ToList();
                    return;
                }
                else
                {
                    var res = tabb.Where(i => i.lName.Contains(FamSearch1.Text) &&
                                         i.name.Contains(ImySearch1.Text) &&
                                         i.number.Contains(PatrSearch1.Text)
                                         ).ToList();
                    if (res.Count() != 0)
                    {
                        Abonents.ItemsSource = res;
                    }
                    else
                        MessageBox.Show("Внимание!", " Не найдено!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);


                }

            }
            else if (TarifRadio.IsChecked == true)
            {
                var tabb = entities.tarifs.ToList().Where(i => i.avaluable == "1");
                if (ImySearch1.Text.Length == 0)
                {
                    Tarifs.ItemsSource = tabb.ToList();
                    return;
                }
                else
                {
                    var res = tabb.Where(i => i.nameTarif.Contains(ImySearch1.Text)
                                         ).ToList();
                    if (res.Count() != 0)
                    {

                        Tarifs.ItemsSource = res;
                    }
                    else
                        MessageBox.Show("Внимание!", " Не найдено!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
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

