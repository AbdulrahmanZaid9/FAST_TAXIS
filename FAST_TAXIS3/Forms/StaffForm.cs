using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class StaffForm : Form
    {
        private DataGridView dgvStaff;
        private Panel pnlForm;
        private ComboBox cmbStaffType;
        private TextBox txtFName;
        private TextBox txtLName;
        private TextBox txtPhone;
        private ComboBox cmbGender;
        private DateTimePicker dtpDOB;
        private TextBox txtAddress;
        private ComboBox cmbOffice;
        private TextBox txtLicenseNo;
        private TextBox txtJobTitle;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label lblStaffId;
        private int currentStaffId = 0;
        private TextBox txtPassword;  
        public StaffForm()
        {
            InitializeComponent();
            SetupForm();
            LoadOffices();
            LoadStaff();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Staff Management";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1200, 700);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "STAFF MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(450, 650); // زودنا الطول عشان يتسع
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Staff Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Staff ID Label (hidden)
            lblStaffId = new Label();
            lblStaffId.Name = "lblStaffId";
            lblStaffId.Text = "0";
            lblStaffId.Visible = false;
            pnlForm.Controls.Add(lblStaffId);

            // Staff Type
            Label lblStaffType = new Label();
            lblStaffType.Text = "Staff Type:";
            lblStaffType.Font = new Font("Segoe UI", 11);
            lblStaffType.ForeColor = Color.Black;
            lblStaffType.Size = new Size(120, 25);
            lblStaffType.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblStaffType);

            cmbStaffType = new ComboBox();
            cmbStaffType.Name = "cmbStaffType";
            cmbStaffType.Font = new Font("Segoe UI", 11);
            cmbStaffType.Size = new Size(280, 25);
            cmbStaffType.Location = new Point(140, 70);
            cmbStaffType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStaffType.Items.AddRange(new string[] { "Manager", "Admin", "Driver" });
            cmbStaffType.SelectedIndexChanged += CmbStaffType_SelectedIndexChanged;
            pnlForm.Controls.Add(cmbStaffType);

            // First Name
            Label lblFName = new Label();
            lblFName.Text = "First Name:";
            lblFName.Font = new Font("Segoe UI", 11);
            lblFName.ForeColor = Color.Black;
            lblFName.Size = new Size(120, 25);
            lblFName.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblFName);

            txtFName = new TextBox();
            txtFName.Name = "txtFName";
            txtFName.Font = new Font("Segoe UI", 11);
            txtFName.Size = new Size(280, 25);
            txtFName.Location = new Point(140, 110);
            pnlForm.Controls.Add(txtFName);

            // Last Name
            Label lblLName = new Label();
            lblLName.Text = "Last Name:";
            lblLName.Font = new Font("Segoe UI", 11);
            lblLName.ForeColor = Color.Black;
            lblLName.Size = new Size(120, 25);
            lblLName.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblLName);

            txtLName = new TextBox();
            txtLName.Name = "txtLName";
            txtLName.Font = new Font("Segoe UI", 11);
            txtLName.Size = new Size(280, 25);
            txtLName.Location = new Point(140, 150);
            pnlForm.Controls.Add(txtLName);

            // Phone
            Label lblPhone = new Label();
            lblPhone.Text = "Phone:";
            lblPhone.Font = new Font("Segoe UI", 11);
            lblPhone.ForeColor = Color.Black;
            lblPhone.Size = new Size(120, 25);
            lblPhone.Location = new Point(20, 190);
            pnlForm.Controls.Add(lblPhone);

            txtPhone = new TextBox();
            txtPhone.Name = "txtPhone";
            txtPhone.Font = new Font("Segoe UI", 11);
            txtPhone.Size = new Size(280, 25);
            txtPhone.Location = new Point(140, 190);
            pnlForm.Controls.Add(txtPhone);

            // Gender
            Label lblGender = new Label();
            lblGender.Text = "Gender:";
            lblGender.Font = new Font("Segoe UI", 11);
            lblGender.ForeColor = Color.Black;
            lblGender.Size = new Size(120, 25);
            lblGender.Location = new Point(20, 230);
            pnlForm.Controls.Add(lblGender);

            cmbGender = new ComboBox();
            cmbGender.Name = "cmbGender";
            cmbGender.Font = new Font("Segoe UI", 11);
            cmbGender.Size = new Size(280, 25);
            cmbGender.Location = new Point(140, 230);
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Items.AddRange(new string[] { "M", "F" });
            pnlForm.Controls.Add(cmbGender);

            // Date of Birth
            Label lblDOB = new Label();
            lblDOB.Text = "Date of Birth:";
            lblDOB.Font = new Font("Segoe UI", 11);
            lblDOB.ForeColor = Color.Black;
            lblDOB.Size = new Size(120, 25);
            lblDOB.Location = new Point(20, 270);
            pnlForm.Controls.Add(lblDOB);

            dtpDOB = new DateTimePicker();
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Font = new Font("Segoe UI", 11);
            dtpDOB.Size = new Size(280, 25);
            dtpDOB.Location = new Point(140, 270);
            dtpDOB.Format = DateTimePickerFormat.Short;
            pnlForm.Controls.Add(dtpDOB);

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Font = new Font("Segoe UI", 11);
            lblAddress.ForeColor = Color.Black;
            lblAddress.Size = new Size(120, 25);
            lblAddress.Location = new Point(20, 310);
            pnlForm.Controls.Add(lblAddress);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Font = new Font("Segoe UI", 11);
            txtAddress.Size = new Size(280, 25);
            txtAddress.Location = new Point(140, 310);
            pnlForm.Controls.Add(txtAddress);

            // Office
            Label lblOffice = new Label();
            lblOffice.Text = "Office:";
            lblOffice.Font = new Font("Segoe UI", 11);
            lblOffice.ForeColor = Color.Black;
            lblOffice.Size = new Size(120, 25);
            lblOffice.Location = new Point(20, 350);
            pnlForm.Controls.Add(lblOffice);

            cmbOffice = new ComboBox();
            cmbOffice.Name = "cmbOffice";
            cmbOffice.Font = new Font("Segoe UI", 11);
            cmbOffice.Size = new Size(280, 25);
            cmbOffice.Location = new Point(140, 350);
            cmbOffice.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbOffice);

            // Password
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Font = new Font("Segoe UI", 11);
            lblPassword.Location = new Point(20, 390);
            lblPassword.Size = new Size(120, 25);
            pnlForm.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.Size = new Size(280, 25);
            txtPassword.Location = new Point(140, 390);
            txtPassword.PasswordChar = '•';
            pnlForm.Controls.Add(txtPassword);

            // License No (for Driver)
            Label lblLicenseNo = new Label();
            lblLicenseNo.Text = "License No:";
            lblLicenseNo.Font = new Font("Segoe UI", 11);
            lblLicenseNo.ForeColor = Color.Black;
            lblLicenseNo.Size = new Size(120, 25);
            lblLicenseNo.Location = new Point(20, 430); // تحت Password
            lblLicenseNo.Visible = false;
            pnlForm.Controls.Add(lblLicenseNo);

            txtLicenseNo = new TextBox();
            txtLicenseNo.Name = "txtLicenseNo";
            txtLicenseNo.Font = new Font("Segoe UI", 11);
            txtLicenseNo.Size = new Size(280, 25);
            txtLicenseNo.Location = new Point(140, 430); // تحت Password
            txtLicenseNo.Visible = false;
            pnlForm.Controls.Add(txtLicenseNo);

            // Job Title (for Admin)
            Label lblJobTitle = new Label();
            lblJobTitle.Text = "Job Title:";
            lblJobTitle.Font = new Font("Segoe UI", 11);
            lblJobTitle.ForeColor = Color.Black;
            lblJobTitle.Size = new Size(120, 25);
            lblJobTitle.Location = new Point(20, 430); // تحت License No
            lblJobTitle.Visible = false;
            pnlForm.Controls.Add(lblJobTitle);

            txtJobTitle = new TextBox();
            txtJobTitle.Name = "txtJobTitle";
            txtJobTitle.Font = new Font("Segoe UI", 11);
            txtJobTitle.Size = new Size(280, 25);
            txtJobTitle.Location = new Point(140, 430); // تحت License No
            txtJobTitle.Visible = false;
            pnlForm.Controls.Add(txtJobTitle);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(400, 100);
            pnlButtons.Location = new Point(20, 520); // تحت Job Title
            pnlButtons.BackColor = Color.Transparent;
            pnlForm.Controls.Add(pnlButtons);

            // Save Button
            btnSave = new Button();
            btnSave.Text = "SAVE";
            btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.BackColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.Size = new Size(90, 35);
            btnSave.Location = new Point(0, 0);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            pnlButtons.Controls.Add(btnSave);

            // Update Button
            btnUpdate = new Button();
            btnUpdate.Text = "UPDATE";
            btnUpdate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnUpdate.BackColor = Color.FromArgb(40, 167, 69);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Size = new Size(90, 35);
            btnUpdate.Location = new Point(100, 0);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Enabled = false;
            btnUpdate.Click += BtnUpdate_Click;
            pnlButtons.Controls.Add(btnUpdate);

            // Delete Button
            btnDelete = new Button();
            btnDelete.Text = "DELETE";
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.ForeColor = Color.White;
            btnDelete.Size = new Size(90, 35);
            btnDelete.Location = new Point(200, 0);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Enabled = false;
            btnDelete.Click += BtnDelete_Click;
            pnlButtons.Controls.Add(btnDelete);

            // Clear Button
            btnClear = new Button();
            btnClear.Text = "CLEAR";
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.ForeColor = Color.White;
            btnClear.Size = new Size(90, 35);
            btnClear.Location = new Point(300, 0);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += BtnClear_Click;
            pnlButtons.Controls.Add(btnClear);

            // DataGridView
            dgvStaff = new DataGridView();
            dgvStaff.Name = "dgvStaff";
            dgvStaff.Size = new Size(850, 600);
            dgvStaff.Location = new Point(500, 80);
            dgvStaff.BackgroundColor = Color.White;
            dgvStaff.BorderStyle = BorderStyle.None;
            dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.MultiSelect = false;
            dgvStaff.ReadOnly = true;
            dgvStaff.RowHeadersVisible = false;
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.CellClick += DgvStaff_CellClick;

            // DataGridView Style
            dgvStaff.EnableHeadersVisualStyles = false;
            dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStaff.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvStaff.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStaff.ColumnHeadersHeight = 40;

            dgvStaff.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvStaff.DefaultCellStyle.ForeColor = Color.Black;
            dgvStaff.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvStaff.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvStaff.DefaultCellStyle.Padding = new Padding(5);
            dgvStaff.RowTemplate.Height = 35;

            this.Controls.Add(dgvStaff);

            // Female Drivers Report Button
            Button btnFemaleDrivers = new Button();
            btnFemaleDrivers.Text = "FEMALE DRIVERS - CYBERJAYA";
            btnFemaleDrivers.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnFemaleDrivers.BackColor = Color.FromArgb(255, 128, 0);
            btnFemaleDrivers.ForeColor = Color.White;
            btnFemaleDrivers.Size = new Size(250, 40);
            btnFemaleDrivers.Location = new Point(500, 690);
            btnFemaleDrivers.FlatStyle = FlatStyle.Flat;
            btnFemaleDrivers.FlatAppearance.BorderSize = 0;
            btnFemaleDrivers.Click += BtnFemaleDrivers_Click;
            this.Controls.Add(btnFemaleDrivers);

            // Drivers Over 25 Button
            Button btnDriversOver25 = new Button();
            btnDriversOver25.Text = "DRIVERS OVER 25";
            btnDriversOver25.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDriversOver25.BackColor = Color.FromArgb(0, 123, 255);
            btnDriversOver25.ForeColor = Color.White;
            btnDriversOver25.Size = new Size(200, 40);
            btnDriversOver25.Location = new Point(760, 690);
            btnDriversOver25.FlatStyle = FlatStyle.Flat;
            btnDriversOver25.FlatAppearance.BorderSize = 0;
            btnDriversOver25.Click += BtnDriversOver25_Click;
            this.Controls.Add(btnDriversOver25);
        }

        private void LoadOffices()
        {
            DataTable dt = OfficeData.GetAllOffices();
            cmbOffice.DisplayMember = "OfficeName";
            cmbOffice.ValueMember = "OfficeID";
            cmbOffice.DataSource = dt;
        }

        private void LoadStaff()
        {
            DataTable dt;

            if (SessionManager.CurrentStaffRole == "Manager")
            {
                // ✅ مدير يشوف موظفين مكتبه فقط
                dt = StaffData.GetStaffByOffice(SessionManager.CurrentOfficeID);
            }
            else
            {
                // ✅ Admin و Director يشوفون الكل
                dt = StaffData.GetAllStaff();
            }

            dgvStaff.DataSource = dt;
        }

        private void CmbStaffType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cmbStaffType.SelectedItem?.ToString();

            // Hide all dynamic fields
            txtLicenseNo.Visible = false;
            var licenseLabel = txtLicenseNo.Parent.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Text == "License No:");

            if (licenseLabel != null)
            {
                licenseLabel.Visible = false;
            }
            txtJobTitle.Visible = false;
            var jobTitleLabel = txtJobTitle.Parent.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Text == "Job Title:");

            if (jobTitleLabel != null)
            {
                jobTitleLabel.Visible = false;
            }
            // Show fields based on selected type
            if (selectedType == "Driver")
            {
                txtLicenseNo.Visible = true;
                var lblLicenseNo = txtLicenseNo.Parent.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == "License No:");

                if (lblLicenseNo != null)
                    lblLicenseNo.Visible = true;
            }
            else if (selectedType == "Admin")
            {
                txtJobTitle.Visible = true;
                var lblJobTitle = txtJobTitle.Parent.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == "Job Title:");

                if (lblJobTitle != null)
                    lblJobTitle.Visible = true;
            }
        }
        private void DgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaff.Rows[e.RowIndex];

                currentStaffId = Convert.ToInt32(row.Cells["StaffID"].Value);
                txtFName.Text = row.Cells["FName"].Value.ToString();
                txtLName.Text = row.Cells["LName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                cmbGender.SelectedItem = row.Cells["Gender"].Value?.ToString();

                if (row.Cells["DateOfBirth"].Value != DBNull.Value)
                    dtpDOB.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);

                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                cmbOffice.SelectedValue = Convert.ToInt32(row.Cells["OfficeID"].Value);

                string staffType = row.Cells["StaffType"].Value?.ToString();
                cmbStaffType.SelectedItem = staffType;

                // ✅ جلب كلمة المرور
                if (dgvStaff.Columns.Contains("Password") && row.Cells["Password"].Value != DBNull.Value)
                {
                    txtPassword.Text = row.Cells["Password"].Value.ToString();
                }
                else
                {
                    txtPassword.Text = "";
                }

                // ========== الحل الثاني: جلب الرخصة و JobTitle من قاعدة البيانات ==========
                try
                {
                    // ✅ جلب رقم الرخصة إذا كان Driver
                    if (staffType == "Driver")
                    {
                        string licenseQuery = "SELECT LicenseNo FROM Driver WHERE StaffID = @StaffID";
                        SqlParameter[] licenseParams = { new SqlParameter("@StaffID", currentStaffId) };
                        object licenseResult = DatabaseHelper.ExecuteScalar(licenseQuery, licenseParams);
                        txtLicenseNo.Text = licenseResult?.ToString() ?? "";
                    }
                    else
                    {
                        txtLicenseNo.Text = "";
                    }

                    // ✅ جلب JobTitle إذا كان Admin
                    if (staffType == "Admin")
                    {
                        string jobQuery = "SELECT JobTitle FROM AdminStaff WHERE StaffID = @StaffID";
                        SqlParameter[] jobParams = { new SqlParameter("@StaffID", currentStaffId) };
                        object jobResult = DatabaseHelper.ExecuteScalar(jobQuery, jobParams);
                        txtJobTitle.Text = jobResult?.ToString() ?? "";
                    }
                    else
                    {
                        txtJobTitle.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    // لو حصل خطأ، نخلي الحقل فاضي
                    txtLicenseNo.Text = "";
                    txtJobTitle.Text = "";
                }
                // =====================================================================

                lblStaffId.Text = currentStaffId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // Add Staff
                int staffId = StaffData.AddStaff(
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    cmbGender.SelectedItem?.ToString(),
                    dtpDOB.Value,
                    txtAddress.Text.Trim(),
                    Convert.ToInt32(cmbOffice.SelectedValue),
                    txtPassword.Text.Trim()

                );

                if (staffId > 0)
                {
                    string staffType = cmbStaffType.SelectedItem.ToString();

                    // Add subtype based on selection
                    if (staffType == "Manager")
                    {
                        StaffData.AddManager(staffId);
                    }
                    else if (staffType == "Admin")
                    {
                        StaffData.AddAdminStaff(staffId, txtJobTitle.Text.Trim());
                    }
                    else if (staffType == "Driver")
                    {
                        StaffData.AddDriver(staffId, txtLicenseNo.Text.Trim());
                    }

                    MessageBox.Show("Staff saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadStaff();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving staff");
                MessageBox.Show("Error saving staff: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentStaffId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                // Update Staff
                bool result = StaffData.UpdateStaff(
                    currentStaffId,
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    cmbGender.SelectedItem?.ToString(),
                    dtpDOB.Value,
                    txtAddress.Text.Trim(),
                    Convert.ToInt32(cmbOffice.SelectedValue)
                );

                if (result)
                {
                    string staffType = cmbStaffType.SelectedItem.ToString();

                    // Update subtype
                    if (staffType == "Admin")
                    {
                        StaffData.UpdateAdminStaff(currentStaffId, txtJobTitle.Text.Trim());
                    }
                    else if (staffType == "Driver")
                    {
                        StaffData.UpdateDriver(currentStaffId, txtLicenseNo.Text.Trim());
                    }

                    MessageBox.Show("Staff updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadStaff();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating staff");
                MessageBox.Show("Error updating staff: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentStaffId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this staff member?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = StaffData.DeleteStaff(currentStaffId);

                    if (deleted)
                    {
                        MessageBox.Show("Staff deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadStaff();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting staff");
                    MessageBox.Show("Cannot delete this staff member. They may be referenced by other records.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnFemaleDrivers_Click(object sender, EventArgs e)
        {
            DataTable dt = StaffData.GetFemaleDriversByOffice("Cyberjaya");

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Female Drivers - Cyberjaya Office");
            reportForm.ShowDialog();
        }

        private void BtnDriversOver25_Click(object sender, EventArgs e)
        {
            DataTable dt = StaffData.GetDriversOver25();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Drivers Over 25 Years Old");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtPhone.Clear();
            cmbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now.AddYears(-30);
            txtAddress.Clear();
            cmbOffice.SelectedIndex = -1;
            cmbStaffType.SelectedIndex = -1;
            txtLicenseNo.Clear();
            txtLicenseNo.Visible = false;
            txtJobTitle.Clear();
            txtJobTitle.Visible = false;
            currentStaffId = 0;
            lblStaffId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtFName.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                MessageBox.Show("First name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                MessageBox.Show("Last name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLName.Focus();
                return false;
            }

            if (cmbStaffType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select staff type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStaffType.Focus();
                return false;
            }

            if (cmbOffice.SelectedIndex == -1)
            {
                MessageBox.Show("Please select office.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOffice.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidAge(dtpDOB.Value, 18))
            {
                MessageBox.Show("Staff must be at least 18 years old.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDOB.Focus();
                return false;
            }

            if (cmbStaffType.SelectedItem.ToString() == "Driver")
            {
                if (string.IsNullOrWhiteSpace(txtLicenseNo.Text))
                {
                    MessageBox.Show("License number is required for drivers.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLicenseNo.Focus();
                    return false;
                }

                if (!ValidationHelper.IsValidLicenseNumber(txtLicenseNo.Text))
                {
                    MessageBox.Show("Invalid license number format.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLicenseNo.Focus();
                    return false;
                }
            }

            if (cmbStaffType.SelectedItem.ToString() == "Admin")
            {
                if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
                {
                    MessageBox.Show("Job title is required for admin staff.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtJobTitle.Focus();
                    return false;
                }
            }

            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.Dispose();
        }
    }
}