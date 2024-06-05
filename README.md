# Recrutement

Recrutement is an ASP .Net Core web application designed to streamline the recruitment process. This project features a comprehensive platform for job seekers, recruiters, and public users to interact with job postings, manage applications, and facilitate the hiring process.

## Features

### Espace Offre
Accessible to everyone, this section displays all available job offers. Key functionalities include:

- Displaying job offers with details such as ID, recruiter ID, contract type (CDD, CDI), sector, profile (Deug, Licence, Master, Ingénieur, etc.), position, and remuneration.
- Performing multi-criteria searches for job offers by sector, profile, remuneration, and more.

### Espace Recruteur
A recruiter, defined by attributes such as ID, name, phone, and email, can perform the following operations:

- Manage their job offers (add, modify, or delete).
- Display and review applicants who have applied for their offers, and access their CVs.
- View statistics of their job applications.
- Browse job offers posted by other recruiters.

### Espace Candidat
A candidate, defined by attributes such as ID, name, surname, age, title, degree, years of experience, and CV, can perform the following operations:

- Browse all job offers.
- Conduct searches for job opportunities.
- Apply for job offers.
- View the history of offers they have applied to.

### Authentication
Both recruiters and candidates must authenticate before performing any operations.

### User Interface
The web interfaces are designed using Bootstrap for a responsive and modern look.

### Deployment
The web application is deployed on IIS (Internet Information Services).

## Getting Started

### Prerequisites

- .NET Core SDK
- SQL Server
- IIS (Internet Information Services)

### Installation

1. **Clone the repository**:
    ```sh
    git clone https://github.com/AyoubTe/E-recrutement.git
    cd E-recrutement
    ```

2. **Set up the database**:
    - Ensure SQL Server is running.
    - Update the database connection string in `appsettings.json`.

3. **Run the application**:
    ```sh
    dotnet run
    ```

4. **Deploy on IIS**:
    - Publish the application:
      ```sh
      dotnet publish --configuration Release
      ```
    - Configure IIS to point to the published output.

### Access the Application

Open your browser and navigate to `http://localhost:5000` (or the configured IIS URL).

## Contributing

We welcome contributions from the community! If you’d like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Create a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or suggestions, feel free to reach out to us at:

- GitHub Issues: [https://github.com/AyoubTe/E-recrutement/issues](https://github.com/AyoubTe/E-recrutement/issues)

---

Thank you for using Recrutement! Together, we can simplify and enhance the hiring process.
