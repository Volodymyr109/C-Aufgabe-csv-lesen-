using System;
using System.Collections.Generic;
using System.IO;

public class Rechnung
{
    public int KundenNr { get; set; }
    public int ArtikelNr { get; set; }
    public double Anzahl { get; set; }


    public Rechnung(int kundenNr, int artikelNr, int anzahl)
    {
        KundenNr = kundenNr;
        ArtikelNr = artikelNr;
        Anzahl = anzahl;
    }
    // public string ToCSV()
    // {
    //     return KundenNr + ";" + ArtikelNr + ";" + Anzahl;
    // }

    // public static void ToCSV(List<Rechnung> rechnungen)
    // {
    //     using (StreamWriter sw = new StreamWriter("RechnungTEST.csv"))
    //     {
    //         foreach (Rechnung rechnung in rechnungen)
    //         {
    //             sw.WriteLine(rechnung.ToCSV());
    //         }
    //     }
    // }

    public static List<Rechnung> FromCSV()
    {
        List<Rechnung> rechnungen = new List<Rechnung>();

        using (StreamReader sr = new StreamReader("RechnungTEST.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 3 && int.TryParse(parts[0], out int kundenNr) && int.TryParse(parts[1], out int artikelNr) && int.TryParse(parts[2], out int anzahl))
                {
                    rechnungen.Add(new Rechnung(kundenNr, artikelNr, anzahl));
                }
            }
        }

        return rechnungen;
    }
}
public class Kunde
{
    public int KundenNr { get; set; }
    public string Name { get; set; }
    public string Strasse { get; set; }
    public string Ort { get; set; }

    public Kunde(int kundenNr, string name, string strasse, string ort)
    {
        KundenNr = kundenNr;
        Name = name;
        Strasse = strasse;
        Ort = ort;
    }

    // public string ToCSV()
    // {
    //     return $"{KundenNr};{Name};{Strasse};{Ort}";
    // }

    // public static void ToCSV(List<Kunde> kunden)
    // {
    //     using (StreamWriter sw = new StreamWriter("Kunden.csv"))
    //     {
    //         foreach (Kunde kunde in kunden)
    //         {
    //             sw.WriteLine(kunde.ToCSV());
    //         }
    //     }
    // }

    public static List<Kunde> FromCSV()
    {  
        List<Kunde> kunden = new List<Kunde>();

        using (StreamReader sr = new StreamReader("KundenTEST.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 4 && int.TryParse(parts[0], out int kundenNr))
                {
                    string name = parts[1];
                    string strasse = parts[2];
                    string ort = parts[3];

                    kunden.Add(new Kunde(kundenNr, name, strasse, ort));
                }
            }
        }

        return kunden;
    }
}
public class Artikel
{
    public int ArtikelNr { get; set; }
    public string Name { get; set; }
    public double Preis { get; set; }

    public Artikel(int artikelNr, string name, float preis)
    {
        ArtikelNr = artikelNr;
        Name = name;
        Preis = preis;
    }

    // public string ToCSV()
    // {
    //     return $"{ArtikelNr};{Name};{Preis}";
    // }

    // public static void ToCSV(List<Artikel> artikels)
    // {
    //     using (StreamWriter sw = new StreamWriter("Artikel.csv"))
    //     {
    //         foreach (Artikel artikel in artikels)
    //         {
    //             sw.WriteLine(artikel.ToCSV());
    //         }
    //     }
    // }

    public static List<Artikel> FromCSV()
    {
        List<Artikel> artikels = new List<Artikel>();

        using (StreamReader sr = new StreamReader("ArtikelTEST.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 3 && int.TryParse(parts[0], out int artikelNr) && float.TryParse(parts[2], out float preis))
                {
                    string name = parts[1];

                    artikels.Add(new Artikel(artikelNr, name, preis));
                }
            }
        }

        return artikels;
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-----------------Kunden----------------");
        List<Kunde> kunden = Kunde.FromCSV();
        foreach (Kunde kunde in kunden)
        {
            Console.WriteLine($"KundenNr: {kunde.KundenNr}, Name: {kunde.Name}, Strasse: {kunde.Strasse}, Ort: {kunde.Ort}.");
        }

        Console.WriteLine("-----------------Artikel----------------");
        List<Artikel> artikels = Artikel.FromCSV();
        foreach (Artikel artikel in artikels)
        {
            Console.WriteLine($"ArtikelNr: {artikel.ArtikelNr}, Name: {artikel.Name}, Preis: {artikel.Preis:C}");
        }

        Console.WriteLine("-----------------Rechnung----------------");
        List<Rechnung> rechnungen = Rechnung.FromCSV();

        var artikelDictionary = artikels.ToDictionary(a => a.ArtikelNr);

        foreach (Rechnung rechnung in rechnungen)
        {
            Console.WriteLine($"KundenNr: {rechnung.KundenNr}, ArtikelNr: {rechnung.ArtikelNr}, Anzahl: {rechnung.Anzahl}");
        }

        Console.WriteLine("---------------Gesamtbetrag--------------");
        GesamtbetragProKunde(kunden, rechnungen, artikelDictionary);
    }
    public static void GesamtbetragProKunde(List<Kunde> kunden, List<Rechnung> rechnungen, Dictionary<int, Artikel> artikelDictionary)
    {
        foreach (var kunde in kunden)
        {
            var kundenRechnungen = rechnungen.Where(r => r.KundenNr == kunde.KundenNr).ToList();

            double gesamtbetrag = 0;
            foreach (var rechnung in kundenRechnungen)
            {
                if (artikelDictionary.TryGetValue(rechnung.ArtikelNr, out var artikel))
                {
                    gesamtbetrag += rechnung.Anzahl * artikel.Preis;
                }
            }

            Console.WriteLine($"Kunde: {kunde.Name}, Gesamtbetrag: {gesamtbetrag:C}");
        }
    }
}
// internal class Program
// {
    
//     static void Main(string[] args)
//     {
//         Console.WriteLine("-----------------Kunden----------------");
//         List<Kunde> kunden = Kunde.FromCSV();
//         foreach (Kunde kunde in kunden)
//         {
//             Console.WriteLine($"KundenNr: {kunde.KundenNr}, Name: {kunde.Name}, Strasse: {kunde.Strasse}, Ort: {kunde.Ort}.");
//         }

//         Console.WriteLine("-----------------Artikel----------------");
//         List<Artikel> artikels = Artikel.FromCSV();
//         foreach (Artikel artikel in artikels)
//         {
//             Console.WriteLine($"ArtikelNr: {artikel.ArtikelNr}, Name: {artikel.Name}, Preis: {artikel.Preis}");
//         }
//         Console.WriteLine("-----------------Rechnung----------------");
//         List<Rechnung> rechnungen = Rechnung.FromCSV();
//         foreach (Rechnung rechnung in rechnungen)
//         {
//             Console.WriteLine($"KundenNr: {rechnung.KundenNr}, ArtikelNr: {rechnung.ArtikelNr}, Anzahl: {rechnung.Anzahl}");
//         }
//         GesamtbetragProKunde(kunden, rechnungen);
//     }
// }