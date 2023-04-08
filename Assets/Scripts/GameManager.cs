using System;
using System.Collections.Generic;
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

    public List<PartyMember> Party;

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
        GameTime.DaysAdvanced += DaysAdvanced;
        Party = new List<PartyMember>()
        {
            new PartyMember("Arkas"),
            new PartyMember("Guude"),
            new PartyMember("Chad"),
            new PartyMember("Drooo"),
            new PartyMember("WormJess")
        };
    }

    // Update is called once per frame
    private void Update()
    {
        if(!IsPaused)
        {
            GameTime.AddSeconds(Time.deltaTime * GameTimeSpeed);
        }
    }

    private void DaysAdvanced(int days)
    {
        for(int i = 0; i < days; i++)
        {
            foreach(var member in Party)
            {
                if(!member.IsDead)
                {
                    member.ProgressByDay(this);
                }
            }
        }
    }

    public event BoolEventHandler PauseStateChange;
}

public delegate void BoolEventHandler(bool state);

public class Pacing
{
    public string Name;
    public float Speed;
    public int HealthDamage;

    public static Pacing Steady = new Pacing() { Name = "Steady", Speed = 2, HealthDamage = 0 };
    public static Pacing Strenuous = new Pacing() { Name = "Strenuous", Speed = 4, HealthDamage = 1 };
    public static Pacing Grueling = new Pacing() { Name = "Grueling", Speed = 8, HealthDamage = 2 };

    public override string ToString()
    {
        return Name;
    }
}

public class Rationing
{
    public string Name;
    public int Consumption;
    public int HealthDamage;
    public float IllnessChance;

    public static Rationing Filling = new Rationing() { Name = "Filling", Consumption = 3, HealthDamage = 0, IllnessChance = 0f };
    public static Rationing Meager = new Rationing() { Name = "Meager", Consumption = 2, HealthDamage = 1, IllnessChance = 0.05f};
    public static Rationing Barebones = new Rationing() { Name = "Bare-bones", Consumption = 1, HealthDamage = 2, IllnessChance = 0.1f };

    public override string ToString()
    {
        return Name;
    }
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
        IsDead = false;
    }

    public void ProgressByDay(GameManager manager, bool resting = false)
    {
        if(manager.Food < 1)
        {
            DaysStarved++;
            Debug.LogWarning($"{Name} is starving!");
        }
        else
        {
            DaysStarved = 0;
            manager.Food -= manager.Rations.Consumption;
            if (manager.Food < 0) manager.Food = 0;
        }
        Health -= manager.Rations.HealthDamage + DaysStarved * 10;
        if (!resting)
        {
            Health -= manager.Pace.HealthDamage;
            if(DaysSick > 0 || DaysStarved > 0)
            {
                DaysSick++;
            }
            else if (UnityEngine.Random.value < manager.Rations.IllnessChance + (Health / 400))
            {
                DaysSick++;
                Debug.LogWarning($"{Name} has dysentery!");
            }
        }
        else // Resting
        {
            DaysSick = DaysSick > 0 ? DaysSick - 1:0; // Reduce Illness
        }
        if(Health <= 0)
        {
            IsDead = true;
            Debug.LogWarning($"{Name} has died!");
        }
    }

    public override string ToString()
    {
        return Name;
    }
}