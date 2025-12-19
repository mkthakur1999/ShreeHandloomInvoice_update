using System;
using System.Windows;
using AdminUtilityDashboard.Helpers;

namespace AdminUtilityDashboard.Views
{
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string newPassword = txtNewPassword.Password.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Please enter both Username and New Password.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string query = "UPDATE Users SET Password=@pwd WHERE UserName=@user";
                int rows = 0;

                DBHelper.ExecuteNonQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@pwd", newPassword);
                    cmd.Parameters.AddWithValue("@user", username);
                });

                MessageBox.Show("Password reset successful!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, nameof(ForgotPasswordWindow), nameof(BtnReset_Click));
                MessageBox.Show("Error resetting password: " + ex.Message);
            }
        }
    }
}
