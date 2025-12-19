using AdminUtilityDashboard.Helpers;
using System;
using System.Data.SQLite;
using System.Windows;

namespace AdminUtilityDashboard.Views
{
    public partial class SignupWindow : Window
    {
        private string _dbPath = @"Data Source=tasks.db";

        public SignupWindow()
        {
            InitializeComponent();
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullName = txtFullName.Text.Trim();
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password.Trim();
                string email = txtEmail.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username and Password are required.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string insertQuery = "INSERT INTO Users (UserName, Password, FullName, Email) VALUES (@user, @pass, @name, @mail)";
                DBHelper.ExecuteNonQuery(insertQuery, cmd =>
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@name", fullName);
                    cmd.Parameters.AddWithValue("@mail", email);
                });

                MessageBox.Show("Signup successful! You can now login.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, nameof(SignupWindow), nameof(Signup_Click));
                MessageBox.Show("Error while signing up: " + ex.Message);
            }
        }

    }
}
