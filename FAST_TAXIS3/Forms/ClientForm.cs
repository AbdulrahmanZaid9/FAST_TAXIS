using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class ClientForm : Form
    {
        private DataGridView dgvClients;
        private Panel pnlForm;
        private ComboBox cmbClientType;
        private TextBox txtFName;
        private TextBox txtLName;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtCity;
        private TextBox txtCompanyName;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewContracts;
        private Button btnPrivateReport;
        private Button btnBusinessReport;
        private Label lblClientId;
        private int currentClientId = 0;

        public ClientForm()
        {
            InitializeComponent();
            SetupForm();
            LoadClients();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Client Management";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1200, 700);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "CLIENT MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(450, 550);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Client Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Client ID Label (hidden)
            lblClientId = new Label();
            lblClientId.Name = "lblClientId";
            lblClientId.Text = "0";
            lblClientId.Visible = false;
            pnlForm.Controls.Add(lblClientId);

            // Client Type
            Label lblClientType = new Label();
            lblClientType.Text = "Client Type:";
            lblClientType.Font = new Font("Segoe UI", 11);
            lblClientType.ForeColor = Color.Black;
            lblClientType.Size = new Size(120, 25);
            lblClientType.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblClientType);

            cmbClientType = new ComboBox();
            cmbClientType.Name = "cmbClientType";
            cmbClientType.Font = new Font("Segoe UI", 11);
            cmbClientType.Size = new Size(280, 25);
            cmbClientType.Location = new Point(140, 70);
            cmbClientType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientType.Items.AddRange(new string[] { "Private", "Business" });
            cmbClientType.SelectedIndexChanged += CmbClientType_SelectedIndexChanged;
            pnlForm.Controls.Add(cmbClientType);

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

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Font = new Font("Segoe UI", 11);
            lblAddress.ForeColor = Color.Black;
            lblAddress.Size = new Size(120, 25);
            lblAddress.Location = new Point(20, 230);
            pnlForm.Controls.Add(lblAddress);

            txtAddress = new TextBox();
            txtAddress.Name = "txtAddress";
            txtAddress.Font = new Font("Segoe UI", 11);
            txtAddress.Size = new Size(280, 25);
            txtAddress.Location = new Point(140, 230);
            pnlForm.Controls.Add(txtAddress);

            // City
            Label lblCity = new Label();
            lblCity.Text = "City:";
            lblCity.Font = new Font("Segoe UI", 11);
            lblCity.ForeColor = Color.Black;
            lblCity.Size = new Size(120, 25);
            lblCity.Location = new Point(20, 270);
            pnlForm.Controls.Add(lblCity);

            txtCity = new TextBox();
            txtCity.Name = "txtCity";
            txtCity.Font = new Font("Segoe UI", 11);
            txtCity.Size = new Size(280, 25);
            txtCity.Location = new Point(140, 270);
            pnlForm.Controls.Add(txtCity);

            // Company Name (for Business clients)
            Label lblCompanyName = new Label();
            lblCompanyName.Text = "Company Name:";
            lblCompanyName.Font = new Font("Segoe UI", 11);
            lblCompanyName.ForeColor = Color.Black;
            lblCompanyName.Size = new Size(120, 25);
            lblCompanyName.Location = new Point(20, 310);
            lblCompanyName.Visible = false;
            pnlForm.Controls.Add(lblCompanyName);

            txtCompanyName = new TextBox();
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Font = new Font("Segoe UI", 11);
            txtCompanyName.Size = new Size(280, 25);
            txtCompanyName.Location = new Point(140, 310);
            txtCompanyName.Visible = false;
            pnlForm.Controls.Add(txtCompanyName);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(400, 100);
            pnlButtons.Location = new Point(20, 360);
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

            // View Contracts Button
            btnViewContracts = new Button();
            btnViewContracts.Text = "VIEW CONTRACTS";
            btnViewContracts.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnViewContracts.BackColor = Color.FromArgb(255, 128, 0);
            btnViewContracts.ForeColor = Color.White;
            btnViewContracts.Size = new Size(190, 35);
            btnViewContracts.Location = new Point(0, 45);
            btnViewContracts.FlatStyle = FlatStyle.Flat;
            btnViewContracts.FlatAppearance.BorderSize = 0;
            btnViewContracts.Enabled = false;
            btnViewContracts.Click += BtnViewContracts_Click;
            pnlButtons.Controls.Add(btnViewContracts);

            // DataGridView
            dgvClients = new DataGridView();
            dgvClients.Name = "dgvClients";
            dgvClients.Size = new Size(850, 550);
            dgvClients.Location = new Point(500, 80);
            dgvClients.BackgroundColor = Color.White;
            dgvClients.BorderStyle = BorderStyle.None;
            dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.MultiSelect = false;
            dgvClients.ReadOnly = true;
            dgvClients.RowHeadersVisible = false;
            dgvClients.AllowUserToAddRows = false;
            dgvClients.CellClick += DgvClients_CellClick;

            // DataGridView Style
            dgvClients.EnableHeadersVisualStyles = false;
            dgvClients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClients.ColumnHeadersHeight = 40;

            dgvClients.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvClients.DefaultCellStyle.ForeColor = Color.Black;
            dgvClients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvClients.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvClients.DefaultCellStyle.Padding = new Padding(5);
            dgvClients.RowTemplate.Height = 35;

            this.Controls.Add(dgvClients);

            // Report Buttons Panel
            Panel pnlReports = new Panel();
            pnlReports.Size = new Size(850, 60);
            pnlReports.Location = new Point(500, 640);
            pnlReports.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlReports);

            // Private Clients by City Button
            btnPrivateReport = new Button();
            btnPrivateReport.Text = "PRIVATE CLIENTS BY CITY";
            btnPrivateReport.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPrivateReport.BackColor = Color.FromArgb(0, 123, 255);
            btnPrivateReport.ForeColor = Color.White;
            btnPrivateReport.Size = new Size(220, 40);
            btnPrivateReport.Location = new Point(20, 10);
            btnPrivateReport.FlatStyle = FlatStyle.Flat;
            btnPrivateReport.FlatAppearance.BorderSize = 0;
            btnPrivateReport.Click += BtnPrivateReport_Click;
            pnlReports.Controls.Add(btnPrivateReport);

            // Business Clients Cyberjaya Button
            btnBusinessReport = new Button();
            btnBusinessReport.Text = "BUSINESS CLIENTS - CYBERJAYA";
            btnBusinessReport.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBusinessReport.BackColor = Color.FromArgb(255, 128, 0);
            btnBusinessReport.ForeColor = Color.White;
            btnBusinessReport.Size = new Size(280, 40);
            btnBusinessReport.Location = new Point(260, 10);
            btnBusinessReport.FlatStyle = FlatStyle.Flat;
            btnBusinessReport.FlatAppearance.BorderSize = 0;
            btnBusinessReport.Click += BtnBusinessReport_Click;
            pnlReports.Controls.Add(btnBusinessReport);

            // Private Clients Nov 2025 Button
            Button btnPrivateNov2025 = new Button();
            btnPrivateNov2025.Text = "PRIVATE CLIENTS - NOV 2025";
            btnPrivateNov2025.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPrivateNov2025.BackColor = Color.FromArgb(40, 167, 69);
            btnPrivateNov2025.ForeColor = Color.White;
            btnPrivateNov2025.Size = new Size(240, 40);
            btnPrivateNov2025.Location = new Point(560, 10);
            btnPrivateNov2025.FlatStyle = FlatStyle.Flat;
            btnPrivateNov2025.FlatAppearance.BorderSize = 0;
            btnPrivateNov2025.Click += BtnPrivateNov2025_Click;
            pnlReports.Controls.Add(btnPrivateNov2025);
        }

        private void LoadClients()
        {
            DataTable dt = ClientData.GetAllClients();
            dgvClients.DataSource = dt;
        }

        private void CmbClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cmbClientType.SelectedItem?.ToString();

            var companyLabel = txtCompanyName.Parent.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Text == "Company Name:");

            if (selectedType == "Business")
            {
                txtCompanyName.Visible = true;

                if (companyLabel != null)
                {
                    companyLabel.Visible = true;
                }

                btnViewContracts.Enabled = currentClientId > 0;
            }
            else
            {
                txtCompanyName.Visible = false;

                if (companyLabel != null)
                {
                    companyLabel.Visible = false;
                }

                btnViewContracts.Enabled = false;
            }
        }

        private void DgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClients.Rows[e.RowIndex];

                currentClientId = Convert.ToInt32(row.Cells["ClientID"].Value);
                txtFName.Text = row.Cells["FName"].Value.ToString();
                txtLName.Text = row.Cells["LName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                txtCity.Text = row.Cells["City"].Value?.ToString();

                string clientType = row.Cells["ClientType"].Value?.ToString();
                cmbClientType.SelectedItem = clientType;

                if (clientType == "Business" && dgvClients.Columns.Contains("CompanyName"))
                {
                    txtCompanyName.Text = row.Cells["CompanyName"].Value?.ToString();
                    btnViewContracts.Enabled = true;
                }

                lblClientId.Text = currentClientId.ToString();

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
                // Add Client
                int clientId = ClientData.AddClient(
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtCity.Text.Trim()
                );

                if (clientId > 0)
                {
                    string clientType = cmbClientType.SelectedItem.ToString();

                    // Add subtype based on selection
                    if (clientType == "Private")
                    {
                        ClientData.AddPrivateClient(clientId);
                    }
                    else if (clientType == "Business")
                    {
                        ClientData.AddBusinessClient(clientId, txtCompanyName.Text.Trim());
                    }

                    MessageBox.Show("Client saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving client");
                MessageBox.Show("Error saving client: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentClientId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                // Update Client
                bool result = ClientData.UpdateClient(
                    currentClientId,
                    txtFName.Text.Trim(),
                    txtLName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtCity.Text.Trim()
                );

                if (result)
                {
                    string clientType = cmbClientType.SelectedItem.ToString();

                    // Update subtype
                    if (clientType == "Business")
                    {
                        ClientData.UpdateBusinessClient(currentClientId, txtCompanyName.Text.Trim());
                    }

                    MessageBox.Show("Client updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating client");
                MessageBox.Show("Error updating client: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentClientId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this client?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = ClientData.DeleteClient(currentClientId);

                    if (deleted)
                    {
                        MessageBox.Show("Client deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadClients();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting client");
                    MessageBox.Show("Cannot delete this client. They may have jobs or contracts.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnViewContracts_Click(object sender, EventArgs e)
        {
            if (currentClientId == 0)
                return;

            DataTable dt = ContractData.GetContractsByClient(currentClientId);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, $"Contracts for {txtCompanyName.Text}");
            reportForm.ShowDialog();
        }

        private void BtnPrivateReport_Click(object sender, EventArgs e)
        {
            DataTable dt = ClientData.GetPrivateClientCountByCity();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Private Clients by City");
            reportForm.ShowDialog();
        }

        private void BtnBusinessReport_Click(object sender, EventArgs e)
        {
            DataTable dt = ClientData.GetBusinessClientsByCity("Cyberjaya");

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Business Clients in Cyberjaya");
            reportForm.ShowDialog();
        }

        private void BtnPrivateNov2025_Click(object sender, EventArgs e)
        {
            DataTable dt = ClientData.GetPrivateClientsByHireDate(new DateTime(2025, 11, 1));

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Private Clients - November 2025");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtCompanyName.Clear();
            cmbClientType.SelectedIndex = -1;
            currentClientId = 0;
            lblClientId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnViewContracts.Enabled = false;

            txtCompanyName.Visible = false;
            var companyLabel = txtCompanyName.Parent.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Text == "Company Name:");

            if (companyLabel != null)
            {
                companyLabel.Visible = false;
            }
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

            if (cmbClientType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select client type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbClientType.Focus();
                return false;
            }

            if (cmbClientType.SelectedItem.ToString() == "Business")
            {
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    MessageBox.Show("Company name is required for business clients.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompanyName.Focus();
                    return false;
                }
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