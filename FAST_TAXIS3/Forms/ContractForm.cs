using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Forms
{
    public partial class ContractForm : Form
    {
        private DataGridView dgvContracts;
        private Panel pnlForm;
        private ComboBox cmbBusinessClient;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private NumericUpDown nudAgreedJobs;
        private TextBox txtFixedFee;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewJobs;
        private Button btnContractSummary;
        private Label lblContractId;
        private int currentContractId = 0;

        public ContractForm()
        {
            InitializeComponent();
            SetupForm();
            LoadBusinessClients();
            LoadContracts();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Contract Management";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1200, 700);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "CONTRACT MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(450, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(450, 450);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Contract Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Contract ID Label (hidden)
            lblContractId = new Label();
            lblContractId.Name = "lblContractId";
            lblContractId.Text = "0";
            lblContractId.Visible = false;
            pnlForm.Controls.Add(lblContractId);

            // Business Client
            Label lblClient = new Label();
            lblClient.Text = "Business Client:";
            lblClient.Font = new Font("Segoe UI", 11);
            lblClient.ForeColor = Color.Black;
            lblClient.Size = new Size(140, 25);
            lblClient.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblClient);

            cmbBusinessClient = new ComboBox();
            cmbBusinessClient.Name = "cmbBusinessClient";
            cmbBusinessClient.Font = new Font("Segoe UI", 11);
            cmbBusinessClient.Size = new Size(260, 25);
            cmbBusinessClient.Location = new Point(160, 70);
            cmbBusinessClient.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbBusinessClient);

            // Start Date
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Font = new Font("Segoe UI", 11);
            lblStartDate.ForeColor = Color.Black;
            lblStartDate.Size = new Size(140, 25);
            lblStartDate.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblStartDate);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Font = new Font("Segoe UI", 11);
            dtpStartDate.Size = new Size(260, 25);
            dtpStartDate.Location = new Point(160, 110);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Value = DateTime.Now;
            pnlForm.Controls.Add(dtpStartDate);

            // End Date
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Font = new Font("Segoe UI", 11);
            lblEndDate.ForeColor = Color.Black;
            lblEndDate.Size = new Size(140, 25);
            lblEndDate.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblEndDate);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Font = new Font("Segoe UI", 11);
            dtpEndDate.Size = new Size(260, 25);
            dtpEndDate.Location = new Point(160, 150);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Value = DateTime.Now.AddYears(1);
            dtpEndDate.Checked = false;
            dtpEndDate.ShowCheckBox = true;
            pnlForm.Controls.Add(dtpEndDate);

            // Agreed Number of Jobs
            Label lblAgreedJobs = new Label();
            lblAgreedJobs.Text = "Agreed Jobs:";
            lblAgreedJobs.Font = new Font("Segoe UI", 11);
            lblAgreedJobs.ForeColor = Color.Black;
            lblAgreedJobs.Size = new Size(140, 25);
            lblAgreedJobs.Location = new Point(20, 190);
            pnlForm.Controls.Add(lblAgreedJobs);

            nudAgreedJobs = new NumericUpDown();
            nudAgreedJobs.Name = "nudAgreedJobs";
            nudAgreedJobs.Font = new Font("Segoe UI", 11);
            nudAgreedJobs.Size = new Size(260, 25);
            nudAgreedJobs.Location = new Point(160, 190);
            nudAgreedJobs.Minimum = 1;
            nudAgreedJobs.Maximum = 9999;
            nudAgreedJobs.Value = 10;
            pnlForm.Controls.Add(nudAgreedJobs);

            // Fixed Fee
            Label lblFixedFee = new Label();
            lblFixedFee.Text = "Fixed Fee (RM):";
            lblFixedFee.Font = new Font("Segoe UI", 11);
            lblFixedFee.ForeColor = Color.Black;
            lblFixedFee.Size = new Size(140, 25);
            lblFixedFee.Location = new Point(20, 230);
            pnlForm.Controls.Add(lblFixedFee);

            txtFixedFee = new TextBox();
            txtFixedFee.Name = "txtFixedFee";
            txtFixedFee.Font = new Font("Segoe UI", 11);
            txtFixedFee.Size = new Size(260, 25);
            txtFixedFee.Location = new Point(160, 230);
            txtFixedFee.Text = "0.00";
            txtFixedFee.TextAlign = HorizontalAlignment.Right;
            pnlForm.Controls.Add(txtFixedFee);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(400, 100);
            pnlButtons.Location = new Point(20, 280);
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

            // View Jobs Button
            btnViewJobs = new Button();
            btnViewJobs.Text = "VIEW JOBS";
            btnViewJobs.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnViewJobs.BackColor = Color.FromArgb(255, 128, 0);
            btnViewJobs.ForeColor = Color.White;
            btnViewJobs.Size = new Size(190, 35);
            btnViewJobs.Location = new Point(0, 45);
            btnViewJobs.FlatStyle = FlatStyle.Flat;
            btnViewJobs.FlatAppearance.BorderSize = 0;
            btnViewJobs.Enabled = false;
            btnViewJobs.Click += BtnViewJobs_Click;
            pnlButtons.Controls.Add(btnViewJobs);

            // Contract Summary Button
            btnContractSummary = new Button();
            btnContractSummary.Text = "CONTRACT SUMMARY";
            btnContractSummary.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnContractSummary.BackColor = Color.FromArgb(40, 167, 69);
            btnContractSummary.ForeColor = Color.White;
            btnContractSummary.Size = new Size(190, 35);
            btnContractSummary.Location = new Point(200, 45);
            btnContractSummary.FlatStyle = FlatStyle.Flat;
            btnContractSummary.FlatAppearance.BorderSize = 0;
            btnContractSummary.Enabled = false;
            btnContractSummary.Click += BtnContractSummary_Click;
            pnlButtons.Controls.Add(btnContractSummary);

            // DataGridView
            dgvContracts = new DataGridView();
            dgvContracts.Name = "dgvContracts";
            dgvContracts.Size = new Size(850, 550);
            dgvContracts.Location = new Point(500, 80);
            dgvContracts.BackgroundColor = Color.White;
            dgvContracts.BorderStyle = BorderStyle.None;
            dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContracts.MultiSelect = false;
            dgvContracts.ReadOnly = true;
            dgvContracts.RowHeadersVisible = false;
            dgvContracts.AllowUserToAddRows = false;
            dgvContracts.CellClick += DgvContracts_CellClick;

            // DataGridView Style
            dgvContracts.EnableHeadersVisualStyles = false;
            dgvContracts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvContracts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvContracts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvContracts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvContracts.ColumnHeadersHeight = 40;

            dgvContracts.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvContracts.DefaultCellStyle.ForeColor = Color.Black;
            dgvContracts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvContracts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvContracts.DefaultCellStyle.Padding = new Padding(5);
            dgvContracts.RowTemplate.Height = 35;

            this.Controls.Add(dgvContracts);

            // Report Buttons Panel
            Panel pnlReports = new Panel();
            pnlReports.Size = new Size(850, 60);
            pnlReports.Location = new Point(500, 640);
            pnlReports.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlReports);

            // Current Contracts KL Button
            Button btnCurrentContractsKL = new Button();
            btnCurrentContractsKL.Text = "CURRENT CONTRACTS - KL";
            btnCurrentContractsKL.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCurrentContractsKL.BackColor = Color.FromArgb(255, 128, 0);
            btnCurrentContractsKL.ForeColor = Color.White;
            btnCurrentContractsKL.Size = new Size(260, 40);
            btnCurrentContractsKL.Location = new Point(20, 10);
            btnCurrentContractsKL.FlatStyle = FlatStyle.Flat;
            btnCurrentContractsKL.FlatAppearance.BorderSize = 0;
            btnCurrentContractsKL.Click += BtnCurrentContractsKL_Click;
            pnlReports.Controls.Add(btnCurrentContractsKL);

            // Expiring Contracts Button
            Button btnExpiringContracts = new Button();
            btnExpiringContracts.Text = "EXPIRING CONTRACTS (30 DAYS)";
            btnExpiringContracts.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnExpiringContracts.BackColor = Color.FromArgb(220, 53, 69);
            btnExpiringContracts.ForeColor = Color.White;
            btnExpiringContracts.Size = new Size(280, 40);
            btnExpiringContracts.Location = new Point(300, 10);
            btnExpiringContracts.FlatStyle = FlatStyle.Flat;
            btnExpiringContracts.FlatAppearance.BorderSize = 0;
            btnExpiringContracts.Click += BtnExpiringContracts_Click;
            pnlReports.Controls.Add(btnExpiringContracts);

            // All Contracts Button
            Button btnAllContracts = new Button();
            btnAllContracts.Text = "ALL CONTRACTS";
            btnAllContracts.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAllContracts.BackColor = Color.FromArgb(0, 123, 255);
            btnAllContracts.ForeColor = Color.White;
            btnAllContracts.Size = new Size(200, 40);
            btnAllContracts.Location = new Point(600, 10);
            btnAllContracts.FlatStyle = FlatStyle.Flat;
            btnAllContracts.FlatAppearance.BorderSize = 0;
            btnAllContracts.Click += BtnAllContracts_Click;
            pnlReports.Controls.Add(btnAllContracts);
        }

        private void LoadBusinessClients()
        {
            DataTable dt = ClientData.GetBusinessClients();
            dt.Columns.Add("DisplayName", typeof(string), "CompanyName + ' - ' + FName + ' ' + LName");
            cmbBusinessClient.DisplayMember = "DisplayName";
            cmbBusinessClient.ValueMember = "ClientID";
            cmbBusinessClient.DataSource = dt;
        }

        private void LoadContracts()
        {
            DataTable dt = ContractData.GetAllContracts();
            dgvContracts.DataSource = dt;
        }

        private void DgvContracts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContracts.Rows[e.RowIndex];

                currentContractId = Convert.ToInt32(row.Cells["ContractID"].Value);
                cmbBusinessClient.SelectedValue = Convert.ToInt32(row.Cells["ClientID"].Value);
                dtpStartDate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);

                if (row.Cells["EndDate"].Value != DBNull.Value)
                {
                    dtpEndDate.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
                    dtpEndDate.Checked = true;
                }
                else
                {
                    dtpEndDate.Checked = false;
                }

                nudAgreedJobs.Value = Convert.ToInt32(row.Cells["AgreedNumJobs"].Value);
                txtFixedFee.Text = Convert.ToDecimal(row.Cells["FixedFee"].Value).ToString("F2");

                lblContractId.Text = currentContractId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnViewJobs.Enabled = true;
                btnContractSummary.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int contractId = ContractData.AddContract(
                    dtpStartDate.Value,
                    dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null,
                    (int)nudAgreedJobs.Value,
                    decimal.Parse(txtFixedFee.Text),
                    Convert.ToInt32(cmbBusinessClient.SelectedValue)
                );

                if (contractId > 0)
                {
                    MessageBox.Show("Contract saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadContracts();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving contract");
                MessageBox.Show("Error saving contract: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                bool result = ContractData.UpdateContract(
                    currentContractId,
                    dtpStartDate.Value,
                    dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null,
                    (int)nudAgreedJobs.Value,
                    decimal.Parse(txtFixedFee.Text),
                    Convert.ToInt32(cmbBusinessClient.SelectedValue)
                );

                if (result)
                {
                    MessageBox.Show("Contract updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadContracts();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating contract");
                MessageBox.Show("Error updating contract: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this contract?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = ContractData.DeleteContract(currentContractId);

                    if (deleted)
                    {
                        MessageBox.Show("Contract deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadContracts();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting contract");
                    MessageBox.Show("Cannot delete this contract. It may have associated jobs.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnViewJobs_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
                return;

            DataTable dt = JobData.GetJobsByContract(currentContractId);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, $"Jobs for Contract #{currentContractId}");
            reportForm.ShowDialog();
        }

        private void BtnContractSummary_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
                return;

            DataTable dt = ContractData.GetContractJobSummary(currentContractId);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, $"Contract Summary #{currentContractId}");
            reportForm.ShowDialog();
        }

        private void BtnCurrentContractsKL_Click(object sender, EventArgs e)
        {
            DataTable dt = ContractData.GetContractsByCity("Kuala Lumpur");

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Current Contracts - Kuala Lumpur");
            reportForm.ShowDialog();
        }

        private void BtnExpiringContracts_Click(object sender, EventArgs e)
        {
            DataTable dt = ContractData.GetExpiringContracts(30);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Contracts Expiring in 30 Days");
            reportForm.ShowDialog();
        }

        private void BtnAllContracts_Click(object sender, EventArgs e)
        {
            DataTable dt = ContractData.GetAllContracts();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "All Contracts");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            if (cmbBusinessClient.Items.Count > 0)
                cmbBusinessClient.SelectedIndex = -1;

            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddYears(1);
            dtpEndDate.Checked = false;
            nudAgreedJobs.Value = 10;
            txtFixedFee.Text = "0.00";
            currentContractId = 0;
            lblContractId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnViewJobs.Enabled = false;
            btnContractSummary.Enabled = false;

            cmbBusinessClient.Focus();
        }

        private bool ValidateInput()
        {
            if (cmbBusinessClient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a business client.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBusinessClient.Focus();
                return false;
            }

            if (dtpEndDate.Checked && dtpEndDate.Value <= dtpStartDate.Value)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Focus();
                return false;
            }

            if (nudAgreedJobs.Value <= 0)
            {
                MessageBox.Show("Agreed number of jobs must be greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudAgreedJobs.Focus();
                return false;
            }

            if (!decimal.TryParse(txtFixedFee.Text, out decimal fee) || fee <= 0)
            {
                MessageBox.Show("Please enter a valid fixed fee amount.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFixedFee.Focus();
                return false;
            }

            if (ContractData.IsClientHasActiveContract(Convert.ToInt32(cmbBusinessClient.SelectedValue)) && currentContractId == 0)
            {
                DialogResult result = MessageBox.Show("This client already has an active contract. Do you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return false;
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