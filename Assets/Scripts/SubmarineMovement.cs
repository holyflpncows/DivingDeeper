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

    public delegate void HitThingAction();

    public static event HitThingAction YouAreDead;

    private void Start()
    {
        _deadSoundFx = GameObject.Find("Dead").GetComponent<AudioSource>();
        _submarineSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckIfAlive();
        ProcessInputs();
        _submarineSprite.flipX = _facingRight;
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
            _moveDirection.x * moveSpeed*(Submarine.Instance.GetDrag+.1f),
            _moveDirection.y * moveSpeed*(Submarine.Instance.GetDrag+.1f));
        _facingRight = _moveDirection.x > 0 && _moveDirection.x != 0;
    }
    
    private static void Died()
    {
        Debug.Log("died");
        _deadSoundFx.Play();
        YouAreDead?.Invoke();
    }
}