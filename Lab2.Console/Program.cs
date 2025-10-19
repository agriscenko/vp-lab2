using Microsoft.EntityFrameworkCore;
using Lab2.DataAccess;
using System.ComponentModel;

namespace Lab2.Console;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new DepartmentDbContext();

        db.Database.EnsureCreated();

        Linq1(db);
        Linq2(db);
    }

    [Description("Izvadīt nodaļu skaitu pirmajā stāvā.")]
    private static void Linq1(DepartmentDbContext db)
    {
        var results = db.Departments
            .Where(d => d.FloorNumber == 1);

        System.Console.WriteLine($"Nodaļu skaits pirmajā stāvā: {results.Count()}");
    }

    [Description("Izvadīt visu trešā stāva nodaļu nosaukumus un aprakstus, kā arī katras nodaļas darbiniekus un viņu amatus.")]
    private static void Linq2(DepartmentDbContext db)
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
}
