using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Login";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "FAST TAXIS";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblTitle.Size = new Size(200, 40);
            lblTitle.Location = new Point(125, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitle);

            // Subtitle Label
            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Database Management System";
            lblSubtitle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Size = new Size(200, 20);
            lblSubtitle.Location = new Point(125, 70);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblSubtitle);
            // Staff ID Label
            Label lblStaffID = new Label();
            lblStaffID.Text = "Staff ID:";
            lblStaffID.Font = new Font("Segoe UI", 11);
            lblStaffID.ForeColor = Color.Black;
            lblStaffID.Size = new Size(100, 25);          // ✅ عرض ثابت
            lblStaffID.Location = new Point(20, 130);      // ✅ نفس المكان
            lblStaffID.TextAlign = ContentAlignment.MiddleRight;
            lblStaffID.TabIndex = 0;
            this.Controls.Add(lblStaffID);

            // Staff ID TextBox
            TextBox txtStaffID = new TextBox();
            txtStaffID.Name = "txtStaffID";
            txtStaffID.Font = new Font("Segoe UI", 11);
            txtStaffID.Size = new Size(220, 25);
            txtStaffID.Location = new Point(140, 130);     // ✅ يبدأ بعد الـ Label مباشرة
            txtStaffID.TabIndex = 1;
            this.Controls.Add(txtStaffID);

            // Password Label
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Font = new Font("Segoe UI", 11);
            lblPassword.ForeColor = Color.Black;
            lblPassword.Size = new Size(100, 25);          // ✅ نفس عرض Label الأول
            lblPassword.Location = new Point(30, 170);      // ✅ نفس X حق الأول
            lblPassword.TextAlign = ContentAlignment.MiddleRight;
            lblPassword.TabIndex = 2;
            this.Controls.Add(lblPassword);

            // Password TextBox
            TextBox txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.Size = new Size(220, 25);
            txtPassword.Location = new Point(140, 170);     // ✅ نفس X حق الأول
            txtPassword.PasswordChar = '•';
            txtPassword.TabIndex = 3;
            this.Controls.Add(txtPassword);

            // Login Button
            Button btnLogin = new Button();
            btnLogin.Text = "LOGIN";
            btnLogin.Name = "btnLogin";
            btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogin.BackColor = Color.FromArgb(255, 128, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Size = new Size(200, 35);
            btnLogin.Location = new Point(125, 240);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.TabIndex = 4;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Message Label
            Label lblMessage = new Label();
            lblMessage.Name = "lblMessage";
            lblMessage.Text = "";
            lblMessage.Font = new Font("Segoe UI", 9);
            lblMessage.ForeColor = Color.Red;
            lblMessage.Size = new Size(300, 20);
            lblMessage.Location = new Point(75, 285);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblMessage);
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            TextBox txtStaffID = this.Controls["txtStaffID"] as TextBox;
            TextBox txtPassword = this.Controls["txtPassword"] as TextBox;
            Label lblMessage = this.Controls["lblMessage"] as Label;

            if (string.IsNullOrWhiteSpace(txtStaffID.Text))
            {
                lblMessage.Text = "Please enter Staff ID";
                txtStaffID.Focus();
                return;
            }

            if (!ValidationHelper.IsNumeric(txtStaffID.Text))
            {
                lblMessage.Text = "Staff ID must be numeric";
                txtStaffID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMessage.Text = "Please enter Password";
                txtPassword.Focus();
                return;
            }

            int staffId = Convert.ToInt32(txtStaffID.Text);
            string password = txtPassword.Text.Trim();

            string query = @"
        SELECT s.*, 
               CASE 
                   WHEN a.JobTitle = 'Director' THEN 'Director'
                   WHEN a.StaffID IS NOT NULL THEN 'Admin'
                   WHEN m.StaffID IS NOT NULL THEN 'Manager'
                   WHEN d.StaffID IS NOT NULL THEN 'Driver'
                   ELSE 'Staff'
               END AS StaffType,
               o.OfficeName
        FROM Staff s
        LEFT JOIN Manager m ON s.StaffID = m.StaffID
        LEFT JOIN AdminStaff a ON s.StaffID = a.StaffID
        LEFT JOIN Driver d ON s.StaffID = d.StaffID
        LEFT JOIN Office o ON s.OfficeID = o.OfficeID
        WHERE s.StaffID = @StaffID AND s.Password = @Password";

            SqlParameter[] parameters = {
        new SqlParameter("@StaffID", staffId),
        new SqlParameter("@Password", password)
    };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string staffName = row["FName"].ToString() + " " + row["LName"].ToString();
                string role = row["StaffType"].ToString();
                int officeId = Convert.ToInt32(row["OfficeID"]);
                string officeName = row["OfficeName"].ToString();

                SessionManager.Login(staffId, staffName, role, officeId, officeName);

                // ✅ أهم سطرين في الكود
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblMessage.Text = "Invalid Staff ID or Password";
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // فقط إذا كان المستخدم قفل النافذة بدون تسجيل دخول
            if (this.DialogResult != DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}