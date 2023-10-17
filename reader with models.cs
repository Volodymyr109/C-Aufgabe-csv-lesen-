using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

internal class Program
{
    static void Main(string[] args)
    {
        // Lese Rechnung.csv
        var rechnungCsvPath = @"D:\Aufgabe NICETEC\Rechnung.csv";
        ReadAndProcessCsv<RechnungModel>(rechnungCsvPath, "Rechnung");

        // Lese Kunden.csv
        var kundenCsvPath = @"D:\Aufgabe NICETEC\Kunden.csv";
        ReadAndProcessCsv<KundenModel>(kundenCsvPath, "Kunden");

        // Lese Artikel.csv
        var artikelCsvPath = @"D:\Aufgabe NICETEC\Artikel.csv";
        ReadAndProcessCsv<ArtikelModel>(artikelCsvPath, "Artikel");
    }

    static void ReadAndProcessCsv<T>(string csvPath, string typeName)
    {
        using (var reader = new StreamReader(csvPath))
        {
            while (reader.EndOfStream == false)
            {
                var content = reader.ReadLine();
                content = content?.Trim();
                if (!string.IsNullOrEmpty(content))
                {
                    var cells = content.Split(',').Select(cell => cell.Trim()).ToList();
                    if (cells.Count >= 3)
                    {
                        if (int.TryParse(cells[0], out int kundenNr) && int.TryParse(cells[1], out int artikelNr) && int.TryParse(cells[2], out int anzahl))
                        {
                            var item = Activator.CreateInstance<T>();
                            if (item is RechnungModel rechnung)
                            {
                                rechnung.KundenNr = kundenNr;
                                rechnung.ArtikelNr = artikelNr;
                                rechnung.Anzahl = anzahl;
                                Console.WriteLine($"{typeName} - KundenNr: {rechnung.KundenNr}, ArtikelNr: {rechnung.ArtikelNr}, Anzahl: {rechnung.Anzahl}");
                            }
                            else if (item is KundenModel kunde)
                            {
                                // Verarbeite KundenModel
                                ProcessKundenModel(kunde, cells);
                            }
                            else if (item is ArtikelModel artikel)
                            {
                                // Verarbeite ArtikelModel
                                ProcessArtikelModel(artikel, cells);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"ERROR: Konvertierung fehlgeschlagen für {typeName}");
                        }
                    }
                }
            }
        }
    }

    static void ProcessKundenModel(KundenModel kunde, List<string> cells)
    {
        kunde.Name = cells[3]; // Annahme: Name befindet sich in der vierten Spalte
        kunde.Strasse = cells[4]; // Annahme: Strasse in der fünften Spalte
        kunde.Ort = cells[5]; // Annahme: Ort in der sechsten Spalte
        Console.WriteLine($"Kunden - KundenNr: {kunde.KundenNr}, Name: {kunde.Name}, Strasse: {kunde.Strasse}, Ort: {kunde.Ort}");
    }

    static void ProcessArtikelModel(ArtikelModel artikel, List<string> cells)
    {
        artikel.Name = cells[3]; // Annahme: Name befindet sich in der vierten Spalte
        if (float.TryParse(cells[4], out float preis))
        {
            artikel.Preis = preis;
        }
        Console.WriteLine($"Artikel - ArtikelNr: {artikel.ArtikelNr}, Name: {artikel.Name}, Preis: {artikel.Preis}");
    }
}
