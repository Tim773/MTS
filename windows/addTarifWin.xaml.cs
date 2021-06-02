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
    /// Логика взаимодействия для addTarifWin.xaml
    /// </summary>
    public partial class addTarifWin : Window
    {
        public addTarifWin()
        {
            InitializeComponent();
        }

        private void addSub_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == string.Empty ||
                        tbName.Text == string.Empty ||
                        tbSMS.Text == string.Empty ||
                        tbGiga.Text == string.Empty ||
                        tbCount.Text == string.Empty)
            {
                MessageBox.Show("Вы указали не все данные", "Редактирование тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateName(tbName.Text) == false)
            {
                MessageBox.Show("Имя содержит недопустимые символы (В этих полях может присутсвовать только кирилица)", "Регистрация тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateNumber(tbMinuts.Text) == false)
            {
                MessageBox.Show("В введённом вами значении минут присутствуют недопустимые символы", "Регистрация тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateNumber(tbSMS.Text) == false)
            {
                MessageBox.Show("В введённом вами значении смс присутствуют недопустимые символы", "Регистрация тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateNumber(tbGiga.Text) == false)
            {
                MessageBox.Show("В введённом вами значении гигабайт присутствуют недопустимые символы", "Регистрация тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateNumber(tbCount.Text) == false)
            {
                MessageBox.Show("В введённом вами значении стоимости тарифа присутствуют недопустимые символы", "Регистрация тарифа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    tarifs tarifs = entities.tarifs.Add(new tarifs
                    {
                        nameTarif = tbName.Text,
                        minuts = Convert.ToInt32(tbMinuts.Text),
                        sms = Convert.ToInt32(tbMinuts.Text),
                        gb = Convert.ToInt32(tbGiga.Text),
                        count = tbCount.Text,
                        avaluable ="1"
                        
                    }
                        );
                    
                    tbName.Text = string.Empty;
                    tbMinuts.Text = string.Empty;
                    tbSMS.Text = string.Empty;
                    tbGiga.Text = string.Empty;
                    tbCount.Text = string.Empty;
                    entities.SaveChanges();
                    MessageBox.Show("Регистрация нового тарифа прошла успешно", "Регистрация абоента", MessageBoxButton.OK);
                    adminWin adminWin = new adminWin();
                    Close();
                    adminWin.ShowDialog();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    throw;
                }
            }

        }

        private void addCan_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = string.Empty;
            tbMinuts.Text = string.Empty;
            tbSMS.Text = string.Empty;
            tbGiga.Text = string.Empty;
            tbCount.Text = string.Empty;           
            adminWin adminWin = new adminWin();
            Close();
            adminWin.ShowDialog();
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
