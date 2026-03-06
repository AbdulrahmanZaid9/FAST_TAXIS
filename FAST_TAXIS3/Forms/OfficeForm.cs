using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Forms
{
    public partial class OfficeForm : Form
    {
        private DataGridView dgvOffices;
        private Panel pnlForm;
        private TextBox txtOfficeName;
        private TextBox txtCity;
        private TextBox txtAddress;
        private TextBox txtPhone;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewManagers;
        private Label lblOfficeId;
        private int currentOfficeId = 0;

        public OfficeForm()
        {
            InitializeComponent();
            SetupForm();
            LoadOffices();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Office Management";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1000, 600);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "OFFICE MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(350, 400);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Office Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Office ID Label (hidden)
            lblOfficeId = new Label();
            lblOfficeId.Name = "lblOfficeId";
            lblOfficeId.Text = "0";
            lblOfficeId.Visible = false;
            pnlForm.Controls.Add(lblOfficeId);

            // Office Name
            Label lblOfficeName = new Label();
            lblOfficeName.Text = "Office Name:";
            lblOfficeName.Font = new Font("Segoe UI", 11);
            lblOfficeName.ForeColor = Color.Black;
            lblOfficeName.Size = new Size(120, 25);
            lblOfficeName.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblOfficeName);

            txtOfficeName = new TextBox();
            txtOfficeName.Name = "txtOfficeName";
            txtOfficeName.Font = new Font("Segoe UI", 11);
            txtOfficeName.Size = new Size(180, 25);
            txtOfficeName.Location = new Point(140, 70);
            pnlForm.Controls.Add(txtOfficeName);

            // City
            Label lblCity = new Label();
            lblCity.Text = "City:";
            lblCity.Font = new Font("Segoe UI", 11);
            lblCity.ForeColor = Color.Black;
            lblCity.Size = new Size(120, 25);
            lblCity.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblCity);

            txtCity = new TextBox();
            txtCity.Name = "txtCity";
            txtCity.Font = new Font("Segoe UI", 11);
            txtCity.Size = new Size(180, 25);
            txtCity.Location = new Point(140, 110);
            pnlForm.Controls.Add(txtCity);

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Font = new Font("Segoe UI", 11);
            lblAddress.ForeColor = Color.Black;
            lblAddress.Size = new Size(120, 25);
            lblAddress.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblAddress);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Font = new Font("Segoe UI", 11);
            txtAddress.Size = new Size(180, 25);
            txtAddress.Location = new Point(140, 150);
            pnlForm.Controls.Add(txtAddress);

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
            txtPhone.Size = new Size(180, 25);
            txtPhone.Location = new Point(140, 190);
            pnlForm.Controls.Add(txtPhone);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(300, 100);
            pnlButtons.Location = new Point(20, 240);
            pnlButtons.BackColor = Color.Transparent;
            pnlForm.Controls.Add(pnlButtons);

            // Save Button
            btnSave = new Button();
            btnSave.Text = "SAVE";
            btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.BackColor = Color.FromArgb(0, 123, 255);
            btnSave.ForeColor = Color.White;
            btnSave.Size = new Size(80, 35);
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
            btnUpdate.Size = new Size(80, 35);
            btnUpdate.Location = new Point(90, 0);
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
            btnDelete.Size = new Size(80, 35);
            btnDelete.Location = new Point(180, 0);
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
            btnClear.Size = new Size(80, 35);
            btnClear.Location = new Point(0, 45);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += BtnClear_Click;
            pnlButtons.Controls.Add(btnClear);

            // View Managers Button
            btnViewManagers = new Button();
            btnViewManagers.Text = "VIEW MANAGERS";
            btnViewManagers.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnViewManagers.BackColor = Color.FromArgb(255, 128, 0);
            btnViewManagers.ForeColor = Color.White;
            btnViewManagers.Size = new Size(170, 35);
            btnViewManagers.Location = new Point(90, 45);
            btnViewManagers.FlatStyle = FlatStyle.Flat;
            btnViewManagers.FlatAppearance.BorderSize = 0;
            btnViewManagers.Click += BtnViewManagers_Click;
            pnlButtons.Controls.Add(btnViewManagers);

            // DataGridView
            dgvOffices = new DataGridView();
            dgvOffices.Name = "dgvOffices";
            dgvOffices.Size = new Size(750, 500);
            dgvOffices.Location = new Point(410, 80);
            dgvOffices.BackgroundColor = Color.White;
            dgvOffices.BorderStyle = BorderStyle.None;
            dgvOffices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOffices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOffices.MultiSelect = false;
            dgvOffices.ReadOnly = true;
            dgvOffices.RowHeadersVisible = false;
            dgvOffices.AllowUserToAddRows = false;
            dgvOffices.CellClick += DgvOffices_CellClick;

            // DataGridView Style
            dgvOffices.EnableHeadersVisualStyles = false;
            dgvOffices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvOffices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOffices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvOffices.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOffices.ColumnHeadersHeight = 40;

            dgvOffices.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvOffices.DefaultCellStyle.ForeColor = Color.Black;
            dgvOffices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvOffices.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvOffices.DefaultCellStyle.Padding = new Padding(5);
            dgvOffices.RowTemplate.Height = 35;

            this.Controls.Add(dgvOffices);
        }

        private void LoadOffices()
        {
            DataTable dt = OfficeData.GetAllOffices();
            dgvOffices.DataSource = dt;

            // Set column headers
            if (dgvOffices.Columns.Count > 0)
            {
                if (dgvOffices.Columns.Contains("OfficeID"))
                    dgvOffices.Columns["OfficeID"].HeaderText = "ID";
                if (dgvOffices.Columns.Contains("OfficeName"))
                    dgvOffices.Columns["OfficeName"].HeaderText = "Office Name";
                if (dgvOffices.Columns.Contains("City"))
                    dgvOffices.Columns["City"].HeaderText = "City";
                if (dgvOffices.Columns.Contains("Address"))
                    dgvOffices.Columns["Address"].HeaderText = "Address";
                if (dgvOffices.Columns.Contains("Phone"))
                    dgvOffices.Columns["Phone"].HeaderText = "Phone";
            }
        }

        private void DgvOffices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOffices.Rows[e.RowIndex];

                currentOfficeId = Convert.ToInt32(row.Cells["OfficeID"].Value);
                txtOfficeName.Text = row.Cells["OfficeName"].Value.ToString();
                txtCity.Text = row.Cells["City"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();

                lblOfficeId.Text = currentOfficeId.ToString();

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
                int officeId = OfficeData.AddOffice(
                    txtOfficeName.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtPhone.Text.Trim()
                );

                if (officeId > 0)
                {
                    MessageBox.Show("Office saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadOffices();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving office");
                MessageBox.Show("Error saving office: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentOfficeId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                bool result = OfficeData.UpdateOffice(
                    currentOfficeId,
                    txtOfficeName.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtPhone.Text.Trim()
                );

                if (result)
                {
                    MessageBox.Show("Office updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadOffices();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating office");
                MessageBox.Show("Error updating office: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentOfficeId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this office?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = OfficeData.DeleteOffice(currentOfficeId);

                    if (deleted)
                    {
                        MessageBox.Show("Office deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadOffices();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting office");
                    MessageBox.Show("Cannot delete this office. It may be referenced by other records.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnViewManagers_Click(object sender, EventArgs e)
        {
            DataTable dt = OfficeData.GetManagersByOffice();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Managers by Office");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            txtOfficeName.Clear();
            txtCity.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            currentOfficeId = 0;
            lblOfficeId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtOfficeName.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtOfficeName.Text))
            {
                MessageBox.Show("Office name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOfficeName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("City is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCity.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                if (!ValidationHelper.IsValidPhone(txtPhone.Text))
                {
                    MessageBox.Show("Invalid phone number format.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
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