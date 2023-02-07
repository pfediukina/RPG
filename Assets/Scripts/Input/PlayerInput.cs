using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Player _player;

    void Start() 
    {
        InitActions();
        InitPlayer();
    }

    void Update() 
    {
        if(_playerActions != null && _player != null) {
            Vector2 mousePos = _playerActions.Mouse.MousePosition.ReadValue<Vector2>();
            _player.MoveCamera(mousePos);
        }
    }

    private void InitPlayer()
    {
        _player = GetComponent<Player>();
        if(_player == null) {
            Debug.LogError("PlayerActions: _player is null");
        }
    }

    private void InitActions()
    {
        _playerActions = new PlayerActions();
        _playerActions.Enable();

        InitKeys();
        InitMouse();
    }

    private void InitKeys() 
    {
        _playerActions.Keys.PermanentAttachCamera.performed += context => _player.PermanentAttachCameraSwitch();
        _playerActions.Keys.TempAttachCamera.started += context => _player.AttachCamera(true);
        _playerActions.Keys.TempAttachCamera.canceled += context => _player.AttachCamera(false);

        _playerActions.Keys.Menu.performed += context => 
        {
            _player.inMenu ^= true;
            _player.PlayerUI.SwitchMainMenu(_player.inMenu);
        };
    }

    private void InitMouse() 
    {
        _playerActions.Mouse.RightClick.performed += context => _player.PlayerClicked(_playerActions.Mouse.MousePosition.ReadValue<Vector2>());
    }
}
