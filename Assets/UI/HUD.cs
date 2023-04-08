using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private GameManager _manager;
    private VisualElement _root;

    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
    }

    public void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        var time = _manager.GameTime;
        _root.Q<Label>("lblTime").text = $"Time: {time.Hour}:00 Month {time.Month}, Day {time.Day}";
        _root.Q<Label>("lblPace").text = $"Pace: {_manager.Pace}";
        _root.Q<Label>("lblRations").text = $"Rations: {_manager.Rations}";
        for(int i = 0; i < 5; i++)
        {
            var member = _manager.Party[i];
            var text = $"{member}: {member.Health}% Sick:{member.DaysSick > 0} Starving:{member.DaysStarved > 0}";
            _root.Q<Label>($"lblParty{i+1}").text = text;
        }
    }
}
