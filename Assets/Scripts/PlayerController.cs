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
    private Rigidbody2D _playerRigidbody;

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
    public float ShrinkSpeed;
    public ParticleSystem SnowTrailParticle;
    public ParticleSystem SnowExplosionParticle;
    public Vector2 StartPosition;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

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
            Enlarge();
            if (PlayerState != PlayerState.GROUND)
            {
                Land();
                PlayerState = PlayerState.GROUND;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (new Vector3(0, -transform.localScale.y/2) / 2), transform.localScale.y/2);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -transform.localScale.y/4) / 2, transform.localScale.y / 2, CollisionMask);
    }

    private bool InputCheck()
    {
#if false //UNITY_STANDALONE || UNITY_EDITOR
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
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
        PlayerState = PlayerState.GROUND;
        GameController.instance.GameStarted = true;
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        UIController.instance.FadeOut();
        SnowTrailParticle.Play();
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
        if (_playerRigidbody.velocity.x > 0)
            transform.localScale *= 1f + ShrinkSpeed * Time.deltaTime;
    }

    private void Shrink()
    {
        //snow particle effect
        //sound effect
        // if size > MIN -> size--
        
        SnowExplosionParticle.Play();
        var x = 0.05f;
        if (transform.localScale.y > 1 + x)
            transform.localScale -= new Vector3(x,x,0);
    }

    private void ResetSize()
    {
        // if hit bird -> ResetSize()
        transform.localScale = Vector3.one;
    }

    private void Fly()
    {
        //var flyTime = x
        //add Y force; flyTime--
    }

    private void Land()
    {
        
        //snow burst particle effect
        if (transform.localScale.x > 1.5)
        {
            SnowExplosionParticle.Play();
            Camera.main.GetComponent<CameraFollow>().Shake = 0.2f;
            if (_playerRigidbody.velocity.y > -10)
            {
                //Die();
            }
        }

        //sound effect
        //screen shake
        // if size > threshold -> Die()
        // if prev state == AIR -> reset sprite
    }

    private void Die()
    {
        //fragment shader
        //particle effect
        SnowExplosionParticle.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        SnowTrailParticle.Stop();
        GetComponent<AudioSource>().Play();
        //screen shake
        Camera.main.GetComponent<CameraFollow>().Shake = 0.5f;

        UIController.instance.SetText("Tap to restart");
        UIController.instance.FadeIn();
        _playerAction = RestartPlayer;
    }

    private void RestartPlayer()
    {
        StartPlayer();
        ResetSize();
        gameObject.transform.position = StartPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.transform.tag)
        {
            case "Bird":
                break;
            case "Obstacle":
                Die();
                break;
        }
    }
}