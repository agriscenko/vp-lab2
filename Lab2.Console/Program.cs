using Lab2.DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lab2.Console;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new DepartmentDbContext();

        db.Database.EnsureCreated();

        Linq1(db);
        Linq2(db);
        Linq3(db);
        Linq4(db);
        Linq5(db);
        Linq6(db);
        Linq7(db);
        Linq8(db);

        ReseedIds(db);
    }

    [Description("Izvadīt nodaļu skaitu pirmajā stāvā.")]
    private static void Linq1(DepartmentDbContext db)
    {
        var results = db.Departments.Where(d => d.FloorNumber == 1);

        System.Console.WriteLine($"Nodaļu skaits pirmajā stāvā: {results.Count()}.");
    }

    [Description("Izvadīt nodaļas nosaukumu ar ID=100, izmantojot FirstOrDefault().")]
    private static void Linq2(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Id == 100);

        if (department != null)
        {
            System.Console.WriteLine($"Atrasta nodaļa: {department.Name}");
        }
        else
        {
            System.Console.WriteLine("Neviena nodaļa ar ID=100 netika atrasta!");
        }
    }

    [Description("Pievienot vienu ierakstu tabulai Departments.")]
    private static void Linq3(DepartmentDbContext db)
    {
        var department = new Department
        {
            Name = "test",
            FloorNumber = 1,
            Email = "test@example.com",
            PhoneNumber = "+371 67123000",
            Rating = 0.0m
        };

        db.Departments.Add(department);

        db.SaveChanges();
    }

    [Description("Pievienot vairākus ierakstus.")]
    private static void Linq4(DepartmentDbContext db)
    {
        var departments = new List<Department>
        {
            new Department
            {
                Name = "Personāla nodaļa",
                FloorNumber = 1,
                Email = "hr@example.com",
                PhoneNumber = "+371 67123001",
                Rating = 4.8m
            },
            new Department {
                Name = "Pārdošanas nodaļa",
                FloorNumber = 1,
                Email = "sales@example.com",
                PhoneNumber = "+371 67123002",
                Rating = 4.5m
            },
            new Department
            {
                Name = "Mārketinga nodaļa",
                FloorNumber = 1,
                Email = "marketing@example.com",
                PhoneNumber = "+371 67123003",
                Rating = 4.6m
            },
            new Department {
                Name = "Grāmatvedības nodaļa",
                FloorNumber = 2,
                Email = "accounting@example.com",
                PhoneNumber = "+371 67123004",
                Rating = 4.7m
            },
            new Department {
                Name = "Klientu atbalsta nodaļa",
                FloorNumber = 2,
                Email = "support@example.com",
                PhoneNumber = "+371 67123005",
                Rating = 4.9m
            },
            new Department {
                Name = "IT nodaļa",
                FloorNumber = 3,
                Email = "it@example.com",
                PhoneNumber = "+371 67123006",
                Rating = 4.8m,
                Description = "Emails get lost. Tickets get answered. :)"
            }
        };

        db.Departments.AddRange(departments);

        db.SaveChanges();
    }

    [Description("Izdēst vienu ierakstu.")]
    private static void Linq5(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Name == "test");

        if (department != null)
        {
            db.Departments.Remove(department);

            db.SaveChanges();
        }
    }

    [Description("Modificēt vienu ierakstu.")]
    private static void Linq6(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Name == "Grāmatvedības nodaļa");

        if (department != null)
        {
            department.Rating = 5.0m;
            department.LastAuditDate = DateTime.Now;

            db.SaveChanges();

            System.Console.WriteLine($"Nosaukums: {department.Name}, Reitings: {department.Rating}, Audits veikts: {department.LastAuditDate}.");
        }
    }

    [Description("Izvadīt visus trešā stāva nodaļu nosaukumus un aprakstus, kā arī katras nodaļas darbiniekus un viņu amatus.")]
    private static void Linq7(DepartmentDbContext db)
    {
        var results = db.Departments
            .Include(d => d.Employees)
            .Where(d => d.FloorNumber == 3);

        foreach (var department in results)
        {
            System.Console.WriteLine($"Nodaļa: {department.Name}, Apraksts: {department.Description}");

            foreach (var employee in department.Employees)
            {
                System.Console.WriteLine($"Darbinieks: {employee.FirstName} {employee.LastName}, Amats: {employee.Position}");
            }
        }
    }

    [Description("Izdēst visus ierakstus.")]
    private static void Linq8(DepartmentDbContext db)
    {
        var all_employees = db.Employees.ToList();
        var all_departments = db.Departments.ToList();

        db.RemoveRange(all_employees);
        db.RemoveRange(all_departments);

        db.SaveChanges();
    }

    private static void ReseedIds(DepartmentDbContext db)
    {
        var tables = new List<string> { "Departments", "Employees" };

        foreach (var table in tables)
        {
            var sql = $"DBCC CHECKIDENT ('{table}', RESEED, 0);";

            db.Database.ExecuteSqlRaw(sql);
        }
    }
}
