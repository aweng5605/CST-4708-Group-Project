﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Group_Project
{
    public partial class Login : Form
    {
        private readonly string connectionString = Global.CONN_STRING;

        public Login()
        {
            InitializeComponent();
            TBusername.Select();
        }

        private void BTNlogin_Click(object sender, EventArgs e)
        {
            // TODO: Guest login?
            LoginCheck();
        }

        private void BTNregister_Click(object sender, EventArgs e)
        {
            Global.ShowRegistration();
            this.Hide();
        }

        private void btnShortcut_Click(object sender, EventArgs e)
        {
            loadCatalog();
        }

        private void LLaboutus_Click(object sender, EventArgs e)
        {
            Global.ShowAboutUs();
            this.Hide();
        }

        private void LLcontactus_Click(object sender, EventArgs e)
        {
            Global.ShowContactUs();
            this.Hide();
        }

        private void loadCatalog()
        {
            String username = TBusername.Text;
            if (String.IsNullOrEmpty(username))
            {
                username = "Guest";
            }
            Global.ShowCatalog(username);
            this.Hide();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginCheck()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE Username = @username and Password = @password", connection))
                {
                    command.Parameters.AddWithValue("@username", TBusername.Text);
                    command.Parameters.AddWithValue("@password", TBpassword.Text);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            loadCatalog();
                        }
                        else if (TBusername.Text == "" || TBpassword.Text == "")
                        {
                            MessageBox.Show("Please enter a username and a password.", "Required Fields Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Account Information. Please Try Again.", "Invalid Account Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void TBpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginCheck();
            }
        }
    }
}