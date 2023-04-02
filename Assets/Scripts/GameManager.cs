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
    public Pacing Pace = Pacing.Steady;

    public enum Rationing
    {
        Barebones = 1,
        Meager = 2,
        Filling = 3,
    }

    public enum Pacing
    {
        Steady = 2,
        Strenuous = 4,
        Grueling = 8,
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