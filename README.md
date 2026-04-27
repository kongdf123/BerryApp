# \# 🏭 BerryApp MES System

# 

# A modern \*\*Manufacturing Execution System (MES)\*\* desktop application built with \*\*WPF (.NET 9)\*\*, designed using \*\*MVVM architecture\*\*, \*\*dependency injection\*\*, and \*\*modular navigation patterns\*\*.

# 

# This project simulates a real-world industrial monitoring system, including \*\*machine telemetry\*\*, \*\*order management\*\*, and \*\*alarm handling\*\*, making it suitable as a \*\*portfolio-level enterprise desktop application\*\*.

# 

# \---

# 

# \## 🚀 Key Features

# 

# \### 📊 Real-Time Dashboard

# 

# \* Live machine status updates (Running / Stopped / Error)

# \* Simulated telemetry (e.g., temperature)

# \* Event-driven alarm triggering

# 

# \### 📦 Orders Management

# 

# \* Create and delete production orders

# \* Data binding via `ObservableCollection`

# \* MVVM command-based interactions

# 

# \### 🚨 Alarm System

# 

# \* Event-driven architecture using a custom EventBus

# \* Real-time alarm panel updates

# \* Timestamped alert tracking

# 

# \### 🧭 Navigation System

# 

# \* Sidebar-based navigation

# \* Dynamic page switching via `ContentControl`

# \* ViewModel → View resolution using DataTemplates

# 

# \### 🧱 Architecture Highlights

# 

# \* Strict \*\*MVVM separation\*\*

# \* Layered design (\*\*UI / Application / Domain / Infrastructure\*\*)

# \* Centralized \*\*Dependency Injection\*\* using `Microsoft.Extensions.DependencyInjection`

# 

# \---

# 

# \## 🧩 Project Structure

# 

# ```

# BerryApp/

# 

# ├── BerryApp.WPF/

# │   ├── App.xaml / App.xaml.cs       # Application bootstrap + DI + DataTemplates

# │   ├── MainWindow.xaml             # Shell (Sidebar + ContentControl)

# │

# │   ├── Pages/

# │   │   ├── DashboardView.xaml      # Real-time machine dashboard

# │   │   ├── OrdersView.xaml         # Order management UI

# │   │   └── AlarmsView.xaml         # Alarm panel UI

# │

# │   ├── ViewModels/

# │   │   ├── MainViewModel.cs        # Navigation + app orchestration

# │   │   ├── DashboardViewModel.cs  # Monitoring logic

# │   │   ├── OrdersViewModel.cs     # CRUD logic

# │   │   └── AlarmsViewModel.cs     # Event-driven alarm handling

# │

# ├── BerryApp.Domain/                # Core business entities

# ├── BerryApp.Biz/                   # Application services (use cases)

# ├── BerryApp.Infra/                 # Infrastructure (PLC, persistence)

# ├── BerryApp.Shared/                # Shared utilities (EventBus, base classes)

# ```

# 

# \---

# 

# \## ⚙️ Getting Started

# 

# \### Prerequisites

# 

# \* .NET 9 SDK

# \* Visual Studio 2022 (17.10+)

# 

# \---

# 

# \### Run the Application

# 

# ```bash

# git clone <your-repo-url>

# cd BerryApp

# ```

# 

# Open `BerryApp.WPF.sln` in Visual Studio and press \*\*F5\*\*.

# 

# \---

# 

# \## 🧠 Architecture Overview

# 

# \### 🔹 MVVM Pattern

# 

# \* Views contain no business logic

# \* ViewModels expose bindable state and commands

# \* Full separation of concerns

# 

# \---

# 

# \### 🔹 Navigation Mechanism

# 

# ```plaintext

# Sidebar Button → Command → NavigationService → CurrentView

# &#x20;                                     ↓

# &#x20;                            ContentControl (UI)

# ```

# 

# \* `ContentControl` binds to `CurrentView`

# \* `DataTemplate` maps ViewModel → View automatically

# 

# \---

# 

# \### 🔹 Event-Driven Design

# 

# ```plaintext

# Machine Update → EventBus → AlarmEvent → AlarmsViewModel → UI

# ```

# 

# \* Decouples modules

# \* Enables real-time UI updates

# 

# \---

# 

# \### 🔹 Dependency Injection

# 

# Configured in `App.xaml.cs`:

# 

# \* Services (MachineService, PlcService)

# \* ViewModels

# \* NavigationService

# \* EventBus

# 

# \---

# 

# \## 🛠️ Technologies Used

# 

# \* \*\*WPF (.NET 9)\*\*

# \* \*\*MVVM Pattern\*\*

# \* \*\*Microsoft.Extensions.DependencyInjection\*\*

# \* \*\*ObservableCollection / INotifyPropertyChanged\*\*

# \* \*\*Custom Event Bus (lightweight pub/sub)\*\*

# 

# \---

# 

# \## 📈 Roadmap (Next Improvements)

# 

# This project is designed to evolve toward a \*\*production-grade MES system\*\*:

# 

# \* \[ ] Real-time charts (e.g., LiveCharts2)

# \* \[ ] Database integration (Dapper + SQL Server)

# \* \[ ] PLC communication (Modbus / OPC UA)

# \* \[ ] Authentication \& role-based UI

# \* \[ ] Logging system (Serilog)

# \* \[ ] Multi-machine dashboard scaling

# 

# \---

# 

# \## 🎯 Purpose of This Project

# 

# This project demonstrates:

# 

# \* Enterprise-level WPF architecture

# \* Real-time UI handling

# \* Decoupled system design

# \* Practical MES domain modeling

# 

# It is intended for:

# 

# \* Portfolio showcase

# \* Interview preparation

# \* Industrial desktop application learning

# 

# \---

# 

# \## 📄 License

# 

# MIT License

# 

# \---

# 

# \## 🤝 Contributions

# 

# Contributions, issues, and suggestions are welcome.

# Feel free to open a Pull Request or Issue.

# 

# \---



