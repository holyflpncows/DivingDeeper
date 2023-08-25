using System.Linq;
using Parts;
using Unity.VisualScripting;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 _moveDirection;

    private bool _amAlive = true;

    public delegate void HitThingAction();

    public static event HitThingAction YouAreDead;

    // Update is called once per frame
    private void Update()
    {
        CheckIfAlive();
        ProcessInputs();
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
            _moveDirection.x * moveSpeed*(Submarine.Instance.GetDrag),
            _moveDirection.y * moveSpeed*(Submarine.Instance.GetDrag));
    }


    
    private static void Died()
    {
        Debug.Log("died");
        var deadSoundFx = GameObject.Find("Dead").GetComponent<AudioSource>();
        deadSoundFx.Play();
        YouAreDead?.Invoke();
    }

}