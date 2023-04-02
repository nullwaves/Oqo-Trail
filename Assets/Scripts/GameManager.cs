using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OqoDateTime GameTime;
    public float GameTimeSpeed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        GameTime = new OqoDateTime(8 * OqoDateTime.MinutesPerHour);
    }

    // Update is called once per frame
    private void Update()
    {
        GameTime.AddSeconds(Time.deltaTime * GameTimeSpeed);
    }
}