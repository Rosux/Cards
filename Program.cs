using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Magic Card Trick";
        Console.CursorVisible = false;
        List<Card> deck = new List<Card>();
        deck.Add(new Card(
            $"<Red>♥<Reset>A",
            [@"     ", @"  <Red>♥<Reset>  ", @"     "],
            SporeTraceColor.White
        ));
        for(int i=2;i<11;i++)
        {
            deck.Add(new Card(
                $"<Red>♥<Reset>{i}",
                [@"     ", @"  <Red>♥<Reset>  ", @"     "],
                SporeTraceColor.White
            ));
        }
        deck.Add(new Card(
            $"<Red>♥<Reset>J",
            [@"     ", @"  <Red>♥<Reset>  ", @"     "],
            SporeTraceColor.White
        ));
        deck.Add(new Card(
            $"<Red>♥<Reset>Q",
            [@"     ", @"  <Red>♥<Reset>  ", @"     "],
            SporeTraceColor.White
        ));
        deck.Add(new Card(
            $"<Red>♥<Reset>K",
            [@"     ", @"  <Red>♥<Reset>  ", @"     "],
            SporeTraceColor.White
        ));
        string savedName = "";
        int selectedCard = 0;
        while(true)
        {
            Spore.Clear();
            Spore.Color("Pick a card (using the Up/Down arrows and Enter to select):");
            for(int i=0;i<deck.Count;i++)
            {
                Card c = deck[i];
                if(i == selectedCard){c.RimColor = SporeTraceColor.Green;}else{c.RimColor = SporeTraceColor.White;}
                c.DisplayCard(0, i+1);
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            if(key == ConsoleKey.UpArrow){
                selectedCard--;
            }else if(key == ConsoleKey.DownArrow){
                selectedCard++;
            }else if(key == ConsoleKey.Enter){
                Spore.Clear();
                savedName = deck[selectedCard].Name;
                break;
            }
            selectedCard = Math.Clamp(selectedCard, 0, deck.Count-1);
        }
        Random rng = new Random();
        for(int i=0;i<5;i++)
        {
            Spore.Clear();
            Spore.Color("Shuffling the deck...");
            Shuffle(deck);
            for(int j=0;j<deck.Count;j++)
            {
                Card c = deck[j];
                c.RimColor = SporeTraceColor.White;
                c.DisplayCard(0, j+1);
            }
            Thread.Sleep(200);
        }
        for(int i=0;i<10;i++)
        {
            Spore.Clear();
            Spore.Color("Finding your card...");
            deck[rng.Next(deck.Count)].DisplayCard(0, 1);
            Thread.Sleep(200);
        }
        bool selection = false;
        while(true)
        {
            Spore.Clear();
            Spore.Color("Is this your card?");
            foreach(Card c in deck)
            {
                if(c.Name == savedName)
                {
                    c.DisplayCard(0, 1);
                    break;
                }
            }
            Spore.Color($"{(!selection ? "<Red>" : "<Reset>")}>No {(selection ? "<Green>" : "<Reset>")}>Yes");
            ConsoleKey key = Console.ReadKey(true).Key;
            if(key == ConsoleKey.LeftArrow){selection = false;}
            else if(key == ConsoleKey.RightArrow){selection = true;}
            else if(key == ConsoleKey.Enter){break;}
        }
        Spore.Clear();
        if(!selection)
        {
            Spore.Color("LIAR!!!");
        }else{
            Spore.Color("Thanks for playing!");
        }
        Console.ReadKey(true);
    }

    public static void Shuffle(List<Card> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while(n > 1){
            n--;
            int k = rng.Next(n + 1);
            Card value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public struct Card
{
    private static readonly string _colorSplitPattern = @"(<Black>|<DarkBlue>|<DarkGreen>|<DarkCyan>|<DarkRed>|<DarkMagenta>|<DarkYellow>|<Gray>|<DarkGray>|<Blue>|<Green>|<Cyan>|<Red>|<Magenta>|<Yellow>|<White>|<Previous>|<Reset>)";
    public string Name = "a";
    public SporeTraceColor RimColor = SporeTraceColor.White;
    public string[] Art = [
        "     ",
        "     ",
        "     "
    ];

    public Card(){}
    public Card(string name, string[] art, SporeTraceColor rimColor=SporeTraceColor.White)
    {
        if(Regex.Replace(name, _colorSplitPattern, "").Length > 5){throw new Exception("Name parameter must be between 0-5 characters long excluding colors.");}
        if(art.Length != 3 || Regex.Replace(art[0], _colorSplitPattern, "").Length != 5 || Regex.Replace(art[1], _colorSplitPattern, "").Length != 5 || Regex.Replace(art[2], _colorSplitPattern, "").Length != 5){throw new Exception("Art parameter must be 3 strings of 5 chars long");}
        Name = name;
        Art = art;
        RimColor = rimColor;
    }

    public void DisplayCard(int x=0, int y=0)
    {
        PrintColoredText(
            $"<{RimColor}>┌<Reset>{Format(Name, 5, '─')}<{RimColor}>┐<Reset>\n"+
            $"<{RimColor}>│<Reset>{Center(Art[0], 5, ' ')}<{RimColor}>│<Reset>\n"+
            $"<{RimColor}>│<Reset>{Center(Art[1], 5, ' ')}<{RimColor}>│<Reset>\n"+
            $"<{RimColor}>│<Reset>{Center(Art[2], 5, ' ')}<{RimColor}>│<Reset>\n"+
            $"<{RimColor}>└<Reset>{ReverseFormat(Name, 5, '─')}<{RimColor}>┘<Reset>\n",
            x,
            y
        );
    }

    private void PrintColoredText(string text, int startX, int startY)
    {
        int currentY = startY;
        foreach(string line in text.Split('\n'))
        {
            Console.SetCursorPosition(startX, currentY);
            Spore.Color(line);
            currentY++;
        }
    }

    private string Format(string data, int totalWidth, char d=' ')
    {
        return $"{data}<{RimColor}>{new string(d, Math.Max(0, totalWidth-Regex.Replace(data, _colorSplitPattern, "").Length))}<Reset>";
    }
    private string ReverseFormat(string data, int totalWidth, char d=' ')
    {
        return $"<{RimColor}>{new string(d, Math.Max(0, totalWidth-Regex.Replace(data, _colorSplitPattern, "").Length))}<Reset>{data}";
    }
    private string Center(string data, int totalWidth, char filler=' ')
    {
        string m = data;
        for(int i=Math.Max(0, totalWidth-Regex.Replace(data, _colorSplitPattern, "").Length);i>0;i--){
            m = ((i % 2 == 1) ? filler : "") + m + ((i % 2 == 0) ? filler : "");
        }
        return m;
    }
}

// ┌─┐
// │ │
// └─┘

// ♥
// ♦
// ♣
// ♠

// ┌Q♠───┐
// │     │
// │     │
// │     │
// └───♥Q┘

// ┌♥1───┐
// ┌♥2───┐
// ┌♥3───┐
// ┌♥4───┐
// ┌♥5───┐
// ┌♥6───┐
// │     │
// │  ♥  │
// │     │
// └───♥6┘
