using AdminUtilityDashboard.Helpers;
using ShreeHandloomInvoice.Views;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace AdminUtilityDashboard.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string _dbPath = @"Data Source=tasks.db";
        public LoginWindow()
        {
            try
            {
                InitializeComponent();

                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserName TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        FullName TEXT,
                        Email TEXT
                    );";

                DBHelper.EnsureTableExists(createUsersTable);
                var dbuserexist = LoadLastLoginUser();

                if (dbuserexist == "")
                {
                    this.Loaded += (s, e) => txtUsername.Focus();

                }
                else
                {
                    this.Loaded += (s, e) => txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, nameof(LoginWindow), nameof(LoginWindow));

            }
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password.Trim();

                if (ValidateUser(username, password))
                {
                    //MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    //DBHelper.ApplyThemeFromDB();
                    var dashboard = new InvoiceInputPage();
                    dashboard.Show();
                    this.Close(); // Login window band kar do
                }
                else
                {
                    MessageBox.Show("Invalid username or password\nPlease make sure you first signup",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    }
                }
            catch (Exception ex)
            {

               ErrorLogger.LogError( ex,nameof(LoginWindow),nameof(Login_Click));
            }
        }
        private void BtnFrgtStg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var forgotPwd = new ForgotPasswordWindow();
                forgotPwd.ShowDialog();
            }
            catch (Exception ex)
            {

                ErrorLogger.LogError(ex, nameof(LoginWindow), nameof(BtnFrgtStg_Click));

            }
        }

        private bool ValidateUser(string username, string password)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE UserName = @user AND Password = @pass";
                using (var reader = DBHelper.ExecuteReader(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                }))
                {
                    if (reader != null && reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        return count > 0;
                    }
                }
                //return false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, nameof(LoginWindow), nameof(ValidateUser));
            }
            return false;
        }

        private string LoadLastLoginUser()
        {
            try
            {
                string query = "SELECT UserName FROM Users ORDER BY Id DESC LIMIT 1";
                // Agar LastLogin column hoga to "ORDER BY LastLogin DESC LIMIT 1"

                using (var reader = DBHelper.ExecuteReader(query, null))
                {
                    if (reader != null && reader.Read())
                    {
                        txtUsername.Text = reader["UserName"].ToString();
                    }
                }
                return txtUsername.Text;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, nameof(LoginWindow), nameof(LoadLastLoginUser));
            }
            return string.Empty;
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var signup = new SignupWindow();
                signup.ShowDialog();
            }
            catch (Exception ex)
            {

                ErrorLogger.LogError(ex, nameof(LoginWindow), nameof(Signup_Click));
            }
        }

       
    }
}
