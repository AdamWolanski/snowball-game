using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { GROUND, AIR, FLY };
public enum InputType { TOUCH, KEYBOARD };

public class PlayerController : MonoBehaviour
{
    private delegate void Action();
    private Action _playerAction;
    private InputType _playerInputType;
    private PlayerState _playerState;
    public PlayerState PlayerState
    {
        set
        {
            _playerState = value;
            ActionChange(value);
        }
    }


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (GroundCheck())
        {

        }

        if (InputCheck())
        {
            _playerAction();
        }
    }

    private bool GroundCheck()
    {
        return false;
    }

    private bool InputCheck()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
#endif
            return true;
        }

        return false;
    }

    private void Initialize()
    {
        PlayerState = PlayerState.GROUND;
#if UNITY_STANDALONE || UNITY_EDITOR
        _playerInputType = InputType.KEYBOARD;
#else
        _playerInputType = InputType.TOUCH;
#endif
    }

    private void ActionChange(PlayerState action)
    {
        switch (action)
        {
            case PlayerState.GROUND:
                _playerAction = Jump;
                break;
            case PlayerState.AIR:
                _playerAction = Shrink;
                break;
            case PlayerState.FLY:
                _playerAction = Fly;
                break;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump!");
    }

    private void Shrink()
    {
        Debug.Log("Shrink!");
    }

    private void Fly()
    {
        Debug.Log("Lets fly!");
    }
}