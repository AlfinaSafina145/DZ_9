using System;
using System.Collections.Generic;
interface IGame
{
    string GetName();
    void Play(string teamName);
}
class Beach : IGame
{
    public string GetName()
    {
        return "Пляж";
    }

    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Пляж»");
    }
}
class Mousetrap : IGame
{
    public string GetName()
    {
        return "Мышеловка";
    }
    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Мышеловка»");
    }
}
class Sea : IGame
{
    public string GetName()
    {
        return "Море";
    }
    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Море»");
    }
}

class Fishing : IGame
{
    public string GetName()
    {
        return "Рыбалка";
    }
    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Рыбалка»");
    }
}

class Postmen : IGame
{
    public string GetName()
    {
        return "Почтальоны";
    }
    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Почтальоны»");
    }
}

class Slide : IGame
{
    public string GetName()
    {
        return "Горка";
    }

    public void Play(string teamName)
    {
        Console.WriteLine("Команда " + teamName + " играет в игру «Горка»");
    }
}
class GameWithExtra : IGame
{
    private IGame game;
    private string extraName;

    public GameWithExtra(IGame game, string extraName)
    {
        this.game = game;
        this.extraName = extraName;
    }

    public string GetName()
    {
        return game.GetName() + " + " + extraName;
    }

    public void Play(string teamName)
    {
        game.Play(teamName);
        Console.WriteLine("Команда " + teamName + " выполняет доп. этап: " + extraName);
    }
}
class Organizer
{
    private static Organizer instance;
    private string name;

    private Organizer(string name)
    {
        this.name = name;
    }

    public static Organizer GetInstance(string name)
    {
        if (instance == null)
        {
            instance = new Organizer(name);
        }
        return instance;
    }

    public string GetName()
    {
        return name;
    }

    public void AddGame(BigRaces races, IGame game)
    {
        races.AddGame(game);
    }
}
class Team
{
    private string name;
    private List<string> members;

    public Team(string name)
    {
        this.name = name;
        members = new List<string>();
        for (int i = 1; i <= 15; i++)
        {
            members.Add("Участник " + i);
        }
    }

    public string GetName()
    {
        return name;
    }

    public int GetMemberCount()
    {
        return members.Count;
    }
}
class BigRaces
{
    private List<IGame> games;
    private Random random;

    public BigRaces()
    {
        games = new List<IGame>();
        random = new Random();
    }

    public void AddGame(IGame game)
    {
        if (game == null)
        {
            return;
        }
        games.Add(game);
    }
    public void ReplaceGame(int index, IGame game)
    {
        if (game == null)
        {
            return;
        }
        if (index >= 0 && index < games.Count)
        {
            games[index] = game;
        }
    }
    private List<IGame> Shuffle()
    {
        List<IGame> shuffled = new List<IGame>(games);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            IGame tmp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = tmp;
        }
        return shuffled;
    }

    public void Start(List<Team> teams)
    {
        Console.WriteLine("Главный организатор: " + Organizer.GetInstance("").GetName());
        Console.WriteLine("=== НАЧАЛО СОРЕВНОВАНИЙ ===");
        Console.WriteLine();

        foreach (Team team in teams)
        {
            Console.WriteLine("Команда «" + team.GetName() + "», участников: " + team.GetMemberCount());

            List<IGame> shuffled = Shuffle();
            for (int i = 0; i < 6 && i < shuffled.Count; i++)
            {
                shuffled[i].Play(team.GetName());
            }
            Console.WriteLine();
        }
    }
}
class Program
{
    static void Main()
    {
        List<Team> teams = new List<Team>();
        teams.Add(new Team("Россия"));
        teams.Add(new Team("Франция"));
        teams.Add(new Team("Китай"));
        teams.Add(new Team("Казахстан"));

        BigRaces races = new BigRaces();

        Organizer ganja = Organizer.GetInstance("Оливье Ганья");

        ganja.AddGame(races, new Beach());
        ganja.AddGame(races, new Mousetrap());
        ganja.AddGame(races, new Sea());
        ganja.AddGame(races, new Fishing());
        ganja.AddGame(races, new Postmen());
        ganja.AddGame(races, new Slide());

        ganja.AddGame(races, new GameWithExtra(new Beach(), "Бег в надувных кругах"));

        races.Start(teams);

        Console.ReadLine();
    }
}