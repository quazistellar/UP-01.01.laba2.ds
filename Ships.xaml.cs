﻿using System;
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
    /// Логика взаимодействия для Ships.xaml
    /// </summary>
    public partial class Ships : Page
    {
        SpaceShipsTableAdapter ships = new SpaceShipsTableAdapter();

        public Ships()
        {
            InitializeComponent();

            gridTypes.ItemsSource = ships.GetData();


            types.Visibility = Visibility.Collapsed;
            types_del.Visibility = Visibility.Collapsed;
            types_edit.Visibility = Visibility.Collapsed;
            types_ships2.Visibility = Visibility.Collapsed;
            types_ships3.Visibility = Visibility.Collapsed;

            action.Items.Add("Изменить");
            action.Items.Add("Удалить");
            action.Items.Add("Добавить");
        }

        private void types2_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window != null)
            {
                window.Close();
            }
        }

        private void types_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (types_ships.Text != null)
                {
                    object id = (gridTypes.SelectedItem as DataRowView).Row[0];
                    ships.UpdateQuery2(types_ships.Text, Convert.ToInt32(types_ships2.Text), Convert.ToInt32(types_ships3.Text), Convert.ToInt32(id));
                    gridTypes.ItemsSource = ships.GetData();
                }

                else if (types_ships == null)
                {
                    MessageBox.Show("Вы не можете оставить поле пустым", Title = "Ошибка: пустое поле");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при изменении");
            }
        }

        private void types_del_Click(object sender, RoutedEventArgs e)
        {
            object id = (gridTypes.SelectedItem as DataRowView).Row[0];
            ships.DeleteQuery2(Convert.ToInt32(id));
            gridTypes.ItemsSource = ships.GetData();
        }

        private void types_Click(object sender, RoutedEventArgs e)
        {

            types.Visibility = Visibility.Visible;
            types_ships2.Visibility = Visibility.Visible;
            types_ships3.Visibility = Visibility.Visible;

            try
            {
                ships.InsertQuery2(types_ships.Text, Convert.ToInt32(types_ships2.Text), Convert.ToInt32(types_ships3.Text));
                gridTypes.ItemsSource = ships.GetData();
            }
            catch 
            {
                MessageBox.Show("Ошибка при добавлении!", Title="Ошибка");            
            }

         
        }

        private void action_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            types_ships.Text = null;
            types_ships2.Text = null;
            types_ships3.Text = null;

            if (action.SelectedIndex == 2)
            {
              
         
                types_ships2.Visibility = Visibility.Visible;
                types_ships3.Visibility = Visibility.Visible;
                types_del.Visibility = Visibility.Collapsed;
                types_edit.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Visible;
                types_ships.Visibility = Visibility.Visible;
                vvod_type.Text = "Введите название нового корабля ->";
            }

            if (action.SelectedIndex == 1)
            {
                types.Visibility = Visibility.Collapsed;
                types_edit.Visibility = Visibility.Collapsed;
                types_del.Visibility = Visibility.Visible;
                types_ships2.Visibility = Visibility.Collapsed;
                types_ships3.Visibility = Visibility.Collapsed;
                vvod_type.Text = "Выберите запись для удаления\nи нажмите 'Удалить'";
                types_ships.Visibility = Visibility.Collapsed;
            }

            if (action.SelectedIndex == 0)
            {

                if (gridTypes.SelectedItem != null)

                {
    
                    types_ships2.Visibility = Visibility.Visible;
                    types_ships3.Visibility = Visibility.Visible;


                    vvod_type.Text = "Выберите запись для изменения\nи измените данные";

                    types_del.Visibility = Visibility.Collapsed;
                    types_edit.Visibility = Visibility.Visible;

                    types_ships.Visibility = Visibility.Visible;

                    object value1 = (gridTypes.SelectedItem as DataRowView).Row[1];
                    types_ships.Text = value1.ToString();


                    object value2 = (gridTypes.SelectedItem as DataRowView).Row[2];
                    types_ships2.Text = value2.ToString();

                    object value3 = (gridTypes.SelectedItem as DataRowView).Row[3];
                    types_ships3.Text = value3.ToString();


                }

                else
                {
                    MessageBox.Show("Для изменения записи сначала\nвыберите ее, кликнув по ней");
                }

            }
        }
    }
}
