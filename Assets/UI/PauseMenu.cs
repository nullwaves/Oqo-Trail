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
        }

        public void ButtonResume_Click()
        {
            _manager.IsPaused = false;
        }

        public void PauseStateChanged(bool state)
        {
            _root.style.display = stateMap[state];
        }
    }
}