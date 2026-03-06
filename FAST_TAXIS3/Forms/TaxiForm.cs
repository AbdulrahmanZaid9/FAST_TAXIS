using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class TaxiForm : Form
    {
        private DataGridView dgvTaxis;
        private Panel pnlForm;
        private TextBox txtPlateNo;
        private TextBox txtManufacturer;
        private TextBox txtModel;
        private NumericUpDown nudYear;
        private DateTimePicker dtpServiceDate;
        private ComboBox cmbOffice;
        private ComboBox cmbOwner;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnViewDrivers;
        private Button btnDueMaintenance;
        private Label lblTaxiId;
        private int currentTaxiId = 0;

        public TaxiForm()
        {
            InitializeComponent();
            SetupForm();
            LoadOffices();
            LoadOwners();
            LoadTaxis();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Taxi Management";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1200, 700);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "TAXI MANAGEMENT";
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
            lblFormTitle.Text = "Taxi Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Taxi ID Label (hidden)
            lblTaxiId = new Label();
            lblTaxiId.Name = "lblTaxiId";
            lblTaxiId.Text = "0";
            lblTaxiId.Visible = false;
            pnlForm.Controls.Add(lblTaxiId);

            // Plate Number
            Label lblPlateNo = new Label();
            lblPlateNo.Text = "Plate No:";
            lblPlateNo.Font = new Font("Segoe UI", 11);
            lblPlateNo.ForeColor = Color.Black;
            lblPlateNo.Size = new Size(120, 25);
            lblPlateNo.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblPlateNo);

            txtPlateNo = new TextBox();
            txtPlateNo.Name = "txtPlateNo";
            txtPlateNo.Font = new Font("Segoe UI", 11);
            txtPlateNo.Size = new Size(280, 25);
            txtPlateNo.Location = new Point(140, 70);
            txtPlateNo.CharacterCasing = CharacterCasing.Upper;
            pnlForm.Controls.Add(txtPlateNo);

            // Manufacturer
            Label lblManufacturer = new Label();
            lblManufacturer.Text = "Manufacturer:";
            lblManufacturer.Font = new Font("Segoe UI", 11);
            lblManufacturer.ForeColor = Color.Black;
            lblManufacturer.Size = new Size(120, 25);
            lblManufacturer.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblManufacturer);

            txtManufacturer = new TextBox();
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Font = new Font("Segoe UI", 11);
            txtManufacturer.Size = new Size(280, 25);
            txtManufacturer.Location = new Point(140, 110);
            pnlForm.Controls.Add(txtManufacturer);

            // Model
            Label lblModel = new Label();
            lblModel.Text = "Model:";
            lblModel.Font = new Font("Segoe UI", 11);
            lblModel.ForeColor = Color.Black;
            lblModel.Size = new Size(120, 25);
            lblModel.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblModel);

            txtModel = new TextBox();
            txtModel.Name = "txtModel";
            txtModel.Font = new Font("Segoe UI", 11);
            txtModel.Size = new Size(280, 25);
            txtModel.Location = new Point(140, 150);
            pnlForm.Controls.Add(txtModel);

            // Year
            Label lblYear = new Label();
            lblYear.Text = "Year:";
            lblYear.Font = new Font("Segoe UI", 11);
            lblYear.ForeColor = Color.Black;
            lblYear.Size = new Size(120, 25);
            lblYear.Location = new Point(20, 190);
            pnlForm.Controls.Add(lblYear);

            nudYear = new NumericUpDown();
            nudYear.Name = "nudYear";
            nudYear.Font = new Font("Segoe UI", 11);
            nudYear.Size = new Size(280, 25);
            nudYear.Location = new Point(140, 190);
            nudYear.Minimum = 1990;
            nudYear.Maximum = DateTime.Now.Year + 1;
            nudYear.Value = DateTime.Now.Year;
            pnlForm.Controls.Add(nudYear);

            // Next Service Date
            Label lblServiceDate = new Label();
            lblServiceDate.Text = "Next Service:";
            lblServiceDate.Font = new Font("Segoe UI", 11);
            lblServiceDate.ForeColor = Color.Black;
            lblServiceDate.Size = new Size(120, 25);
            lblServiceDate.Location = new Point(20, 230);
            pnlForm.Controls.Add(lblServiceDate);

            dtpServiceDate = new DateTimePicker();
            dtpServiceDate.Name = "dtpServiceDate";
            dtpServiceDate.Font = new Font("Segoe UI", 11);
            dtpServiceDate.Size = new Size(280, 25);
            dtpServiceDate.Location = new Point(140, 230);
            dtpServiceDate.Format = DateTimePickerFormat.Short;
            dtpServiceDate.Value = DateTime.Now.AddMonths(3);
            pnlForm.Controls.Add(dtpServiceDate);

            // Office
            Label lblOffice = new Label();
            lblOffice.Text = "Office:";
            lblOffice.Font = new Font("Segoe UI", 11);
            lblOffice.ForeColor = Color.Black;
            lblOffice.Size = new Size(120, 25);
            lblOffice.Location = new Point(20, 270);
            pnlForm.Controls.Add(lblOffice);

            cmbOffice = new ComboBox();
            cmbOffice.Name = "cmbOffice";
            cmbOffice.Font = new Font("Segoe UI", 11);
            cmbOffice.Size = new Size(280, 25);
            cmbOffice.Location = new Point(140, 270);
            cmbOffice.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbOffice);

            // Owner
            Label lblOwner = new Label();
            lblOwner.Text = "Owner:";
            lblOwner.Font = new Font("Segoe UI", 11);
            lblOwner.ForeColor = Color.Black;
            lblOwner.Size = new Size(120, 25);
            lblOwner.Location = new Point(20, 310);
            pnlForm.Controls.Add(lblOwner);

            cmbOwner = new ComboBox();
            cmbOwner.Name = "cmbOwner";
            cmbOwner.Font = new Font("Segoe UI", 11);
            cmbOwner.Size = new Size(280, 25);
            cmbOwner.Location = new Point(140, 310);
            cmbOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbOwner);

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

            // View Drivers Button
            btnViewDrivers = new Button();
            btnViewDrivers.Text = "VIEW DRIVERS";
            btnViewDrivers.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnViewDrivers.BackColor = Color.FromArgb(255, 128, 0);
            btnViewDrivers.ForeColor = Color.White;
            btnViewDrivers.Size = new Size(190, 35);
            btnViewDrivers.Location = new Point(0, 45);
            btnViewDrivers.FlatStyle = FlatStyle.Flat;
            btnViewDrivers.FlatAppearance.BorderSize = 0;
            btnViewDrivers.Enabled = false;
            btnViewDrivers.Click += BtnViewDrivers_Click;
            pnlButtons.Controls.Add(btnViewDrivers);

            // Due Maintenance Button
            btnDueMaintenance = new Button();
            btnDueMaintenance.Text = "DUE MAINTENANCE";
            btnDueMaintenance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDueMaintenance.BackColor = Color.FromArgb(220, 53, 69);
            btnDueMaintenance.ForeColor = Color.White;
            btnDueMaintenance.Size = new Size(190, 35);
            btnDueMaintenance.Location = new Point(200, 45);
            btnDueMaintenance.FlatStyle = FlatStyle.Flat;
            btnDueMaintenance.FlatAppearance.BorderSize = 0;
            btnDueMaintenance.Click += BtnDueMaintenance_Click;
            pnlButtons.Controls.Add(btnDueMaintenance);

            // DataGridView
            dgvTaxis = new DataGridView();
            dgvTaxis.Name = "dgvTaxis";
            dgvTaxis.Size = new Size(850, 600);
            dgvTaxis.Location = new Point(500, 80);
            dgvTaxis.BackgroundColor = Color.White;
            dgvTaxis.BorderStyle = BorderStyle.None;
            dgvTaxis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaxis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaxis.MultiSelect = false;
            dgvTaxis.ReadOnly = true;
            dgvTaxis.RowHeadersVisible = false;
            dgvTaxis.AllowUserToAddRows = false;
            dgvTaxis.CellClick += DgvTaxis_CellClick;

            // DataGridView Style
            dgvTaxis.EnableHeadersVisualStyles = false;
            dgvTaxis.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvTaxis.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTaxis.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTaxis.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaxis.ColumnHeadersHeight = 40;

            dgvTaxis.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvTaxis.DefaultCellStyle.ForeColor = Color.Black;
            dgvTaxis.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvTaxis.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTaxis.DefaultCellStyle.Padding = new Padding(5);
            dgvTaxis.RowTemplate.Height = 35;

            this.Controls.Add(dgvTaxis);

            // Summary Panel
            Panel pnlSummary = new Panel();
            pnlSummary.Size = new Size(850, 60);
            pnlSummary.Location = new Point(500, 690);
            pnlSummary.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlSummary);

            Label lblTotalTaxis = new Label();
            lblTotalTaxis.Name = "lblTotalTaxis";
            lblTotalTaxis.Text = "Total Taxis: 0";
            lblTotalTaxis.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalTaxis.ForeColor = Color.White;
            lblTotalTaxis.Size = new Size(200, 30);
            lblTotalTaxis.Location = new Point(20, 15);
            pnlSummary.Controls.Add(lblTotalTaxis);

            UpdateSummary();
        }

        private void LoadOffices()
        {
            DataTable dt = OfficeData.GetAllOffices();
            cmbOffice.DisplayMember = "OfficeName";
            cmbOffice.ValueMember = "OfficeID";
            cmbOffice.DataSource = dt;
        }

        private void LoadOwners()
        {
            DataTable dt = OwnerData.GetAllOwners();
            dt.Columns.Add("FullName", typeof(string), "FName + ' ' + LName");
            cmbOwner.DisplayMember = "FullName";
            cmbOwner.ValueMember = "OwnerID";
            cmbOwner.DataSource = dt;
        }

        private void LoadTaxis()
        {
            DataTable dt;

            if (SessionManager.CurrentStaffRole == "Manager")
            {
                dt = TaxiData.GetTaxisByOffice(SessionManager.CurrentOfficeID);
            }
            else
            {
                dt = TaxiData.GetAllTaxis();
            }

            dgvTaxis.DataSource = dt;
        }

        private void UpdateSummary()
        {
            int totalTaxis = TaxiData.GetTotalTaxis();
            Label lblTotalTaxis = this.Controls.Find("lblTotalTaxis", true).FirstOrDefault() as Label;
            if (lblTotalTaxis != null)
            {
                lblTotalTaxis.Text = $"Total Taxis: {totalTaxis}";
            }
        }

        private void DgvTaxis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaxis.Rows[e.RowIndex];

                currentTaxiId = Convert.ToInt32(row.Cells["TaxiID"].Value);
                txtPlateNo.Text = row.Cells["PlateNo"].Value.ToString();
                txtManufacturer.Text = row.Cells["Manufacturer"].Value?.ToString();
                txtModel.Text = row.Cells["Model"].Value?.ToString();

                if (row.Cells["Year"].Value != DBNull.Value)
                    nudYear.Value = Convert.ToInt32(row.Cells["Year"].Value);

                if (row.Cells["NextServiceDate"].Value != DBNull.Value)
                    dtpServiceDate.Value = Convert.ToDateTime(row.Cells["NextServiceDate"].Value);

                cmbOffice.SelectedValue = Convert.ToInt32(row.Cells["OfficeID"].Value);
                cmbOwner.SelectedValue = Convert.ToInt32(row.Cells["OwnerID"].Value);

                lblTaxiId.Text = currentTaxiId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnViewDrivers.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int taxiId = TaxiData.AddTaxi(
                    txtPlateNo.Text.Trim().ToUpper(),
                    txtManufacturer.Text.Trim(),
                    txtModel.Text.Trim(),
                    (int)nudYear.Value,
                    dtpServiceDate.Value,
                    Convert.ToInt32(cmbOffice.SelectedValue),
                    Convert.ToInt32(cmbOwner.SelectedValue)
                );

                if (taxiId > 0)
                {
                    MessageBox.Show("Taxi saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadTaxis();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving taxi");
                MessageBox.Show("Error saving taxi: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentTaxiId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                bool result = TaxiData.UpdateTaxi(
                    currentTaxiId,
                    txtPlateNo.Text.Trim().ToUpper(),
                    txtManufacturer.Text.Trim(),
                    txtModel.Text.Trim(),
                    (int)nudYear.Value,
                    dtpServiceDate.Value,
                    Convert.ToInt32(cmbOffice.SelectedValue),
                    Convert.ToInt32(cmbOwner.SelectedValue)
                );

                if (result)
                {
                    MessageBox.Show("Taxi updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadTaxis();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating taxi");
                MessageBox.Show("Error updating taxi: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentTaxiId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this taxi?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = TaxiData.DeleteTaxi(currentTaxiId);

                    if (deleted)
                    {
                        MessageBox.Show("Taxi deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadTaxis();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting taxi");
                    MessageBox.Show("Cannot delete this taxi. It may be referenced by other records.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnViewDrivers_Click(object sender, EventArgs e)
        {
            if (currentTaxiId == 0)
                return;

            DataTable dt = AllocationData.GetAllocationsByTaxi(currentTaxiId);

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, $"Drivers for Taxi {txtPlateNo.Text}");
            reportForm.ShowDialog();
        }

        private void BtnDueMaintenance_Click(object sender, EventArgs e)
        {
            DataTable dt = TaxiData.GetTaxisDueForMaintenance(DateTime.Parse("2026-01-01"));

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Taxis Due for Maintenance - January 1, 2026");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            txtPlateNo.Clear();
            txtManufacturer.Clear();
            txtModel.Clear();
            nudYear.Value = DateTime.Now.Year;
            dtpServiceDate.Value = DateTime.Now.AddMonths(3);

            if (cmbOffice.Items.Count > 0)
                cmbOffice.SelectedIndex = 0;

            if (cmbOwner.Items.Count > 0)
                cmbOwner.SelectedIndex = 0;

            currentTaxiId = 0;
            lblTaxiId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnViewDrivers.Enabled = false;

            txtPlateNo.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPlateNo.Text))
            {
                MessageBox.Show("Plate number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNo.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidPlateNumber(txtPlateNo.Text))
            {
                MessageBox.Show("Invalid plate number format.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNo.Focus();
                return false;
            }

            if (cmbOffice.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an office.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOffice.Focus();
                return false;
            }

            if (cmbOwner.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an owner.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOwner.Focus();
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