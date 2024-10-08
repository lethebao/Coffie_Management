using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Login";
            this.Size = new System.Drawing.Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            Label usernameLabel = new Label();
            usernameLabel.Text = "Username:";
            usernameLabel.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(usernameLabel);

            TextBox usernameTextBox = new TextBox();
            usernameTextBox.Location = new System.Drawing.Point(100, 20);
            usernameTextBox.Width = 150;
            this.Controls.Add(usernameTextBox);

            Label passwordLabel = new Label();
            passwordLabel.Text = "Password:";
            passwordLabel.Location = new System.Drawing.Point(20, 50);
            this.Controls.Add(passwordLabel);

            TextBox passwordTextBox = new TextBox();
            passwordTextBox.Location = new System.Drawing.Point(100, 50);
            passwordTextBox.Width = 150;
            passwordTextBox.PasswordChar = '*';
            this.Controls.Add(passwordTextBox);

            Button loginButton = new Button();
            loginButton.Text = "Login";
            loginButton.Location = new System.Drawing.Point(100, 80);
            loginButton.Click += (sender, e) => Login(usernameTextBox.Text, passwordTextBox.Text);
            this.Controls.Add(loginButton);
        }

        private void Login(string username, string password)
        {
            // For this example, we'll use a hardcoded username and password
            // In a real application, you should use secure authentication methods
            if (username == "admin" && password == "password")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}