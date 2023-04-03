using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Clock : MonoBehaviour
{
    private GameManager manager;
    private TMP_Text text;

    // Start is called before the first frame update
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = $"{string.Format("{0:00}", manager.GameTime.Hour)}:00 {string.Format("{0:00}", manager.GameTime.Day + 1)}/{string.Format("{0:00}", manager.GameTime.Month + 1)}/{string.Format("{0:00}", manager.GameTime.Year + 1)}";
    }
}