﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Group_Project
{
    public partial class Login : Form
    {
        private String connectionString = Global.CONN_STRING;
        public Login()
        {
            InitializeComponent();
            TBusername.Select();
        }

        private void BTNlogin_Click(object sender, EventArgs e)
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
                            MessageBox.Show("Welcome.");
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

        private void LLaboutus_Click(object sender, EventArgs e)
        {
            AboutUs aboutus = new AboutUs();
            aboutus.Show();
            this.Hide();
        }

        private void BTNregister_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void LLcontactus_Click(object sender, EventArgs e)
        {
            ContactUs contactUs = new ContactUs();
            contactUs.Show();
            this.Hide();
        }

        private void btnShortcut_Click(object sender, EventArgs e)
        {
            Catalog c = new Catalog();
            c.Show();
            this.Hide();
        }
    }
}