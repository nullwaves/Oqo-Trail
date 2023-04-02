using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OqoDateTime GameTime;
    public float GameTimeSpeed = 10f;
    public bool IsPaused = false;

    // Supplies
    public int Woqaz = 0;
    public int Clothing = 0;
    public int Ammo = 0;
    public int Food = 0;
    public int Axles = 0;
    public int Wheels = 0;
    public int Tongues = 0;

    public Rationing Rations = Rationing.Filling;
    public float Pace = Pacing.Steady;

    public enum Rationing
    {
        Barebones = 1,
        Meager = 2,
        Filling = 3,
    }

    public static class Pacing
    {
        public const float Steady = 2;
        public const float Strenuous = 4;
        public const float Grueling = 8;
    }

    public void IncreasePace()
    {
        switch(Pace)
        {
            case Pacing.Strenuous: Pace = Pacing.Grueling;
                break;
            case Pacing.Steady: Pace = Pacing.Strenuous;
                break;
            default:
                break;
        }
    }

    public void DecreasePace()
    {
        switch(Pace)
        {
            case Pacing.Strenuous: Pace = Pacing.Steady;
                break;
            case Pacing.Grueling: Pace = Pacing.Strenuous;
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
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