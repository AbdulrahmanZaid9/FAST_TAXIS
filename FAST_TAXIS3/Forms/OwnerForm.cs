using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Forms
{
    public partial class OwnerForm : Form
    {
        private DataGridView dgvOwners;
        private Panel pnlForm;
        private TextBox txtFName;
        private TextBox txtLName;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewTaxis;
        private Button btnMultipleTaxis;
        private Label lblOwnerId;
        private int currentOwnerId = 0;

        public OwnerForm()
        {
            InitializeComponent();
            SetupForm();
            LoadOwners();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Owner Management";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1000, 600);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "OWNER MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(350, 380);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Owner Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Owner ID Label (hidden)
            lblOwnerId = new Label();
            lblOwnerId.Name = "lblOwnerId";
            lblOwnerId.Text = "0";
            lblOwnerId.Visible = false;
            pnlForm.Controls.Add(lblOwnerId);

            // First Name
            Label lblFName = new Label();
            lblFName.Text = "First Name:";
            lblFName.Font = new Font("Segoe UI", 11);
            lblFName.ForeColor = Color.Black;
            lblFName.Size = new Size(120, 25);
            lblFName.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblFName);

            txtFName = new TextBox();
            txtFName.Name = "txtFName";
            txtFName.Font = new Font("Segoe UI", 11);
            txtFName.Size = new Size(180, 25);
            txtFName.Location = new Point(140, 70);
            pnlForm.Controls.Add(txtFName);

            // Last Name
            Label lblLName = new Label();
            lblLName.Text = "Last Name:";
            lblLName.Font = new Font("Segoe UI", 11);
            lblLName.ForeColor = Color.Black;
            lblLName.Size = new Size(120, 25);
            lblLName.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblLName);

            txtLName = new TextBox();
            txtLName.Name = "txtLName";
            txtLName.Font = new Font("Segoe UI", 11);
            txtLName.Size = new Size(180, 25);
            txtLName.Location = new Point(140, 110);
            pnlForm.Controls.Add(txtLName);

            // Phone
            Label lblPhone = new Label();
            lblPhone.Text = "Phone:";
            lblPhone.Font = new Font("Segoe UI", 11);
            lblPhone.ForeColor = Color.Black;
            lblPhone.Size = new Size(120, 25);
            lblPhone.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblPhone);

            txtPhone = new TextBox();
            txtPhone.Name = "txtPhone";
            txtPhone.Font = new Font("Segoe UI", 11);
            txtPhone.Size = new Size(180, 25);
            txtPhone.Location = new Point(140, 150);
            pnlForm.Controls.Add(txtPhone);

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Font = new Font("Segoe UI", 11);
            lblAddress.ForeColor = Color.Black;
            lblAddress.Size = new Size(120, 25);
            lblAddress.Location = new Point(20, 190);
            pnlForm.Controls.Add(lblAddress);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Font = new Font("Segoe UI", 11);
            txtAddress.Size = new Size(180, 25);
            txtAddress.Location = new Point(140, 190);
            pnlForm.Controls.Add(txtAddress);

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

            // View Taxis Button
            btnViewTaxis = new Button();
            btnViewTaxis.Text = "VIEW TAXIS";
            btnViewTaxis.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnViewTaxis.BackColor = Color.FromArgb(255, 128, 0);
            btnViewTaxis.ForeColor = Color.White;
            btnViewTaxis.Size = new Size(170, 35);
            btnViewTaxis.Location = new Point(90, 45);
            btnViewTaxis.FlatStyle = FlatStyle.Flat;
            btnViewTaxis.FlatAppearance.BorderSize = 0;
            btnViewTaxis.Enabled = false;
            btnViewTaxis.Click += BtnViewTaxis_Click;
            pnlButtons.Controls.Add(btnViewTaxis);

            // DataGridView
            dgvOwners = new DataGridView();
            dgvOwners.Name = "dgvOwners";
            dgvOwners.Size = new Size(750, 450);
            dgvOwners.Location = new Point(410, 80);
            dgvOwners.BackgroundColor = Color.White;
            dgvOwners.BorderStyle = BorderStyle.None;
            dgvOwners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOwners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOwners.MultiSelect = false;
            dgvOwners.ReadOnly = true;
            dgvOwners.RowHeadersVisible = false;
            dgvOwners.AllowUserToAddRows = false;
            dgvOwners.CellClick += DgvOwners_CellClick;

            // DataGridView Style
            dgvOwners.EnableHeadersVisualStyles = false;
            dgvOwners.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvOwners.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOwners.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvOwners.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOwners.ColumnHeadersHeight = 40;

            dgvOwners.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvOwners.DefaultCellStyle.ForeColor = Color.Black;
            dgvOwners.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvOwners.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvOwners.DefaultCellStyle.Padding = new Padding(5);
            dgvOwners.RowTemplate.Height = 35;

            this.Controls.Add(dgvOwners);

            // Multiple Taxis Report Button
            btnMultipleTaxis = new Button();
            btnMultipleTaxis.Text = "OWNERS WITH MULTIPLE TAXIS";
            btnMultipleTaxis.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnMultipleTaxis.BackColor = Color.FromArgb(255, 128, 0);
            btnMultipleTaxis.ForeColor = Color.White;
            btnMultipleTaxis.Size = new Size(300, 40);
            btnMultipleTaxis.Location = new Point(410, 540);
            btnMultipleTaxis.FlatStyle = FlatStyle.Flat;
            btnMultipleTaxis.FlatAppearance.BorderSize = 0;
            btnMultipleTaxis.Click += BtnMultipleTaxis_Click;
            this.Controls.Add(btnMultipleTaxis);
        }

        private void LoadOwners()
        {
            DataTable dt = OwnerData.GetAllOwners();
            dgvOwners.DataSource = dt;

            // Set column headers
            if (dgvOwners.Columns.Count > 0)
            {
                if (dgvOwners.Columns.Contains("OwnerID"))
                    dgvOwners.Columns["OwnerID"].HeaderText = "ID";
                if (dgvOwners.Columns.Contains("FName"))
                    dgvOwners.Columns["FName"].HeaderText = "First Name";
                if (dgvOwners.Columns.Contains("LName"))
                    dgvOwners.Columns["LName"].HeaderText = "Last Name";
                if (dgvOwners.Columns.Contains("Phone"))
                    dgvOwners.Columns["Phone"].HeaderText = "Phone";
                if (dgvOwners.Columns.Contains("Address"))
                    dgvOwners.Columns["Address"].HeaderText = "Address";
                if (dgvOwners.Columns.Contains("TaxiCount"))
                    dgvOwners.Columns["TaxiCount"].HeaderText = "Taxis Owned";
            }
        }

        private void DgvOwners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOwners.Rows[e.RowIndex];

                currentOwnerId = Convert.ToInt32(row.Cells["OwnerID"].Value);
                txtFName.Text = row.Cells["FName"].Value.ToString();
                txtLName.Text = row.Cells["LName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();

                lblOwnerId.Text = currentOwnerId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnViewTaxis.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int ownerId = OwnerData.AddOwner(
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtAddress.Text.Trim()
                );

                if (ownerId > 0)
                {
                    MessageBox.Show("Owner saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadOwners();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving owner");
                MessageBox.Show("Error saving owner: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentOwnerId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                bool result = OwnerData.UpdateOwner(
                    currentOwnerId,
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtAddress.Text.Trim()
                );

                if (result)
                {
                    MessageBox.Show("Owner updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadOwners();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating owner");
                MessageBox.Show("Error updating owner: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentOwnerId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this owner?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = OwnerData.DeleteOwner(currentOwnerId);

                    if (deleted)
                    {
                        MessageBox.Show("Owner deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadOwners();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting owner");
                    MessageBox.Show("Cannot delete this owner. They may have taxis registered.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnViewTaxis_Click(object sender, EventArgs e)
        {
            if (currentOwnerId == 0)
                return;

            DataTable dt = TaxiData.GetTaxisByOwner(currentOwnerId);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, $"Taxis Owned by {txtFName.Text} {txtLName.Text}");
            reportForm.ShowDialog();
        }

        private void BtnMultipleTaxis_Click(object sender, EventArgs e)
        {
            DataTable dt = OwnerData.GetOwnersWithMultipleTaxis();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Owners with Multiple Taxis");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            currentOwnerId = 0;
            lblOwnerId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnViewTaxis.Enabled = false;

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