using FAST_TAXIS3.Data;
using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FAST_TAXIS3.Forms
{
    public partial class JobForm : Form
    {
        private DataGridView dgvJobs;
        private Panel pnlForm;
        private ComboBox cmbClient;
        private ComboBox cmbDriver;
        private ComboBox cmbTaxi;
        private ComboBox cmbContract;
        private DateTimePicker dtpPickupDateTime;
        private TextBox txtPickupAddress;
        private TextBox txtDropoffAddress;
        private ComboBox cmbStatus;
        private TextBox txtMileage;
        private TextBox txtCharge;
        private TextBox txtFailReason;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnComplete;
        private Button btnFail;
        private Label lblJobId;
        private int currentJobId = 0;

        public JobForm()
        {
            InitializeComponent();
            SetupForm();
            LoadClients();
            LoadDrivers();
            LoadTaxis();
            LoadContracts();
            LoadJobs();
        }

        private void SetupForm()
        {
            this.Text = "Fast Taxis - Job Management";
            this.Size = new Size(1500, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.MinimumSize = new Size(1400, 800);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "JOB MANAGEMENT";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 55);
            lblTitle.Size = new Size(350, 50);
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            // Form Panel
            pnlForm = new Panel();
            pnlForm.Size = new Size(550, 700);
            pnlForm.Location = new Point(30, 80);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.None;
            this.Controls.Add(pnlForm);

            // Form Title
            Label lblFormTitle = new Label();
            lblFormTitle.Text = "Job Details";
            lblFormTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(255, 128, 0);
            lblFormTitle.Size = new Size(200, 30);
            lblFormTitle.Location = new Point(20, 20);
            pnlForm.Controls.Add(lblFormTitle);

            // Job ID Label (hidden)
            lblJobId = new Label();
            lblJobId.Name = "lblJobId";
            lblJobId.Text = "0";
            lblJobId.Visible = false;
            pnlForm.Controls.Add(lblJobId);

            // Client
            Label lblClient = new Label();
            lblClient.Text = "Client:";
            lblClient.Font = new Font("Segoe UI", 11);
            lblClient.ForeColor = Color.Black;
            lblClient.Size = new Size(120, 25);
            lblClient.Location = new Point(20, 70);
            pnlForm.Controls.Add(lblClient);

            cmbClient = new ComboBox();
            cmbClient.Name = "cmbClient";
            cmbClient.Font = new Font("Segoe UI", 11);
            cmbClient.Size = new Size(380, 25);
            cmbClient.Location = new Point(140, 70);
            cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClient.SelectedIndexChanged += CmbClient_SelectedIndexChanged;
            pnlForm.Controls.Add(cmbClient);

            // Driver
            Label lblDriver = new Label();
            lblDriver.Text = "Driver:";
            lblDriver.Font = new Font("Segoe UI", 11);
            lblDriver.ForeColor = Color.Black;
            lblDriver.Size = new Size(120, 25);
            lblDriver.Location = new Point(20, 110);
            pnlForm.Controls.Add(lblDriver);

            cmbDriver = new ComboBox();
            cmbDriver.Name = "cmbDriver";
            cmbDriver.Font = new Font("Segoe UI", 11);
            cmbDriver.Size = new Size(380, 25);
            cmbDriver.Location = new Point(140, 110);
            cmbDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbDriver);

            // Taxi
            Label lblTaxi = new Label();
            lblTaxi.Text = "Taxi:";
            lblTaxi.Font = new Font("Segoe UI", 11);
            lblTaxi.ForeColor = Color.Black;
            lblTaxi.Size = new Size(120, 25);
            lblTaxi.Location = new Point(20, 150);
            pnlForm.Controls.Add(lblTaxi);

            cmbTaxi = new ComboBox();
            cmbTaxi.Name = "cmbTaxi";
            cmbTaxi.Font = new Font("Segoe UI", 11);
            cmbTaxi.Size = new Size(380, 25);
            cmbTaxi.Location = new Point(140, 150);
            cmbTaxi.DropDownStyle = ComboBoxStyle.DropDownList;
            pnlForm.Controls.Add(cmbTaxi);

            // Contract (Optional)
            Label lblContract = new Label();
            lblContract.Text = "Contract:";
            lblContract.Font = new Font("Segoe UI", 11);
            lblContract.ForeColor = Color.Black;
            lblContract.Size = new Size(120, 25);
            lblContract.Location = new Point(20, 190);
            pnlForm.Controls.Add(lblContract);

            cmbContract = new ComboBox();
            cmbContract.Name = "cmbContract";
            cmbContract.Font = new Font("Segoe UI", 11);
            cmbContract.Size = new Size(380, 25);
            cmbContract.Location = new Point(140, 190);
            cmbContract.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContract.Items.Add(new { Text = "-- No Contract --", Value = 0 });
            pnlForm.Controls.Add(cmbContract);

            // Pickup Date/Time
            Label lblPickupDateTime = new Label();
            lblPickupDateTime.Text = "Pickup Date/Time:";
            lblPickupDateTime.Font = new Font("Segoe UI", 11);
            lblPickupDateTime.ForeColor = Color.Black;
            lblPickupDateTime.Size = new Size(120, 25);
            lblPickupDateTime.Location = new Point(20, 230);
            pnlForm.Controls.Add(lblPickupDateTime);

            dtpPickupDateTime = new DateTimePicker();
            dtpPickupDateTime.Name = "dtpPickupDateTime";
            dtpPickupDateTime.Font = new Font("Segoe UI", 11);
            dtpPickupDateTime.Size = new Size(380, 25);
            dtpPickupDateTime.Location = new Point(140, 230);
            dtpPickupDateTime.Format = DateTimePickerFormat.Custom;
            dtpPickupDateTime.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpPickupDateTime.ShowUpDown = true;
            dtpPickupDateTime.Value = DateTime.Now;
            pnlForm.Controls.Add(dtpPickupDateTime);

            // Pickup Address
            Label lblPickupAddress = new Label();
            lblPickupAddress.Text = "Pickup Address:";
            lblPickupAddress.Font = new Font("Segoe UI", 11);
            lblPickupAddress.ForeColor = Color.Black;
            lblPickupAddress.Size = new Size(120, 25);
            lblPickupAddress.Location = new Point(20, 270);
            pnlForm.Controls.Add(lblPickupAddress);

            txtPickupAddress = new TextBox();
            txtPickupAddress.Name = "txtPickupAddress";
            txtPickupAddress.Font = new Font("Segoe UI", 11);
            txtPickupAddress.Size = new Size(380, 25);
            txtPickupAddress.Location = new Point(140, 270);
            pnlForm.Controls.Add(txtPickupAddress);

            // Dropoff Address
            Label lblDropoffAddress = new Label();
            lblDropoffAddress.Text = "Dropoff Address:";
            lblDropoffAddress.Font = new Font("Segoe UI", 11);
            lblDropoffAddress.ForeColor = Color.Black;
            lblDropoffAddress.Size = new Size(120, 25);
            lblDropoffAddress.Location = new Point(20, 310);
            pnlForm.Controls.Add(lblDropoffAddress);

            txtDropoffAddress = new TextBox();
            txtDropoffAddress.Name = "txtDropoffAddress";
            txtDropoffAddress.Font = new Font("Segoe UI", 11);
            txtDropoffAddress.Size = new Size(380, 25);
            txtDropoffAddress.Location = new Point(140, 310);
            pnlForm.Controls.Add(txtDropoffAddress);

            // Status
            Label lblStatus = new Label();
            lblStatus.Text = "Status:";
            lblStatus.Font = new Font("Segoe UI", 11);
            lblStatus.ForeColor = Color.Black;
            lblStatus.Size = new Size(120, 25);
            lblStatus.Location = new Point(20, 350);
            pnlForm.Controls.Add(lblStatus);

            cmbStatus = new ComboBox();
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Font = new Font("Segoe UI", 11);
            cmbStatus.Size = new Size(380, 25);
            cmbStatus.Location = new Point(140, 350);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new string[] { "Pending", "Completed", "Failed" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += CmbStatus_SelectedIndexChanged;
            pnlForm.Controls.Add(cmbStatus);

            // Mileage
            Label lblMileage = new Label();
            lblMileage.Text = "Mileage (km):";
            lblMileage.Font = new Font("Segoe UI", 11);
            lblMileage.ForeColor = Color.Black;
            lblMileage.Size = new Size(120, 25);
            lblMileage.Location = new Point(20, 390);
            pnlForm.Controls.Add(lblMileage);

            txtMileage = new TextBox();
            txtMileage.Name = "txtMileage";
            txtMileage.Font = new Font("Segoe UI", 11);
            txtMileage.Size = new Size(380, 25);
            txtMileage.Location = new Point(140, 390);
            txtMileage.Text = "0.00";
            txtMileage.TextAlign = HorizontalAlignment.Right;
            pnlForm.Controls.Add(txtMileage);

            // Charge Amount
            Label lblCharge = new Label();
            lblCharge.Text = "Charge (RM):";
            lblCharge.Font = new Font("Segoe UI", 11);
            lblCharge.ForeColor = Color.Black;
            lblCharge.Size = new Size(120, 25);
            lblCharge.Location = new Point(20, 430);
            pnlForm.Controls.Add(lblCharge);

            txtCharge = new TextBox();
            txtCharge.Name = "txtCharge";
            txtCharge.Font = new Font("Segoe UI", 11);
            txtCharge.Size = new Size(380, 25);
            txtCharge.Location = new Point(140, 430);
            txtCharge.Text = "0.00";
            txtCharge.TextAlign = HorizontalAlignment.Right;
            pnlForm.Controls.Add(txtCharge);

            // Fail Reason
            Label lblFailReason = new Label();
            lblFailReason.Text = "Fail Reason:";
            lblFailReason.Font = new Font("Segoe UI", 11);
            lblFailReason.ForeColor = Color.Black;
            lblFailReason.Size = new Size(120, 25);
            lblFailReason.Location = new Point(20, 470);
            lblFailReason.Visible = false;
            pnlForm.Controls.Add(lblFailReason);

            txtFailReason = new TextBox();
            txtFailReason.Name = "txtFailReason";
            txtFailReason.Font = new Font("Segoe UI", 11);
            txtFailReason.Size = new Size(380, 25);
            txtFailReason.Location = new Point(140, 470);
            txtFailReason.Visible = false;
            pnlForm.Controls.Add(txtFailReason);

            // Buttons Panel
            Panel pnlButtons = new Panel();
            pnlButtons.Size = new Size(500, 100);
            pnlButtons.Location = new Point(20, 520);
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

            // Complete Button
            btnComplete = new Button();
            btnComplete.Text = "COMPLETE";
            btnComplete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnComplete.BackColor = Color.FromArgb(40, 167, 69);
            btnComplete.ForeColor = Color.White;
            btnComplete.Size = new Size(120, 35);
            btnComplete.Location = new Point(0, 45);
            btnComplete.FlatStyle = FlatStyle.Flat;
            btnComplete.FlatAppearance.BorderSize = 0;
            btnComplete.Enabled = false;
            btnComplete.Click += BtnComplete_Click;
            pnlButtons.Controls.Add(btnComplete);

            // Fail Button
            btnFail = new Button();
            btnFail.Text = "FAIL";
            btnFail.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnFail.BackColor = Color.FromArgb(220, 53, 69);
            btnFail.ForeColor = Color.White;
            btnFail.Size = new Size(120, 35);
            btnFail.Location = new Point(130, 45);
            btnFail.FlatStyle = FlatStyle.Flat;
            btnFail.FlatAppearance.BorderSize = 0;
            btnFail.Enabled = false;
            btnFail.Click += BtnFail_Click;
            pnlButtons.Controls.Add(btnFail);

            // DataGridView
            dgvJobs = new DataGridView();
            dgvJobs.Name = "dgvJobs";
            dgvJobs.Size = new Size(850, 650);
            dgvJobs.Location = new Point(600, 80);
            dgvJobs.BackgroundColor = Color.White;
            dgvJobs.BorderStyle = BorderStyle.None;
            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobs.MultiSelect = false;
            dgvJobs.ReadOnly = true;
            dgvJobs.RowHeadersVisible = false;
            dgvJobs.AllowUserToAddRows = false;
            dgvJobs.CellClick += DgvJobs_CellClick;

            // DataGridView Style
            dgvJobs.EnableHeadersVisualStyles = false;
            dgvJobs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvJobs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvJobs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvJobs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvJobs.ColumnHeadersHeight = 40;

            dgvJobs.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvJobs.DefaultCellStyle.ForeColor = Color.Black;
            dgvJobs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgvJobs.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvJobs.DefaultCellStyle.Padding = new Padding(5);
            dgvJobs.RowTemplate.Height = 35;

            this.Controls.Add(dgvJobs);

            // Report Buttons Panel
            Panel pnlReports = new Panel();
            pnlReports.Size = new Size(850, 80);
            pnlReports.Location = new Point(600, 740);
            pnlReports.BackColor = Color.FromArgb(52, 58, 64);
            this.Controls.Add(pnlReports);

            // Average Fee Button
            Button btnAverageFee = new Button();
            btnAverageFee.Text = "AVERAGE FEE - PRIVATE";
            btnAverageFee.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAverageFee.BackColor = Color.FromArgb(0, 123, 255);
            btnAverageFee.ForeColor = Color.White;
            btnAverageFee.Size = new Size(200, 40);
            btnAverageFee.Location = new Point(20, 20);
            btnAverageFee.FlatStyle = FlatStyle.Flat;
            btnAverageFee.FlatAppearance.BorderSize = 0;
            btnAverageFee.Click += BtnAverageFee_Click;
            pnlReports.Controls.Add(btnAverageFee);

            // Jobs Per Car Button
            Button btnJobsPerCar = new Button();
            btnJobsPerCar.Text = "JOBS PER CAR";
            btnJobsPerCar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnJobsPerCar.BackColor = Color.FromArgb(255, 128, 0);
            btnJobsPerCar.ForeColor = Color.White;
            btnJobsPerCar.Size = new Size(150, 40);
            btnJobsPerCar.Location = new Point(240, 20);
            btnJobsPerCar.FlatStyle = FlatStyle.Flat;
            btnJobsPerCar.FlatAppearance.BorderSize = 0;
            btnJobsPerCar.Click += BtnJobsPerCar_Click;
            pnlReports.Controls.Add(btnJobsPerCar);

            // Jobs Per Driver Button
            Button btnJobsPerDriver = new Button();
            btnJobsPerDriver.Text = "JOBS PER DRIVER";
            btnJobsPerDriver.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnJobsPerDriver.BackColor = Color.FromArgb(40, 167, 69);
            btnJobsPerDriver.ForeColor = Color.White;
            btnJobsPerDriver.Size = new Size(160, 40);
            btnJobsPerDriver.Location = new Point(410, 20);
            btnJobsPerDriver.FlatStyle = FlatStyle.Flat;
            btnJobsPerDriver.FlatAppearance.BorderSize = 0;
            btnJobsPerDriver.Click += BtnJobsPerDriver_Click;
            pnlReports.Controls.Add(btnJobsPerDriver);

            // Revenue Nov 2025 Button
            Button btnRevenueNov2025 = new Button();
            btnRevenueNov2025.Text = "REVENUE NOV 2025";
            btnRevenueNov2025.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRevenueNov2025.BackColor = Color.FromArgb(220, 53, 69);
            btnRevenueNov2025.ForeColor = Color.White;
            btnRevenueNov2025.Size = new Size(180, 40);
            btnRevenueNov2025.Location = new Point(590, 20);
            btnRevenueNov2025.FlatStyle = FlatStyle.Flat;
            btnRevenueNov2025.FlatAppearance.BorderSize = 0;
            btnRevenueNov2025.Click += BtnRevenueNov2025_Click;
            pnlReports.Controls.Add(btnRevenueNov2025);
        }

        private void LoadClients()
        {
            DataTable dt = ClientData.GetAllClients();
            dt.Columns.Add("DisplayName", typeof(string), "FName + ' ' + LName + ' (' + ClientType + ')'");
            cmbClient.DisplayMember = "DisplayName";
            cmbClient.ValueMember = "ClientID";
            cmbClient.DataSource = dt;
        }

        private void LoadDrivers()
        {
            DataTable dt = StaffData.GetAllDrivers();
            dt.Columns.Add("FullName", typeof(string), "FName + ' ' + LName");
            cmbDriver.DisplayMember = "FullName";
            cmbDriver.ValueMember = "StaffID";
            cmbDriver.DataSource = dt;
        }

        private void LoadTaxis()
        {
            DataTable dt = TaxiData.GetAllTaxis();
            dt.Columns.Add("DisplayName", typeof(string), "PlateNo + ' - ' + Model");
            cmbTaxi.DisplayMember = "DisplayName";
            cmbTaxi.ValueMember = "TaxiID";
            cmbTaxi.DataSource = dt;
        }
        private void LoadContracts()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ContractID", typeof(int));
                dt.Columns.Add("DisplayName", typeof(string));

                DataRow emptyRow = dt.NewRow();
                emptyRow["ContractID"] = 0;
                emptyRow["DisplayName"] = "-- No Contract --";
                dt.Rows.Add(emptyRow);

                DataTable contracts = ContractData.GetCurrentContracts();

                if (contracts != null && contracts.Rows.Count > 0)
                {
                    foreach (DataRow r in contracts.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["ContractID"] = r["ContractID"];

                        string name = "Unknown";
                        if (contracts.Columns.Contains("CompanyName") && r["CompanyName"] != DBNull.Value)
                            name = r["CompanyName"].ToString();
                        else if (contracts.Columns.Contains("ClientName") && r["ClientName"] != DBNull.Value)
                            name = r["ClientName"].ToString();

                        row["DisplayName"] = $"#{r["ContractID"]} - {name}";
                        dt.Rows.Add(row);
                    }
                }

                cmbContract.DisplayMember = "DisplayName";
                cmbContract.ValueMember = "ContractID";
                cmbContract.DataSource = dt;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "LoadContracts Error");
            }
        }
        private void LoadJobs()
        {
            DataTable dt;

            if (SessionManager.CurrentStaffRole == "Driver")
            {
                dt = JobData.GetJobsByDriver(SessionManager.CurrentStaffID, null);
            }
            else if (SessionManager.CurrentStaffRole == "Manager")
            {
                // ⚠️ محتاج دالة تجيب رحلات كل سائقي المكتب
                dt = JobData.GetJobsByOffice(SessionManager.CurrentOfficeID);
            }
            else
            {
                dt = JobData.GetAllJobs();
            }

            dgvJobs.DataSource = dt;
        }

        private void CmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbClient.SelectedValue != null && cmbClient.SelectedValue is int)
                {
                    int clientId = (int)cmbClient.SelectedValue;
                    string clientType = ClientData.GetClientType(clientId);

                    // دائمًا نبدأ بجدول جديد فيه "-- No Contract --"
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ContractID", typeof(int));
                    dt.Columns.Add("DisplayName", typeof(string));

                    DataRow emptyRow = dt.NewRow();
                    emptyRow["ContractID"] = 0;
                    emptyRow["DisplayName"] = "-- No Contract --";
                    dt.Rows.Add(emptyRow);

                    // إذا كان Business Client، نحاول نجيب العقود
                    if (clientType == "Business")
                    {
                        DataTable contracts = ContractData.GetContractsByClient(clientId);

                        // إذا في عقود، نضيفها تحت "-- No Contract --"
                        if (contracts != null && contracts.Rows.Count > 0)
                        {
                            foreach (DataRow r in contracts.Rows)
                            {
                                DataRow row = dt.NewRow();
                                row["ContractID"] = r["ContractID"];

                                // نحدد الاسم المعروض
                                string name = "Unknown";
                                if (contracts.Columns.Contains("CompanyName") && r["CompanyName"] != DBNull.Value)
                                    name = r["CompanyName"].ToString();
                                else if (contracts.Columns.Contains("ClientName") && r["ClientName"] != DBNull.Value)
                                    name = r["ClientName"].ToString();

                                row["DisplayName"] = $"#{r["ContractID"]} - {name}";
                                dt.Rows.Add(row);
                            }
                        }
                    }

                    // ربط الكومبو
                    cmbContract.DisplayMember = "DisplayName";
                    cmbContract.ValueMember = "ContractID";
                    cmbContract.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading contracts: " + ex.Message);
            }
        }

        private void CmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = cmbStatus.SelectedItem?.ToString();

            if (status == "Completed")
            {
                txtMileage.Enabled = true;
                txtCharge.Enabled = true;
                txtFailReason.Visible = false;
                var failLabel = txtFailReason.Parent.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == "Fail Reason:");

                if (failLabel != null)
                {
                    failLabel.Visible = false;
                }
            }
            else if (status == "Failed")
            {
                txtMileage.Enabled = false;
                txtCharge.Enabled = false;
                txtMileage.Text = "0.00";
                txtCharge.Text = "0.00";
                txtFailReason.Visible = true;
                var failLabel = txtFailReason.Parent.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == "Fail Reason:");

                if (failLabel != null)
                {
                    failLabel.Visible = true;
                }
            }
            else
            {
                txtMileage.Enabled = true;
                txtCharge.Enabled = true;
                txtFailReason.Visible = false;
                var failLabel = txtFailReason.Parent.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == "Fail Reason:");

                if (failLabel != null)
                {
                    failLabel.Visible = false;
                }
            }
        }

        private void DgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvJobs.Rows[e.RowIndex];

                currentJobId = Convert.ToInt32(row.Cells["JobID"].Value);
                cmbClient.SelectedValue = Convert.ToInt32(row.Cells["ClientID"].Value);
                cmbDriver.SelectedValue = Convert.ToInt32(row.Cells["DriverID"].Value);
                cmbTaxi.SelectedValue = Convert.ToInt32(row.Cells["TaxiID"].Value);

                if (row.Cells["ContractID"].Value != DBNull.Value)
                    cmbContract.SelectedValue = Convert.ToInt32(row.Cells["ContractID"].Value);
                else
                    cmbContract.SelectedValue = 0;

                dtpPickupDateTime.Value = Convert.ToDateTime(row.Cells["PickupDateTime"].Value);
                txtPickupAddress.Text = row.Cells["PickupAddress"].Value.ToString();
                txtDropoffAddress.Text = row.Cells["DropoffAddress"].Value.ToString();

                string status = row.Cells["Status"].Value.ToString();
                cmbStatus.SelectedItem = status;

                if (row.Cells["MileageKm"].Value != DBNull.Value)
                    txtMileage.Text = Convert.ToDecimal(row.Cells["MileageKm"].Value).ToString("F2");

                if (row.Cells["ChargeAmount"].Value != DBNull.Value)
                    txtCharge.Text = Convert.ToDecimal(row.Cells["ChargeAmount"].Value).ToString("F2");

                if (row.Cells["FailReason"].Value != DBNull.Value)
                    txtFailReason.Text = row.Cells["FailReason"].Value.ToString();

                lblJobId.Text = currentJobId.ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnComplete.Enabled = status != "Completed";
                btnFail.Enabled = status != "Failed";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int jobId = JobData.AddJob(
                    dtpPickupDateTime.Value,
                    txtPickupAddress.Text.Trim(),
                    txtDropoffAddress.Text.Trim(),
                    cmbStatus.SelectedItem.ToString(),
                    decimal.TryParse(txtMileage.Text, out decimal mileage) ? mileage : (decimal?)null,
                    decimal.TryParse(txtCharge.Text, out decimal charge) ? charge : (decimal?)null,
                    txtFailReason.Text.Trim(),
                    Convert.ToInt32(cmbClient.SelectedValue),
                    Convert.ToInt32(cmbDriver.SelectedValue),
                    Convert.ToInt32(cmbTaxi.SelectedValue),
                    cmbContract.SelectedValue != null && (int)cmbContract.SelectedValue > 0 ? (int?)Convert.ToInt32(cmbContract.SelectedValue) : null
                );

                if (jobId > 0)
                {
                    MessageBox.Show("Job saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadJobs();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving job");
                MessageBox.Show("Error saving job: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentJobId == 0)
                return;

            if (!ValidateInput())
                return;

            try
            {
                bool result = JobData.UpdateJob(
                    currentJobId,
                    dtpPickupDateTime.Value,
                    txtPickupAddress.Text.Trim(),
                    txtDropoffAddress.Text.Trim(),
                    cmbStatus.SelectedItem.ToString(),
                    decimal.TryParse(txtMileage.Text, out decimal mileage) ? mileage : (decimal?)null,
                    decimal.TryParse(txtCharge.Text, out decimal charge) ? charge : (decimal?)null,
                    txtFailReason.Text.Trim(),
                    Convert.ToInt32(cmbClient.SelectedValue),
                    Convert.ToInt32(cmbDriver.SelectedValue),
                    Convert.ToInt32(cmbTaxi.SelectedValue),
                    cmbContract.SelectedValue != null && (int)cmbContract.SelectedValue > 0 ? (int?)Convert.ToInt32(cmbContract.SelectedValue) : null
                );

                if (result)
                {
                    MessageBox.Show("Job updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadJobs();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating job");
                MessageBox.Show("Error updating job: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentJobId == 0)
                return;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this job?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool deleted = JobData.DeleteJob(currentJobId);

                    if (deleted)
                    {
                        MessageBox.Show("Job deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                        LoadJobs();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error deleting job");
                    MessageBox.Show("Error deleting job: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            if (currentJobId == 0)
                return;

            if (!decimal.TryParse(txtMileage.Text, out decimal mileage) || mileage <= 0)
            {
                MessageBox.Show("Please enter valid mileage.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMileage.Focus();
                return;
            }

            if (!decimal.TryParse(txtCharge.Text, out decimal charge) || charge <= 0)
            {
                MessageBox.Show("Please enter valid charge amount.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCharge.Focus();
                return;
            }

            try
            {
                bool result = JobData.CompleteJob(currentJobId, mileage, charge);

                if (result)
                {
                    MessageBox.Show("Job completed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadJobs();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error completing job");
                MessageBox.Show("Error completing job: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFail_Click(object sender, EventArgs e)
        {
            if (currentJobId == 0)
                return;

            if (string.IsNullOrWhiteSpace(txtFailReason.Text))
            {
                MessageBox.Show("Please enter fail reason.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFailReason.Focus();
                return;
            }

            try
            {
                bool result = JobData.FailJob(currentJobId, txtFailReason.Text.Trim());

                if (result)
                {
                    MessageBox.Show("Job marked as failed!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    LoadJobs();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error failing job");
                MessageBox.Show("Error failing job: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAverageFee_Click(object sender, EventArgs e)
        {
            DataTable dt = JobData.GetAverageFeeForPrivateClients().GetType().GetProperty("Table")?.GetValue(null) as DataTable;

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Average Fee - Private Clients");
            reportForm.ShowDialog();
        }

        private void BtnJobsPerCar_Click(object sender, EventArgs e)
        {
            DataTable dt = JobData.GetJobCountPerTaxi();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Jobs Per Car");
            reportForm.ShowDialog();
        }

        private void BtnJobsPerDriver_Click(object sender, EventArgs e)
        {
            DataTable dt = JobData.GetJobCountPerDriver();

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Jobs Per Driver");
            reportForm.ShowDialog();
        }

        private void BtnRevenueNov2025_Click(object sender, EventArgs e)
        {
            DataTable dt = JobData.GetTotalChargedPerCar(new DateTime(2025, 11, 1));

            ReportsForm reportForm = new ReportsForm();
            reportForm.LoadReportData(dt, "Revenue by Car - November 2025");
            reportForm.ShowDialog();
        }

        private void ClearForm()
        {
            if (cmbClient.Items.Count > 0)
                cmbClient.SelectedIndex = -1;
            if (cmbDriver.Items.Count > 0)
                cmbDriver.SelectedIndex = -1;
            if (cmbTaxi.Items.Count > 0)
                cmbTaxi.SelectedIndex = -1;

            cmbContract.SelectedValue = 0;
            dtpPickupDateTime.Value = DateTime.Now;
            txtPickupAddress.Clear();
            txtDropoffAddress.Clear();
            cmbStatus.SelectedIndex = 0;
            txtMileage.Text = "0.00";
            txtCharge.Text = "0.00";
            txtFailReason.Clear();
            txtFailReason.Visible = false;

            currentJobId = 0;
            lblJobId.Text = "0";

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnComplete.Enabled = false;
            btnFail.Enabled = false;

            cmbClient.Focus();
        }

        private bool ValidateInput()
        {
            if (cmbClient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a client.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbClient.Focus();
                return false;
            }

            if (cmbDriver.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a driver.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDriver.Focus();
                return false;
            }

            if (cmbTaxi.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a taxi.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTaxi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPickupAddress.Text))
            {
                MessageBox.Show("Pickup address is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPickupAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDropoffAddress.Text))
            {
                MessageBox.Show("Dropoff address is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDropoffAddress.Focus();
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