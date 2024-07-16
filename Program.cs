class Program
{
    static void Main(string[] args)
    {
        Spore.Clear();
        for(int i=0;i<CardType.Cards.Length;i++)
        {
            string cardType = CardType.Cards[i];
            Spore.Color(
                $"<Reset>┌{cardType}<Red>♥<Reset>{(cardType.Length == 1 ? '─' : "")}──┐  "
                +
                $"<Reset>┌{cardType}<White>♣<Reset>{(cardType.Length == 1 ? '─' : "")}──┐  "
                +
                $"<Reset>┌{cardType}<Red>♦<Reset>{(cardType.Length == 1 ? '─' : "")}──┐  "
                +
                $"<Reset>┌{cardType}<White>♠<Reset>{(cardType.Length == 1 ? '─' : "")}──┐\n"
            );
            if(i == CardType.Cards.Length-1)
            {
                Spore.Color(
                    "│     │  │     │  │     │  │     │ \n"+
                    "│     │  │     │  │     │  │     │ \n"+
                    "│     │  │     │  │     │  │     │ \n"+
                    $"└──{(cardType.Length == 1 ? '─' : "")}<Red>♥<Reset>{cardType}┘  "+
                    $"└──{(cardType.Length == 1 ? '─' : "")}<White>♣<Reset>{cardType}┘  "+
                    $"└──{(cardType.Length == 1 ? '─' : "")}<Red>♦<Reset>{cardType}┘  "+
                    $"└──{(cardType.Length == 1 ? '─' : "")}<White>♠<Reset>{cardType}┘\n"
                );
            }
        }
    }
}

public static class CardType
{
    public static readonly string Ace = "A";
    public static readonly string Two = "2";
    public static readonly string Three = "3";
    public static readonly string Four = "4";
    public static readonly string Five = "5";
    public static readonly string Six = "6";
    public static readonly string Seven = "7";
    public static readonly string Eight = "8";
    public static readonly string Nine = "9";
    public static readonly string Ten = "10";
    public static readonly string Jack = "J";
    public static readonly string Queen = "Q";
    public static readonly string King = "K";
    public static readonly string[] Cards = new string[]{
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    };
}

// ┌─┬┐
// │ ││
// ├─┼┤
// └─┴┘
// ♥
// ♦
// ♣
// ♠