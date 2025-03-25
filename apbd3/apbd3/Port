public static class Port
{
    public static void PrzeniesKontener(string numerKontenera, Statek z, Statek doStatku)
    {
        var kontener = z.PrzeniesKontener(numerKontenera);
        if (kontener != null)
        {
            doStatku.ZaladujKontener(kontener);
        }
    }

    public static void WypiszInformacjeOKontenerze(Kontener kontener)
    {
        Console.WriteLine($"Kontener: {kontener.GetName()}, Masa: {kontener.Masa}, Max: {kontener.MaxLadow}");
    }
}
