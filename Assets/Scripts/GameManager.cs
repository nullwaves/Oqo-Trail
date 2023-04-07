using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OqoDateTime GameTime;
    public float GameTimeSpeed = 10f;
    private bool _paused = false;
    public bool IsPaused { 
        get 
        { 
            return _paused;
        }
        set
        {
            _paused = value;
            PauseStateChange?.Invoke(_paused);
        }
    }

    private CharMove player;

    // Supplies
    public int Woqaz = 0;
    public int Clothing = 0;
    public int Ammo = 0;
    public int Food = 0;
    public int Axles = 0;
    public int Wheels = 0;
    public int Tongues = 0;

    private Rationing _rations;
    public Rationing Rations 
    {
        get 
        {
            return _rations; 
        } 
        set 
        { 
            _rations = value;
        }
    }
    private Pacing _pace;
    public Pacing Pace
    {
        get
        {
            return _pace;
        }
        set 
        { 
            _pace = value;
            player.playerSpeed = _pace.Speed;
        }
    }

    public void IncreasePace()
    {
        if(Pace == Pacing.Strenuous) Pace = Pacing.Grueling;
        if(Pace == Pacing.Steady) Pace = Pacing.Strenuous;
    }

    public void DecreasePace()
    {
        if (Pace == Pacing.Strenuous) Pace = Pacing.Steady;
        if (Pace == Pacing.Grueling) Pace = Pacing.Strenuous;
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<CharMove>();
        Rations = Rationing.Filling;
        Pace = Pacing.Steady;
        GameTime = new OqoDateTime(8 * OqoDateTime.MinutesPerHour);
        GameTime.HoursAdvanced += HoursAdvanced;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!IsPaused)
        {
            GameTime.AddSeconds(Time.deltaTime * GameTimeSpeed);
        }
    }

    private void HoursAdvanced(int hours)
    {
        Debug.LogError($"Time: {GameTime}");
    }

    public event BoolEventHandler PauseStateChange;
}

public delegate void BoolEventHandler(bool state);

public class Pacing
{
    public string Name;
    public float Speed;

    public static Pacing Steady = new Pacing() { Name = "Steady", Speed = 2 };
    public static Pacing Strenuous = new Pacing() { Name = "Strenuous", Speed = 4 };
    public static Pacing Grueling = new Pacing() { Name = "Grueling", Speed = 8 };
}

public class Rationing
{
    public string Name;
    public int Consumption;

    public static Rationing Filling = new Rationing() { Name = "Filling", Consumption = 3 };
    public static Rationing Meager = new Rationing() { Name = "Meager", Consumption = 2 };
    public static Rationing Barebones = new Rationing() { Name = "Bare-bones", Consumption = 1 };
}

public class PartyMember
{
    public string Name;
    public int Health;
    public int DaysSick;
    public int DaysStarved;
    public bool IsDead;

    public PartyMember(string name)
    {
        Name = name;
        Health = 100;
        DaysSick = 0;
        DaysStarved = 0;
    }

    public void ProgressByDay()
    {
        // Update health based on pace, sickness, and starvation
    }
}