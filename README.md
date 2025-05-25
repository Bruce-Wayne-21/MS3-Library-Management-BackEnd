# MS3-Library-Management-BackEnd

This repository contains the backend implementation for the **Library Management System (MS3)**. Built with C#, it provides core APIs and logic for managing library resources, user accounts, transactions, and more.

---

## 📚 Features

- **Book Management:** Add, update, delete, and search for books.
- **User Management:** Register library users, update user information, and manage library cards.
- **Borrow/Return System:** Track borrowing, returning, and overdue books.
- **Transaction Logging:** Maintain records for all library operations.
- **Authentication & Authorization:** Secure endpoints for different user roles (e.g., admin, librarian, member).
- **Reporting:** Generate reports on inventory, users, and transactions.

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6 or above)
- SQL Server or compatible database

### Setup Instructions

1. **Clone the repository:**
    ```sh
    git clone https://github.com/Bruce-Wayne-21/MS3-Library-Management-BackEnd.git
    cd MS3-Library-Management-BackEnd
    ```

2. **Configure the database:**
   - Update the connection string in `appsettings.json` to match your SQL Server setup.

3. **Run database migrations (if applicable):**
    ```sh
    dotnet ef database update
    ```

4. **Build and run the application:**
    ```sh
    dotnet build
    dotnet run
    ```

5. **Access the API:**
   - By default, the API will be available at `http://localhost:5000` (or as configured).

---

## 📂 Project Structure

- `Controllers/`: API endpoints for resources
- `Models/`: Data models and DTOs
- `Services/`: Business logic services
- `Data/`: Database context and migrations
- `appsettings.json`: Configuration file

---

## 🧑‍💻 Contributing

Contributions are welcome!  
Please open issues for bugs or feature requests, and feel free to submit pull requests.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

**Happy coding and managing your library!** 📖
