using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Ground, Air, Fly };

public class PlayerController : MonoBehaviour {
    public delegate void Action();
    public static event Action OnAction;

    private PlayerState currentState = PlayerState.Ground;
    private PlayerState prevState = PlayerState.Ground;

    public float GroundDist;
    public float SpriteOffset;

    private void Start()
    {
        OnAction += Jump;
    }

    private void Update()
    {
        // if ground check == false -> OnAction += Air else OnAction += Ground
        // if hit bird -> OnAction -> Fly

        if ( StateCheck() )
        {
            prevState = currentState;
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space))
        {
#else
        if (TouchController.Tapped())
        {
#endif
            OnAction();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentState++;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, GroundDist + SpriteOffset);
    }

    private void StateChange(PlayerState newState)
    {
        OnAction -= Jump;
        OnAction -= Shrink;
        OnAction -= Fly;

        switch (currentState)
        {
            case PlayerState.Ground:
                OnAction += Jump;
                break;
            case PlayerState.Air:
                OnAction += Shrink;
                break;
            case PlayerState.Fly:
                OnAction += Fly;
                break;
        }
    }

    private bool StateCheck()
    {
        return (currentState == prevState);
    }

    private void Jump()
    {
        Debug.Log("jump func");
    }

    private void Shrink()
    {
        Debug.Log("shrink func");
    }

    private void Fly()
    {
        Debug.Log("fly func");
    }
}
