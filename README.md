# Fast Taxis Database System

A comprehensive **taxi company management system** built with C# and Windows Forms. This application transforms manual operations into a fully digital solution for managing employees, vehicles, clients, contracts, and trips with real-time reporting.

![C#](https://img.shields.io/badge/C%23-12.0-purple)
![.NET](https://img.shields.io/badge/.NET-4.7.2-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-Desktop-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

## 📋 Table of Contents
- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [Installation](#-installation)
- [User Roles](#-user-roles)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

- **Staff Management** – Full employee lifecycle with role-based permissions
- **Fleet Management** – Complete taxi tracking with owner information
- **Client Management** – Support for both private individuals and corporate clients
- **Contract Handling** – Business contract management with job tracking
- **Job Management** – Complete trip lifecycle from booking to completion
- **20+ Reports** – All required queries plus nested queries
- **Role-Based Access** – 5 user levels with granular permissions
- **Modern UI** – Clean, intuitive Windows Forms interface

## 🛠️ Technology Stack

```
Frontend          Windows Forms (.NET 4.7.2)
Backend           C# 12.0
Database          SQL Server 2019
Data Access       ADO.NET
Authentication    Session-based with role permissions
Reporting         Custom Reports Module
```

## 📁 Project Structure

```
FAST_TAXIS3/
├── 📂 Helpers/
│   ├── DatabaseHelper.cs      # Database connection and queries
│   ├── SessionManager.cs      # User session and permissions
│   ├── ValidationHelper.cs    # Input validation
│   ├── Logger.cs              # Error logging
│   └── Constants.cs           # Application constants
├── 📂 Data/
│   ├── OfficeData.cs          # Office CRUD operations
│   ├── StaffData.cs           # Staff CRUD operations
│   ├── OwnerData.cs           # Owner CRUD operations
│   ├── TaxiData.cs            # Taxi CRUD operations
│   ├── AllocationData.cs      # Driver allocation operations
│   ├── ClientData.cs          # Client CRUD operations
│   ├── ContractData.cs        # Contract CRUD operations
│   ├── JobData.cs             # Job CRUD operations
│   └── ReportData.cs          # All report queries (a-t)
├── 📂 Forms/
│   ├── SplashScreenForm.cs    # Loading screen
│   ├── LoginForm.cs           # Authentication
│   ├── MainForm.cs            # Dashboard
│   ├── OfficeForm.cs          # Office management
│   ├── StaffForm.cs           # Staff management
│   ├── OwnerForm.cs           # Owner management
│   ├── TaxiForm.cs            # Taxi management
│   ├── AllocationForm.cs      # Driver allocation
│   ├── ClientForm.cs          # Client management
│   ├── ContractForm.cs        # Contract management
│   ├── JobForm.cs             # Trip management
│   ├── ReportsForm.cs         # Reports dashboard
│   └── UserManualForm.cs      # Built-in user manual
└── 📄 Program.cs              # Application entry point
```

## 🗃️ Database Schema

**13 Tables** with complete relationships and cascade delete:

```
Office (1) ────┴──── (Many) Staff
Staff (1) ─────┴──── (Many) Driver, Manager, AdminStaff
Owner (1) ─────┴──── (Many) Taxi
Office (1) ─────┴──── (Many) Taxi
Taxi (1) ─────┴──── (Many) TaxiDriverAllocation
Driver (1) ─────┴──── (Many) TaxiDriverAllocation
Client (1) ─────┴──── (Many) PrivateClient, BusinessClient
BusinessClient (1) ──┴──── (Many) Contract
Client (1) ─────┴──── (Many) Job
Driver (1) ─────┴──── (Many) Job
Taxi (1) ─────┴──── (Many) Job
Contract (1) ───┴──── (Many) Job
```

## 💻 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/FastTaxis.git
   ```

2. **Set up the database**
   ```bash
   # Run the SQL script in SQL Server Management Studio
   FastTaxis/Database/FastTaxis_Schema.sql
   ```

3. **Update connection string**
   ```csharp
   // In Helpers/DatabaseHelper.cs
   public static string ConnectionString = "Server=YOUR_SERVER;Database=FastTaxis;Integrated Security=True;";
   ```

4. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

## 👥 User Roles

| Role | Staff ID | Password | Access Level |
|------|----------|----------|--------------|
| **Director** | 1 | 1234 | Full system access |
| **Admin** | 2 | 1234 | Full system access |
| **Manager** | 3 | 1234 | Own office only |
| **Driver** | 4 | 1234 | Own jobs only |
| **Staff** | 5+ | 1234 | Clients only |

## 📊 Reports Implemented

### Basic Queries (a-t)
```
(a) Managers at each office
(b) Female drivers in Cyberjaya
(c) Staff count per office
(d) Taxis at Cyberjaya office
(e) Total registered taxis
(f) Drivers allocated per taxi
(g) Owners with multiple taxis
(h) Business clients in Cyberjaya
(i) Current contracts in Kuala Lumpur
(j) Private clients per city
(k) Jobs by driver on given day
(l) Drivers over 25 years old
(m) Private clients - Nov 2025
(n) Private clients with >3 hires
(o) Average fee - private clients
(p) Total jobs per car
(q) Total jobs per driver
(r) Total charged per car - Nov 2025
(s) Contract job summary
(t) Taxis due for maintenance - Jan 1, 2026
```

### Nested Queries
```
1) Drivers above average jobs
2) Taxis never used
3) Clients above average spending
4) Offices above average staff
5) Owners above average revenue
```

## 📸 Screenshots

| | | |
|:---:|:---:|:---:|
| **Login Form** | **Main Dashboard** | **Office Management** |
| **Staff Management** | **Job Management** | **Reports Dashboard** |

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Contact

Project Link: [https://github.com/yourusername/FastTaxis](https://github.com/yourusername/FastTaxis)

---

**Made with ❤️ for Fast Taxis**
