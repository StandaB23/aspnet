
using System;
using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.EntityFrameworkCore;

var data = InsuranceCorp.Data.DataInitialization.LoadDataFromJson();
Console.WriteLine($"Načteno položek z data.json: {data.Count()}");

try
{
    DataInitialization.InitDb(data);

    DataInitialization.TestDb();

    Console.WriteLine("Konec");
}
catch (Exception ex)
{
    var origColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine( ex.ToString() );
    Console.ForegroundColor = origColor;
}




