using System;
using System.Collections.Generic;
using System.IO;

public class Rechnung
{
    public int KundenNr { get; set; }
    public int ArtikelNr { get; set; }
    public int Anzahl { get; set; }

    public Rechnung(int kundenNr, int artikelNr, int anzahl)
    {
        KundenNr = kundenNr;
        ArtikelNr = artikelNr;
        Anzahl = anzahl;
    }

    public string ToCSV()
    {
        return KundenNr + ";" + ArtikelNr + ";" + Anzahl;
    }

    public static void ToCSV(List<Rechnung> rechnungen)
    {
        using (StreamWriter sw = new StreamWriter("RechnungTEST.csv"))
        {
            foreach (Rechnung rechnung in rechnungen)
            {
                sw.WriteLine(rechnung.ToCSV());
            }
        }
    }

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

internal class Program
{
    static void Main(string[] args)
    {
        List<Rechnung> rechnungen = Rechnung.FromCSV();
        foreach (Rechnung rechnung in rechnungen)
        {
            Console.WriteLine($"KundenNr: {rechnung.KundenNr}, ArtikelNr: {rechnung.ArtikelNr}, Anzahl: {rechnung.Anzahl}");
        }
    }
}
// using System;
// using System.Collections.Generic;
// using System.IO;

// public class Rechnung
// {
//     public int KundenNr { get; set; }
//     public int ArtikelNr { get; set; }
//     public int Anzahl { get; set; }

//     public Rechnung(int kundenNr, int artikelNr, int anzahl)
//     {
//         KundenNr = kundenNr;
//         ArtikelNr = artikelNr;
//         Anzahl = anzahl;
//     }

//     public string ToCSV()
//     {
//         return KundenNr + ";" + ArtikelNr + ";" + Anzahl;
//     }

//     public static void ToCSV(List<Rechnung> rechnungen)
//     {
//         using (StreamWriter sw = new StreamWriter("RechnungTEST.csv"))
//         {
//             foreach (Rechnung rechnung in rechnungen)
//             {
//                 sw.WriteLine(rechnung.ToCSV());
//             }
//         }
//     }
// }

// internal class Program
// {
//     static void Main(string[] args)
//     {
//         List<Rechnung> rechnungen = new List<Rechnung>()
//         {
//             new Rechnung(111, 32323, 9),
//             new Rechnung(222, 45673, 7),
//             new Rechnung(333, 452323, 9),
//             new Rechnung(444, 24673, 7),
//         };

//         Rechnung.ToCSV(rechnungen);
//     }
// }