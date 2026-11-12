using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

List<videojatek> JatekLista = [];

const string FilePath = "..\\..\\..\\data\\videogames.csv";
using StreamReader sr = new(FilePath);
_ = sr.ReadLine();
while (!sr.EndOfStream)
{
    var reszek = sr.ReadLine().Split(',');
    JatekLista.Add(new()
    {
        Cim = reszek[0],
        Mufaj = reszek[1],
        KiadasDatum = DateTime.Parse(reszek[2]),
        AktivJatekosok = int.Parse(reszek[3]),
        Ertekeles = double.Parse(reszek[4].Replace('.', ','))
    });
}

foreach (var item in JatekLista)
{
    Console.WriteLine(item);
}

var nagyobbmint = JatekLista.Count(p => p.Ertekeles > 4.5);
Console.WriteLine($"1. Ennyi játék értékelése jobb mint 4.5: {nagyobbmint} db");

var legtobbJatekos = JatekLista.MaxBy(p => p.AktivJatekosok);
Console.WriteLine($"2. Legtöbb játékos: {legtobbJatekos.Cim} : {legtobbJatekos.AktivJatekosok}");

var elott = JatekLista.Any(x => x.KiadasDatum.Year < 2020);
Console.WriteLine($"3. Van-e 2020 előtt kiadott játék?: {(elott ? "Igen" : "Nem")}");

var atlagErtekeles = JatekLista.Average(x => x.Ertekeles);
Console.WriteLine($"4. Játékok értékelésének átlaga: {atlagErtekeles:0.0}");

var cim = JatekLista.Any(x => x.Cim == "Cyber City 2077");
Console.WriteLine($"5. Van-e Cyber City 2077 nevű játék?: {(cim ?  "igen" : "nem")} ");


Console.WriteLine("6. RPG játékok:");
var rpgjatek = JatekLista.Where(x => x.Mufaj == "RPG").OrderByDescending(x => x.KiadasDatum);
foreach (var r in rpgjatek)
{
    Console.WriteLine($"\t{r.Cim} : {r.KiadasDatum}");
}

Console.WriteLine($"7. A 3 legjobb értékelésű játék:");
var legjobbHarom = JatekLista.OrderByDescending(x => x.Ertekeles).Take(3);
foreach (var l in legjobbHarom)
{
    Console.WriteLine($"\t{l.Cim} : {l.Ertekeles:0.0}");
}

var avg = JatekLista.Where(x => x.KiadasDatum.Year > 2022).Average(x => x.Ertekeles);
Console.WriteLine($"8. 2022 után kiadott játékok átlagos értékelése: {avg:0.0}");


var rpgJatekok = JatekLista.GroupBy(x => x.Mufaj);
Console.WriteLine("9. Játékok csoportosítása műfaj szerint: ");

foreach (var rpg in rpgJatekok)
{
    Console.Write(($"\t{rpg.Key} ({rpg.Count()} db)\n"));
}
Console.WriteLine("10. Legjobb értékelések műfajonként:");
var mufajok = JatekLista.GroupBy(x => x.Mufaj);
foreach (var csak in mufajok)
{
    var legjobb = csak.MaxBy(x => x.Ertekeles);
    Console.WriteLine($"\t{legjobb.Mufaj} : {legjobb.Ertekeles:0.0}");
}