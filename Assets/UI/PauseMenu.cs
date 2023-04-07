using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.UI
{
    public class PauseMenu : MonoBehaviour
    {
        GameManager _manager;
        VisualElement _root;
        Dictionary<bool, DisplayStyle> stateMap = new Dictionary<bool, DisplayStyle>
        {
            { true, DisplayStyle.Flex },
            { false, DisplayStyle.None },
        };

        // Use this for initialization
        void Start()
        {
            _manager = FindObjectOfType<GameManager>();
            PauseStateChanged(false);
            _manager.PauseStateChange += PauseStateChanged;
        }

        public void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            Button btnResume = _root.Q<Button>("btnResume");

            btnResume.clicked += ButtonResume_Click;
            _root.Q<Button>("btnSteady").clicked += ButtonSteady_Click;
            _root.Q<Button>("btnStrenuous").clicked += ButtonStrenuous_Click;
            _root.Q<Button>("btnGrueling").clicked += ButtonGrueling_Click;
            _root.Q<Button>("btnFilling").clicked += ButtonFilling_Click;
            _root.Q<Button>("btnMeager").clicked += ButtonMeager_Click;
            _root.Q<Button>("btnBarebones").clicked += ButtonBarebones_Click;
        }

        public void ButtonResume_Click()
        {
            _manager.IsPaused = false;
        }

        public void ButtonSteady_Click()
        {
            _manager.Pace = Pacing.Steady;
            _manager.IsPaused = false;
        }
        public void ButtonStrenuous_Click()
        {
            _manager.Pace = Pacing.Strenuous;
            _manager.IsPaused = false;
        }
        public void ButtonGrueling_Click()
        {
            _manager.Pace = Pacing.Grueling;
            _manager.IsPaused = false;
        }
        public void ButtonFilling_Click()
        {
            _manager.Rations = Rationing.Filling;
            _manager.IsPaused = false;
        }
        public void ButtonMeager_Click()
        {
            _manager.Rations = Rationing.Meager;
            _manager.IsPaused = false;
        }
        public void ButtonBarebones_Click()
        {
            _manager.Rations = Rationing.Barebones;
            _manager.IsPaused = false;
        }
        public void PauseStateChanged(bool state)
        {
            _root.style.display = stateMap[state];
        }
    }
}