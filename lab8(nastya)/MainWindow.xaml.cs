using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Configuration;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace lab8
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT * FROM Manufacturers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand(query, connection, transaction);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    ManufacturersGrid.ItemsSource = dataTable.DefaultView;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT * FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand(query, connection, transaction);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    ProductsGrid.ItemsSource = dataTable.DefaultView;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //добавление продукта
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            string name = NameTextBox.Text;
            string inventoryNumber = InvTextBox.Text;
            string size = SizeTextBox.Text;
            decimal weight;
            string type = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();
            DateTime arrivalDate;
            int quantity;
            decimal price;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Имя'.");
                return;
            }

            if (string.IsNullOrEmpty(inventoryNumber))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Инвентарный номер'.");
                return;
            }

            if (string.IsNullOrEmpty(size))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Размер'.");
                return;
            }

            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Пожалуйста, выберите значение для поля 'Тип'.");
                return;
            }

            if (!decimal.TryParse(WeightTextBox.Text, out weight))
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение для поля 'Вес'.");
                return;
            }

            if (!DateTime.TryParse(DatePicker.Text, out arrivalDate))
            {
                MessageBox.Show("Пожалуйста, выберите корректную дату для поля 'Дата прибытия'.");
                return;
            }

            if (!int.TryParse(NumberTextBox.Text, out quantity))
            {
                MessageBox.Show("Пожалуйста, введите корректное целочисленное значение для поля 'Количество'.");
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out price))
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение для поля 'Цена'.");
                return;
            }


            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string selectQuery = "SELECT COUNT(*) FROM Products WHERE InventoryNumber = @InventoryNumber";
            string insertQuery = @"
        INSERT INTO Products (Name, InventoryNumber, Size, Weight, Type, ArrivalDate, Quantity, Price)
        VALUES (@Name, @InventoryNumber, @Size, @Weight, @Type, @ArrivalDate, @Quantity, @Price)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@InventoryNumber", inventoryNumber);

                        int existingCount = (int)selectCommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Запись с указанным инв. номером уже существует. Пожалуйста, выберите другой инв. номер.");
                            return;
                        }
                    }

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@InventoryNumber", inventoryNumber);
                        command.Parameters.AddWithValue("@Size", size);
                        command.Parameters.AddWithValue("@Weight", weight);
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@ArrivalDate", arrivalDate);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Данные успешно записаны в таблицу 'Products'.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи данных: {ex.Message}");
            }
        }


        //получение детальной информации для продукта
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string inventoryNumber = InvTextBox.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string selectQuery = "SELECT * FROM Products WHERE InventoryNumber = @InventoryNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InventoryNumber", inventoryNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string size = reader["Size"].ToString();
                                string type = reader["Type"].ToString();
                                decimal weight = (decimal)reader["Weight"];
                                int quantity = (int)reader["Quantity"];
                                decimal price = (decimal)reader["Price"];
                                string name = reader["Name"].ToString();
                                DateTime arrivalDate = (DateTime)reader["ArrivalDate"];

                                SizeTextBox.Text = size;
                                TypeComboBox.Text = type;
                                WeightTextBox.Text = weight.ToString();
                                NumberTextBox.Text = quantity.ToString();
                                PriceTextBox.Text = price.ToString();
                                NameTextBox.Text = name;
                                DatePicker.SelectedDate = arrivalDate;
                            }
                            else
                            {
                                MessageBox.Show("Товар с указанным инвентарным номером не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }


        //удаление продукта
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string inventoryNumber = InvTextBox.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("DeleteProductAndManufacturer", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@InventoryNumber", inventoryNumber);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Продукт успешно удален.");
                            InvTextBox.Text = string.Empty;
                            SizeTextBox.Text = string.Empty;
                            TypeComboBox.Text = string.Empty;
                            WeightTextBox.Text = string.Empty;
                            NumberTextBox.Text = string.Empty;
                            PriceTextBox.Text = string.Empty;
                            NameTextBox.Text = string.Empty;
                            DatePicker.SelectedDate = null;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при удалении продукта: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }


        //обновление данных о продукте
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            // Считываем данные из элементов управления
            string name = NameTextBox.Text;
            string inventoryNumber = InvTextBox.Text;
            string size = SizeTextBox.Text;
            decimal weight;
            string type = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();
            DateTime arrivalDate;
            int quantity;
            decimal price;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Имя'.");
                return;
            }

            if (string.IsNullOrEmpty(inventoryNumber))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Инвентарный номер'.");
                return;
            }

            if (string.IsNullOrEmpty(size))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Размер'.");
                return;
            }

            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Пожалуйста, выберите значение для поля 'Тип'.");
                return;
            }

            if (!decimal.TryParse(WeightTextBox.Text, out weight))
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение для поля 'Вес'.");
                return;
            }

            if (!DateTime.TryParse(DatePicker.Text, out arrivalDate))
            {
                MessageBox.Show("Пожалуйста, выберите корректную дату для поля 'Дата прибытия'.");
                return;
            }

            if (!int.TryParse(NumberTextBox.Text, out quantity))
            {
                MessageBox.Show("Пожалуйста, введите корректное целочисленное значение для поля 'Количество'.");
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out price))
            {
                MessageBox.Show("Пожалуйста, введите корректное числовое значение для поля 'Цена'.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("UPDATE Products SET Name = @Name, Size = @Size, Weight = @Weight, Type = @Type, ArrivalDate = @ArrivalDate, Quantity = @Quantity, Price = @Price WHERE InventoryNumber = @InventoryNumber", connection, transaction))
                            {
                                command.Parameters.AddWithValue("@InventoryNumber", inventoryNumber);
                                command.Parameters.AddWithValue("@Name", name);
                                command.Parameters.AddWithValue("@Size", size);
                                command.Parameters.AddWithValue("@Weight", weight);
                                command.Parameters.AddWithValue("@Type", type);
                                command.Parameters.AddWithValue("@ArrivalDate", arrivalDate);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Price", price);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Информация успешно обновлена.");
                                }
                                else
                                {
                                    MessageBox.Show("Продукт с указанным инвентарным номером не найден.");
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при обновлении информации: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }







        string SelectedPhotoFilePath;
        private void SelectPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedPhotoPath = openFileDialog.FileName;

                BitmapImage bitmapImage = new BitmapImage(new Uri(selectedPhotoPath));
                SelectedPhotoImage.Source = bitmapImage;

                SelectedPhotoFilePath = selectedPhotoPath;
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SelectPhoto();
        }

        //добавление нового производителя
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            // Считываем данные из элементов управления
            string country = CountryTextBox.Text;
            string organization = OrganizationTextBox.Text;
            string address = AdressTextBox.Text;
            string phone = PhoneTextBox.Text;
            string photoFilePath = SelectedPhotoFilePath;
            int staffId;

            if (string.IsNullOrEmpty(country))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Страна'.");
                return;
            }

            if (string.IsNullOrEmpty(organization))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Организация'.");
                return;
            }

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Адрес'.");
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Пожалуйста, введите значение для поля 'Телефон'.");
                return;
            }

            if (!int.TryParse(StaffId.Text, out staffId))
            {
                MessageBox.Show("Пожалуйста, введите корректное целочисленное значение для поля 'ID сотрудника'.");
                return;
            }


            // Проверяем, существует ли запись с указанным названием организации
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string selectQuery = "SELECT COUNT(*) FROM Manufacturers WHERE Organization = @Organization";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Organization", organization);

                    int existingCount = (int)selectCommand.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Запись с указанным названием организации уже существует. Пожалуйста, выберите другое название организации.");
                        return;
                    }
                }

                // Добавляем данные в таблицу
                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Manufacturers (Country, Organization, Address, Phone, Photo, ProductId) VALUES (@Country, @Organization, @Address, @Phone, @PhotoFilePath, @StaffId)", connection))
                    {
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@Organization", organization);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@PhotoFilePath", photoFilePath);
                        command.Parameters.AddWithValue("@StaffId", staffId);


                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Данные производителя успешно добавлены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении данных производителя: {ex.Message}");
                }
            }
        }


        //получение детальной информации о производителе
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {

            string organization = OrganizationTextBox.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("SELECT Country, Organization, Address, Phone, Photo, ProductId FROM Manufacturers WHERE Organization = @Organization", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Organization", organization);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string country = reader.GetString(0);
                                    string organizationn = reader.GetString(1);
                                    string address = reader.GetString(2);
                                    string phone = reader.GetString(3);
                                    string photoFilePath = reader.GetString(4);
                                    int staffId = reader.GetInt32(5);


                                    CountryTextBox.Text = country;
                                    OrganizationTextBox.Text = organizationn;
                                    AdressTextBox.Text = address;
                                    PhoneTextBox.Text = phone;
                                    StaffId.Text = staffId.ToString();

                                    SelectedPhotoFilePath = photoFilePath;
                                    if (!string.IsNullOrEmpty(photoFilePath))
                                    {
                                        SelectedPhotoImage.Source = new BitmapImage(new Uri(photoFilePath));
                                    }
                                    else
                                    {
                                        SelectedPhotoImage.Source = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ошибка.");
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при получении информации о производителе: {ex.Message}");
                    }
                }
            }
        }


        //удаление производителя
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string organization = OrganizationTextBox.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("DeleteManufacturerByOrganization", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Organization", organization);

                                command.ExecuteNonQuery();
                            }

                            CountryTextBox.Text = string.Empty;
                            OrganizationTextBox.Text = string.Empty;
                            AdressTextBox.Text = string.Empty;
                            PhoneTextBox.Text = string.Empty;
                            StaffId.Text = string.Empty;
                            SelectedPhotoImage.Source = null;
                            

                            transaction.Commit();
                            MessageBox.Show("Запись успешно удалена.");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }



        //изменение информации о производителе
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            string organization = OrganizationTextBox.Text;
            string country = CountryTextBox.Text;
            string address = AdressTextBox.Text;
            string phone = PhoneTextBox.Text;
            string photo = SelectedPhotoFilePath;

            int staffId;

            if (!int.TryParse(StaffId.Text, out staffId))
            {
                MessageBox.Show("Неверный формат ID товара.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand selectCommand = new SqlCommand("SELECT Id FROM Products WHERE Id = @StaffId", connection, transaction))
                        {
                            selectCommand.Parameters.AddWithValue("@StaffId", staffId);

                            using (SqlDataReader reader = selectCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    MessageBox.Show("Товар с указанным ID и организацией не найден.");
                                    return;
                                }
                            }
                        }

                        using (SqlCommand updateCommand = new SqlCommand("UPDATE Manufacturers SET Country = @Country, Address = @Address, Phone = @Phone, Photo = @Photo, ProductId = @StaffId WHERE Organization = @Organization", connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@Country", country);
                            updateCommand.Parameters.AddWithValue("@Address", address);
                            updateCommand.Parameters.AddWithValue("@Phone", phone);
                            updateCommand.Parameters.AddWithValue("@Photo", photo);
                            updateCommand.Parameters.AddWithValue("@Organization", organization);
                            updateCommand.Parameters.AddWithValue("@StaffId", staffId);

                            updateCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Информация о товаре успешно обновлена.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении информации о товаре: {ex.Message}");
                    }
                }
            }
        }


        //сортировка
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT * FROM Manufacturers order by Organization DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand(query, connection, transaction);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    ManufacturersGrid.ItemsSource = dataTable.DefaultView;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }

            string query2 = "SELECT * FROM Products order by Price DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand(query2, connection, transaction);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    ProductsGrid.ItemsSource = dataTable.DefaultView;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
        }





        private int currentIndex = 0;
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = ManufacturersGrid.Items.Count - 2;
            }

            ShowCurrentItem();

            if (ManufacturersGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ManufacturersGrid.SelectedItem;
                if (selectedRow["Id"] != null && selectedRow["Organization"] != null && selectedRow["Country"] != null &&
                    selectedRow["Address"] != null && selectedRow["Phone"] != null && selectedRow["Photo"] != null && selectedRow["ProductId"] != null)
                {
                    var selectedData = new
                    {
                        Id = selectedRow["Id"],
                        Organization = selectedRow["Organization"],
                        Country = selectedRow["Country"],
                        Address = selectedRow["Address"],
                        Phone = selectedRow["Phone"],
                        Photo = selectedRow["Photo"],
                        ProductId = selectedRow["ProductId"]
                    };
                    FillFields(selectedData.Id.ToString(), selectedData.Organization.ToString(), selectedData.Country.ToString(), selectedData.Address.ToString(), selectedData.Phone.ToString(), selectedData.Photo.ToString(), selectedData.ProductId.ToString());
                }
                else
                { }
            }
        }

        private void ShowCurrentItem()
        {
            if (currentIndex >= 0 && currentIndex < ManufacturersGrid.Items.Count)
            {
                ManufacturersGrid.SelectedIndex = currentIndex;
                ManufacturersGrid.ScrollIntoView(ManufacturersGrid.SelectedItem);
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (currentIndex >= ManufacturersGrid.Items.Count - 2)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            ShowCurrentItem();

            if (ManufacturersGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ManufacturersGrid.SelectedItem;
                if (selectedRow["Id"] != null && selectedRow["Organization"] != null && selectedRow["Country"] != null &&
                    selectedRow["Address"] != null && selectedRow["Phone"] != null && selectedRow["Photo"] != null && selectedRow["ProductId"] != null)
                {
                    var selectedData = new
                    {
                        Id = selectedRow["Id"],
                        Organization = selectedRow["Organization"],
                        Country = selectedRow["Country"],
                        Address = selectedRow["Address"],
                        Phone = selectedRow["Phone"],
                        Photo = selectedRow["Photo"],
                        ProductId = selectedRow["ProductId"]
                    };
                    FillFields(selectedData.Id.ToString(), selectedData.Organization.ToString(), selectedData.Country.ToString(), selectedData.Address.ToString(), selectedData.Phone.ToString(), selectedData.Photo.ToString(), selectedData.ProductId.ToString());
                }
                else
                { }
            }
        }

        public void FillFields(string id, string organization, string country, string address, string phone, string photo, string productId)
        {
            StaffId.Text = id;
            OrganizationTextBox.Text = organization;
            CountryTextBox.Text = country;
            AdressTextBox.Text = address;
            PhoneTextBox.Text = phone;
            SelectedPhotoImage.Source = new BitmapImage(new Uri(photo));
        }






        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            if (currentIndex2 > 0)
            {
                currentIndex2--;
            }
            else
            {
                currentIndex2 = ProductsGrid.Items.Count - 2;
            }

            ShowCurrentItem();

            if (ProductsGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ProductsGrid.SelectedItem;
                if (selectedRow["Id"] != null && selectedRow["Name"] != null && selectedRow["InventoryNumber"] != null &&
                    selectedRow["Size"] != null && selectedRow["Weight"] != null && selectedRow["Type"] != null &&
                    selectedRow["ArrivalDate"] != null && selectedRow["Quantity"] != null && selectedRow["Price"] != null)
                {
                    var selectedData = new
                    {
                        Id = selectedRow["Id"],
                        Name = selectedRow["Name"],
                        InventoryNumber = selectedRow["InventoryNumber"],
                        Size = selectedRow["Size"],
                        Weight = selectedRow["Weight"],
                        Type = selectedRow["Type"],
                        ArrivalDate = selectedRow["ArrivalDate"],
                        Quantity = selectedRow["Quantity"],
                        Price = selectedRow["Price"]
                    };
                    FillFields2(
                        selectedData.InventoryNumber.ToString(),
                        selectedData.Size.ToString(),
                        selectedData.Type.ToString(),
                        selectedData.Weight.ToString(),
                        selectedData.Quantity.ToString(),
                        selectedData.Price.ToString(),
                        selectedData.Name.ToString(),
                        Convert.ToDateTime(selectedData.ArrivalDate)
                    );
                }
                else
                { }
            }
        }

        public void FillFields2(string invNumber, string size, string type, string weight, string quantity, string price, string name, DateTime arrivalDate)
        {
            InvTextBox.Text = invNumber;
            SizeTextBox.Text = size;

            foreach (ComboBoxItem item in TypeComboBox.Items)
            {
                if (item.Content.ToString() == type)
                {
                    item.IsSelected = true;
                    break;
                }
            }

            WeightTextBox.Text = weight;
            NumberTextBox.Text = quantity;
            PriceTextBox.Text = price;
            NameTextBox.Text = name;
            DatePicker.SelectedDate = arrivalDate;
        }

        private int currentIndex2 = 0;
        private void ShowCurrentItem2()
        {
            if (currentIndex2 >= 0 && currentIndex2 < ProductsGrid.Items.Count)
            {
                ProductsGrid.SelectedIndex = currentIndex2;
                ProductsGrid.ScrollIntoView(ProductsGrid.SelectedItem);
            }
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            if (currentIndex2 >= ProductsGrid.Items.Count - 2)
            {
                currentIndex2 = 0;
            }
            else
            {
                currentIndex2++;
            }
            ShowCurrentItem2();


            if (ProductsGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ProductsGrid.SelectedItem;
                if (selectedRow["Id"] != null && selectedRow["Name"] != null && selectedRow["InventoryNumber"] != null &&
                    selectedRow["Size"] != null && selectedRow["Weight"] != null && selectedRow["Type"] != null &&
                    selectedRow["ArrivalDate"] != null && selectedRow["Quantity"] != null && selectedRow["Price"] != null)
                {
                    var selectedData = new
                    {
                        Id = selectedRow["Id"],
                        Name = selectedRow["Name"],
                        InventoryNumber = selectedRow["InventoryNumber"],
                        Size = selectedRow["Size"],
                        Weight = selectedRow["Weight"],
                        Type = selectedRow["Type"],
                        ArrivalDate = selectedRow["ArrivalDate"],
                        Quantity = selectedRow["Quantity"],
                        Price = selectedRow["Price"]
                    };
                    FillFields2(
                        selectedData.InventoryNumber.ToString(),
                        selectedData.Size.ToString(),
                        selectedData.Type.ToString(),
                        selectedData.Weight.ToString(),
                        selectedData.Quantity.ToString(),
                        selectedData.Price.ToString(),
                        selectedData.Name.ToString(),
                        Convert.ToDateTime(selectedData.ArrivalDate)
                    );
                }
                else
                { }
            }
        }
    }
}

