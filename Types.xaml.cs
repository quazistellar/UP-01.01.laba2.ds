using System;
using System.Collections.Generic;
using System.Data;
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
using UP_laba2_dataset.UP_laba_1DataSetTableAdapters;

namespace UP_laba2_dataset
{
    /// <summary>
    /// Логика взаимодействия для Types.xaml
    /// </summary>
    public partial class Types : Page
    {

        ShipsTypesTableAdapter typesSh = new ShipsTypesTableAdapter();
        public Types()
        {
            InitializeComponent();

            gridTypes.ItemsSource = typesSh.GetData();


            types.Visibility = Visibility.Collapsed;
            types_del.Visibility = Visibility.Collapsed;
            types_edit.Visibility = Visibility.Collapsed;
            action.Items.Add("Изменить");
            action.Items.Add("Удалить");
            action.Items.Add("Добавить");
        }

        private void action_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (action.SelectedIndex == 2)
            {
                types_ships.Text = "";

                types_del.Visibility = Visibility.Collapsed;
                types_edit.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Visible;
                types_ships.Visibility = Visibility.Visible;
                vvod_type.Text = "Введите название нового типа корабля ->";
            }

            if (action.SelectedIndex == 1)
            {
                types.Visibility = Visibility.Collapsed;
                types_edit.Visibility = Visibility.Collapsed;
                types_del.Visibility = Visibility.Visible;

                vvod_type.Text = "Выберите запись для удаления\nи нажмите 'Удалить'";
                types_ships.Visibility = Visibility.Collapsed;
            }

            if (action.SelectedIndex == 0)
            {

                if (gridTypes.SelectedItem != null)

                {
                    vvod_type.Text = "Выберите запись для изменения\nи измените данные";
                    types.Visibility = Visibility.Collapsed;
                    types_del.Visibility = Visibility.Collapsed;
                    types_edit.Visibility = Visibility.Visible;

                    types_ships.Visibility = Visibility.Visible;

                    object value1 = (gridTypes.SelectedItem as DataRowView).Row[1];
                    types_ships.Text = value1.ToString();
                }

                else
                {
                    MessageBox.Show("Для изменения записи сначала\nвыберите ее, кликнув по ней");
                }
            }
        }

        private void types2_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window != null)
            {
                window.Close();
            }

        }

        private void types_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                typesSh.InsertQuery(types_ships.Text);
           
                gridTypes.ItemsSource = typesSh.GetData();
              
          
            }
            catch
            {
                MessageBox.Show("Данные в этом столбце должны быть уникальными,\nтакая запись уже существует!", Title = "Ошибка: уникальность");
            }
        }

        private void types_del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (gridTypes.SelectedItem as DataRowView).Row[0];
                typesSh.DeleteQuery(Convert.ToInt32(id));
                gridTypes.ItemsSource = typesSh.GetData();
            }

            catch
            {
                MessageBox.Show("Ошибка при удалении", Title = "Ошибка");
            }
        }

        private void types_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (gridTypes.SelectedItem as DataRowView).Row[0];

         
                if (types_ships.Text != null)
                {
                    typesSh.UpdateQuery(types_ships.Text, Convert.ToInt32(id));
                    gridTypes.ItemsSource = typesSh.GetData();
                }

                else if (types_ships == null)
                {
                    MessageBox.Show("Вы не можете оставить поле пустым", Title = "Ошибка: пустое поле");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении", Title = "Ошибка");
            }

        }
    }
}