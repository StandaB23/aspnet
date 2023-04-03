using InsuranceCorp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InsuranceCorp.Data;
public class DataInitialization
{
    public static List<Person> LoadDataFromJson(string path = "data.json")
    {
        var jsonString = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<Person>>(jsonString);
    }

    public static void InitDb(List<Person> data)
    {
        using var db = new InsCorpDbContext();

        if (!db.Database.CanConnect())
        {
            Console.WriteLine("Vytvářím databázi..");
            db.Database.EnsureCreated();
            Console.WriteLine("OK - Databáze vytvořená.");

            Console.WriteLine($"Vkládám data: {data.Count()} položek.");
            db.Persons.AddRange(data);
            db.SaveChanges();
            Console.WriteLine("OK - uloženo");
        }
        else
        {
            Console.WriteLine("!!! databaze jiz existuje !!!");
        }
    }

    public static void Migrate()
    {
        using var db = new InsCorpDbContext();
        db.Database.Migrate();
    }

    public static void TestDb()
    {
        using var db = new InsCorpDbContext();

        Console.WriteLine("SQL query, take 10:");
        var first10 = db.Persons.Include(x => x.Address).Include(x => x.Constracts).Take(10).ToList();
        Console.WriteLine($"OK - {first10.Count()}");

        Console.WriteLine("SQL query, address null");
        //var addrnull = db.Persons.Include(x => x.Address).Where(x => x.Address == null).ToList();
        var addrnull = db.Persons.Where(x => x.Address == null);
        Console.WriteLine($"OK - {addrnull.Count()}");

        Console.WriteLine("SQL query, no contracts");
        //var nocontr = db.Persons.Include(x => x.Contracts).Where(x => x.Contracts == null || !x.Contracts.Any()).ToList();
        var nocontr = db.Persons.Where(x => x.Constracts == null || !x.Constracts.Any());
        Console.WriteLine($"OK - {nocontr.Count()}");
    }
}
