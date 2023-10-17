using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

public class RechnungModel
{
    public int KundenNr { get; set; }
    public int ArtikelNr { get; set; }
    public int Anzahl { get; set; }
}

public class KundenModel 
{
    public int KundenNr { get; set; }
    public string Name { get; set; }
    public string Strasse { get; set; }
    public string Ort { get; set; }
}

public class ArtikelModel
{
    public int ArtikelNr { get; set; } 
    public string Name { get; set; }
    public float Preis { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string rechnungCsvFilePath = "Rechnung.csv";
        string kundenCsvFilePath = "Kunden.csv";
        string artikelCsvFilePath = "Artikel.csv";

        var rechnungen = ReadCsvFile<RechnungModel>(rechnungCsvFilePath);
        var kunden = ReadCsvFile<KundenModel>(kundenCsvFilePath);
        var artikel = ReadCsvFile<ArtikelModel>(artikelCsvFilePath);

        Console.WriteLine("Rechnungen:");
        foreach (var rechnung in rechnungen)
        {
            Console.WriteLine($"KundenNr: {rechnung.KundenNr}, ArtikelNr: {rechnung.ArtikelNr}, Anzahl: {rechnung.Anzahl}");
        }

        Console.WriteLine("\nKunden:");
        foreach (var kunde in kunden)
        {
            Console.WriteLine($"KundenNr: {kunde.KundenNr}, Name: {kunde.Name}, Strasse: {kunde.Strasse}, Ort: {kunde.Ort}");
        }

        Console.WriteLine("\nArtikel:");
        foreach (var artikelEntry in artikel)
        {
            Console.WriteLine($"ArtikelNr: {artikelEntry.ArtikelNr}, Name: {artikelEntry.Name}, Preis: {artikelEntry.Preis}");
        }
    }

    static List<T> ReadCsvFile<T>(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            return csv.GetRecords<T>().ToList();
        }
    }
}
