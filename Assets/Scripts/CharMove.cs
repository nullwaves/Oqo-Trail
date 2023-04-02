using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    private GameManager _manager;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
        controller = gameObject.AddComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerSpeed = _manager.Pace;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) Debug.LogWarning("Fire1 Pressed");
        if(Input.GetButtonDown("Fire2")) Debug.LogWarning("Fire2 Pressed");
        if(Input.GetButtonDown("Fire3")) Debug.LogWarning("Fire3 Pressed");
        if(Input.GetButtonDown("Pace"))
        {
            if(Input.GetAxis("Pace") < 0)
            {
                _manager.DecreasePace();
            }
            else 
            {
                _manager.IncreasePace();
            }
            playerSpeed = _manager.Pace;
        }
        if(!_manager.IsPaused)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
    }
}