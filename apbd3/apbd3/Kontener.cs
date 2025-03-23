namespace apbd3;

public interface IHazardNotifier
{
    void SendMessage(string message);
}

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) {}
}

public abstract class Kontener
{
    public double Masa { get; protected set; }
    public double Wysokosc { get; protected set; }
    public double Waga { get; protected set; }
    public double Glebokosc { get; protected set; }
    public double MaxLadow { get; protected set; }
    protected string Numer { get; set; }
    protected static int counter = 1;
    
    public Kontener(double maxLadow, string type)
    {
        MaxLadow = maxLadow;
        Numer = $"KON-{type}-{counter++}";
    }

    public string GetName()
    {
        return Numer;
    }

    public virtual void OproznienieLadunku()
    {
        Masa = 0;
    }

    public virtual void ZaladowanieLadunku(double masaLadunku)
    {
        if (masaLadunku + Masa > MaxLadow)
        {
            throw new OverfillException("Przeładowano kontener");
        }
        Masa += masaLadunku;
    }
}

public class KontenerL : Kontener, IHazardNotifier
{
    private bool Niebiezpieczny;
    
    public KontenerL(double maxLadow, bool niebiezpieczny) : base(maxLadow, "L")
    {
        Niebiezpieczny = niebiezpieczny;
    }

    public void SendMessage(string message)
    {
        Console.WriteLine(Numer + ": " + message);
    }

    public override void ZaladowanieLadunku(double masaLadunku)
    {
        double maxAllowed = Niebiezpieczny ? MaxLadow * 0.5 : MaxLadow * 0.9;
        if (masaLadunku + Masa > maxAllowed)
        {
            SendMessage("Próba wykonania niebezpiecznej sytuacji");
            throw new OverfillException("Przeładowano kontener");
        }
        Masa += masaLadunku;
    }
}

public class KontenerG : Kontener, IHazardNotifier
{
    public double Cisnienie { get; private set; }
    
    public KontenerG(double maxLadow, double cisnienie) : base(maxLadow, "G")
    {
        Cisnienie = cisnienie;
    }

    public void SendMessage(string message)
    {
        Console.WriteLine(Numer + ": " + message);
    }

    public override void OproznienieLadunku()
    {
        Masa *= 0.05;
    }
}

public class KontenerC : Kontener
{
    public double Temperatura { get; private set; }
    public string TypProduktu { get; private set; }
    
    public KontenerC(double maxLadow, double temperatura, string typProduktu) : base(maxLadow, "C")
    {
        Temperatura = temperatura;
        TypProduktu = typProduktu;
    }
    
    public void ZaladowanieLadunku(double masaLadunku, double temperaturaLadunku)
    {
        if (masaLadunku + Masa > MaxLadow)
        {
            throw new OverfillException("Przeładowanie kontenera chłodniczego");
        }
        if (temperaturaLadunku < Temperatura)
        {
            throw new Exception("Temperatura ładunku jest zbyt niska dla tego kontenera");
        }
        Masa += masaLadunku;
    }
}