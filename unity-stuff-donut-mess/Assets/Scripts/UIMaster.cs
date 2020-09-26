using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIMaster : MonoBehaviour
{

    private InputMaster _inputMaster;
    public GameObject pauseFrame;
    public PlayerMovement playerMovementScript;
    private bool state;

    private void Awake() {
        state = false;
        _inputMaster = new InputMaster();
        _inputMaster.Player.Pause.performed += ctx => Pause();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    public void Pause() {
        pauseFrame.SetActive(!state);
        state = !state;
        playerMovementScript.pause = state;
    }

    public void Quit ()
    {
        Application.Quit();
    }

    private void OnEnable() {
        _inputMaster.Enable();
    }

    private void OnDisable() {
        _inputMaster.Disable();
    }
    
}
