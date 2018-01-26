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
        if (InputCheck())
        {
            _playerAction();
        }

        if (!GameController.instance.GameStarted) return;

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
    }

    private bool IsGrounded()
    {
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
        _playerAction = StartPlayer;
    }

    private void StartPlayer()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
        PlayerState = PlayerState.GROUND;
        GameController.instance.GameStarted = true;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        UIController.instance.FadeOut();
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

    private void ResetSize()
    {
        // if hit bird -> ResetSize()
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
        // if size > threshold -> Die()
        // if prev state == AIR -> reset sprite
    }

    private void Die()
    {
        //fragment shader
        //particle effect
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bird")
        {
            ResetSize();
            //change sprite
        }
    }
}