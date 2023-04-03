using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Pacing : MonoBehaviour
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
        text.text = $"Pace: {manager.Pace}\r\nRations: {manager.Rations}";
    }
}