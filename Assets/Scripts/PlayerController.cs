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
        get { return _playerState; }
        set
        {
            _playerState = value;
            ActionChange(value);
        }
    }
    public LayerMask CollisionMask;
    public Transform GroundCheck;
    public float CollisionCircleRadius;
    public float JumpForce;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Debug.Log(IsGrounded());
        if (IsGrounded())
        {
            if (PlayerState != PlayerState.GROUND)
            {
                Land();
                PlayerState = PlayerState.GROUND;
            }
            else
            {
                Enlarge();
            }
        }
        else
        {
            if (PlayerState != PlayerState.AIR)
            {
                PlayerState = PlayerState.AIR;
            }
        }

        if (InputCheck())
        {
            _playerAction();
        }
    }

    private bool IsGrounded()
    {
        //return (Physics.Raycast(transform.position, -Vector2.up, 1f));
        return Physics2D.OverlapCircle(GroundCheck.position, 0.8f, CollisionMask);
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
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce, ForceMode2D.Force);
    }

    private void Enlarge()
    {
        //every tick
        //if size < MAX -> size++
    }

    private void Shrink()
    {
        //snow particle effect
        //sound effect
        // if size > MIN -> size--
    }

    private void Fly()
    {
        //var flyTime = x
        //add Y force; flyTime--
    }

    private void Land()
    {
        //snow burst particle effect
        //sound effect
        //screen shake
        // if size > threshold -> destroy 
    }
}