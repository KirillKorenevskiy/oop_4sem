using Lab_11.Model;
using Lab_11.View;
using Lab_11.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_11
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        List<Consultation> consultations = new();
        private void OnStartup(object sender, StartupEventArgs e)
        {
            connectToDB();
            MainWindow window = new();
            MainViewModel viewModel = new(consultations);
            window.DataContext = viewModel;
            window.Show();
        }

        private void connectToDB()
        {
                string sql = "select * from consultation;";
                SqlConnection connection = null;
                DataTable dataTable = new DataTable();
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Consultation cons;

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string subject = reader.GetString(2);
                        string time = reader.GetString(3);
                        DateOnly date = DateOnly.FromDateTime(reader.GetDateTime(4));
                        cons = new(name, subject, time, date);
                        consultations.Add(cons);
                    }
                }

                reader.CloseAsync();
            }
        }
    }
}
