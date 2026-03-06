# Fast Taxis Database System

![C#](https://img.shields.io/badge/C%23-12.0-purple)
![.NET](https://img.shields.io/badge/.NET-4.7.2-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2020-red)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-Desktop-green)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)

A comprehensive **taxi company management system** built with C# and Windows Forms. This application transforms manual operations into a fully digital solution for managing employees, vehicles, clients, contracts, and trips with real-time reporting.

---

## 📋 Table of Contents
- [Overview](#-overview)
- [✨ Features](#-features)
- [🛠️ Technology Stack](#️-technology-stack)
- [📁 Project Structure](#-project-structure)
- [🗃️ Database Schema](#️-database-schema)
- [👥 User Roles](#-user-roles)
- [📊 Reports](#-reports)
- [🚀 Getting Started](#-getting-started)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)

---

## 📌 Overview

**Fast Taxis Database System** is a complete ERP solution developed for Fast Taxis company. It replaces manual paperwork with a digital system that streamlines operations across multiple offices in Kuala Lumpur and Selangor.

**Key Facts:**
- 27 source files
- 13 database tables
- 20+ reports
- 4 user roles
- 32,000+ lines of code

---

## ✨ Features

| Module | Description |
|--------|-------------|
| **👥 Staff Management** | Complete employee lifecycle with role-based permissions |
| **🚖 Fleet Management** | Full taxi tracking with owner information and maintenance |
| **👤 Client Management** | Support for both private individuals and corporate clients |
| **📄 Contract Handling** | Business contract management with job tracking |
| **📦 Job Management** | Complete trip lifecycle from booking to completion |
| **📊 20+ Reports** | All required queries (a-t) plus 5 nested queries |
| **🔐 Role-Based Access** | 4 user levels with granular permissions |
| **⚡ Performance** | Optimized queries with proper indexing |

---

## 🛠️ Technology Stack

```
├── Frontend         →  Windows Forms (.NET 4.7.2)
├── Backend          →  C# 12.0
├── Database         →  SQL Server 20
├── Data Access      →  ADO.NET
├── Authentication   →  Session-based with role permissions
├── Logging          →  Custom Logger class
└── Validation       →  Regex and custom validators
```

---

## 📁 Project Structure

```
FAST_TAXIS3/
├── 📂 Helpers/                  # Utility classes
│   ├── DatabaseHelper.cs        # Database connection manager
│   ├── SessionManager.cs        # User session & permissions
│   ├── ValidationHelper.cs      # Input validation
│   ├── Logger.cs                 # Error logging
│   └── Constants.cs              # Application constants
│
├── 📂 Data/                      # Data access layer
│   ├── OfficeData.cs             # Office operations
│   ├── StaffData.cs              # Staff operations
│   ├── OwnerData.cs              # Owner operations
│   ├── TaxiData.cs               # Taxi operations
│   ├── AllocationData.cs         # Driver allocation
│   ├── ClientData.cs             # Client operations
│   ├── ContractData.cs           # Contract operations
│   ├── JobData.cs                # Job operations
│   └── ReportData.cs             # All report queries (a-t)
│
└── 📂 Forms/                      # User interface
    ├── SplashScreenForm.cs        # Loading screen
    ├── LoginForm.cs               # Authentication
    ├── MainForm.cs                 # Dashboard
    ├── OfficeForm.cs               # Office management
    ├── StaffForm.cs                # Staff management
    ├── OwnerForm.cs                # Owner management
    ├── TaxiForm.cs                 # Taxi management
    ├── AllocationForm.cs           # Driver allocation
    ├── ClientForm.cs               # Client management
    ├── ContractForm.cs             # Contract management
    ├── JobForm.cs                  # Trip management
    ├── ReportsForm.cs              # Reports dashboard
    └── UserManualForm.cs           # Built-in user manual
```

---

## 🗃️ Database Schema

**13 Tables** with complete relationships and cascade delete:

```
Office ────┴──── Staff
Staff ─────┴──── Driver, Manager, AdminStaff
Owner ─────┴──── Taxi
Office ─────┴──── Taxi
Taxi ─────┴──── TaxiDriverAllocation
Driver ─────┴──── TaxiDriverAllocation
Client ─────┴──── PrivateClient, BusinessClient
BusinessClient ──┴──── Contract
Client ─────┴──── Job
Driver ─────┴──── Job
Taxi ─────┴──── Job
Contract ───┴──── Job
```

---

## 👥 User Roles

| Role | Staff ID | Password | Access Level |
|------|----------|----------|--------------|
| **Director** | 1 | 1234 | Full system access |
| **Admin** | 2 | 1234 | Full system access |
| **Manager** | 3 | 1234 | Own office only |
| **Driver** | 4 | 1234 | Own jobs only |

**Permission Matrix:**

| Module | Director | Admin | Manager | Driver |
|--------|----------|-------|---------|--------|
| Offices | ✅ | ✅ | ❌ | ❌ |
| Staff | ✅ | ✅ | ⚠️ Office only | ❌ |
| Owners | ✅ | ✅ | ✅ | ❌ |
| Taxis | ✅ | ✅ | ⚠️ Office only | ❌ |
| Allocation | ✅ | ✅ | ⚠️ Office only | ❌ |
| Clients | ✅ | ✅ | ✅ | ❌ |
| Contracts | ✅ | ✅ | ⚠️ Office only | ❌ |
| Jobs | ✅ | ✅ | ⚠️ Office only | ⚠️ Own only |
| Reports | ✅ | ✅ | ⚠️ Office only | ❌ |

---

## 📊 Reports

### Basic Queries (a-t)

| Query | Description |
|-------|-------------|
| (a) | Managers at each office |
| (b) | Female drivers in Cyberjaya |
| (c) | Staff count per office |
| (d) | Taxis at Cyberjaya office |
| (e) | Total registered taxis |
| (f) | Drivers allocated per taxi |
| (g) | Owners with multiple taxis |
| (h) | Business clients in Cyberjaya |
| (i) | Current contracts in Kuala Lumpur |
| (j) | Private clients per city |
| (k) | Jobs by driver on given day |
| (l) | Drivers over 25 years old |
| (m) | Private clients - Nov 2025 |
| (n) | Private clients with >3 hires |
| (o) | Average fee - private clients |
| (p) | Total jobs per car |
| (q) | Total jobs per driver |
| (r) | Total charged per car - Nov 2025 |
| (s) | Contract job summary |
| (t) | Taxis due for maintenance - Jan 1, 2026 |

### Nested Queries

| # | Description |
|---|-------------|
| 1 | Drivers above average jobs |
| 2 | Taxis never used |
| 3 | Clients above average spending |
| 4 | Offices above average staff |
| 5 | Owners above average revenue |

---

## 🚀 Getting Started

### Prerequisites
- Windows 10/11
- SQL Server 2019 or higher
- .NET Framework 4.7.2

### Quick Setup
1. Clone the repository
2. Run the database script
3. Update connection string
4. Build and run

---

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License.

---

## 📞 Contact

- **GitHub**: [@AbdulrahmanZaid9](https://github.com/AbdulrahmanZaid9)
- **Project Link**: [https://github.com/AbdulrahmanZaid9/FAST_TAXIS.git](https://github.com/AbdulrahmanZaid9/FAST_TAXIS.git)

---

**Made with ❤️ for Fast Taxis**
