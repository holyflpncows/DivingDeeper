using System;
using System.Linq;
using Parts;
using Unity.VisualScripting;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 _moveDirection;
    private bool _facingRight = true;
    private bool _amAlive = true;
    private SpriteRenderer _submarineSprite;
    private static AudioSource _deadSoundFx;
    private static AudioSource _ambianceFx;

    public delegate void HitThingEvent();
    public delegate void WinEvent();

    public static event HitThingEvent YouAreDead;
    public static event WinEvent YouWin;

    private void Start()
    {
        _ambianceFx = GameObject.Find("ambiance").GetComponent<AudioSource>();
        _deadSoundFx = GameObject.Find("Dead").GetComponent<AudioSource>();
        _submarineSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckIfAlive();
        ProcessInputs();
        // _submarineSprite.flipX = _facingRight;
    }

    private void CheckIfAlive()
    {
        if (!_amAlive || Submarine.Instance.health > 0) return;
        _amAlive = false;
        Died();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(
            _moveDirection.x * moveSpeed*(1/(Submarine.Instance.GetDrag+1)),
            _moveDirection.y * moveSpeed*(1/(Submarine.Instance.GetDrag+1)));
        _facingRight = _moveDirection.x > 0 && _moveDirection.x != 0;
    }
    
    private static void Died()
    {
        Debug.Log("died");
        _deadSoundFx.Play();
        YouAreDead?.Invoke();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameObject.Find("titanic") != other.gameObject) return;
        Debug.Log("win!");
        YouWin?.Invoke();
        _ambianceFx.Stop();
    }
}