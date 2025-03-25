public class Kontenerowiec
{
    public string Nazwa { get; private set; }
    public double MaksPredkosc { get; private set; } // węzły
    public int MaksLiczbaKontenerow { get; private set; }
    public double MaksWagaLadunku { get; private set; } // tony

    private List<Kontener> kontenery = new();

    public Kontenerowiec(string nazwa, double maksPredkosc, int maksLiczbaKontenerow, double maksWagaLadunku)
    {
        Nazwa = nazwa;
        MaksPredkosc = maksPredkosc;
        MaksLiczbaKontenerow = maksLiczbaKontenerow;
        MaksWagaLadunku = maksWagaLadunku;
    }

    public void ZaladujKontener(Kontener kontener)
    {
        if (kontenery.Count >= MaksLiczbaKontenerow)
            throw new Exception("Przekroczono maksymalną liczbę kontenerów");

        double aktualnaWaga = kontenery.Sum(k => k.Masa);
        if (aktualnaWaga + kontener.Masa > MaksWagaLadunku)
            throw new Exception("Przekroczono maksymalną wagę kontenerów na statku");

        kontenery.Add(kontener);
    }

    public void ZaladujKontenery(List<Kontener> lista)
    {
        foreach (var k in lista)
        {
            ZaladujKontener(k);
        }
    }

    public void UsunKontener(string numer)
    {
        var kontener = kontenery.FirstOrDefault(k => k.GetName() == numer);
        if (kontener != null)
        {
            kontenery.Remove(kontener);
        }
    }

    public void RozladujKontener(string numer)
    {
        var kontener = kontenery.FirstOrDefault(k => k.GetName() == numer);
        kontener?.OproznienieLadunku();
    }

    public void ZastapKontener(string numer, Kontener nowyKontener)
    {
        int index = kontenery.FindIndex(k => k.GetName() == numer);
        if (index >= 0)
        {
            kontenery[index] = nowyKontener;
        }
    }

    public Kontener PrzeniesKontener(string numer)
    {
        var kontener = kontenery.FirstOrDefault(k => k.GetName() == numer);
        if (kontener != null)
        {
            kontenery.Remove(kontener);
        }
        return kontener;
    }

    public void WypiszInformacje()
    {
        Console.WriteLine($"Statek: {Nazwa}");
        Console.WriteLine($" - Maks. prędkość: {MaksPredkosc} węzłów");
        Console.WriteLine($" - Maks. liczba kontenerów: {MaksLiczbaKontenerow}");
        Console.WriteLine($" - Maks. ładowność: {MaksWagaLadunku} ton");
        Console.WriteLine($" - Aktualna liczba kontenerów: {kontenery.Count}");
        Console.WriteLine($" - Aktualna masa ładunku: {kontenery.Sum(k => k.Masa)} ton");

        foreach (var k in kontenery)
        {
            Console.WriteLine($"   > {k.GetName()} | Masa: {k.Masa}/{k.MaxLadow}");
        }
    }

    public Kontener ZnajdzKontener(string numer)
    {
        return kontenery.FirstOrDefault(k => k.GetName() == numer);
    }
}
