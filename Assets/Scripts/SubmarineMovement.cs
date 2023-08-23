using System.Linq;
using Parts;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 _moveDirection;

    public delegate void HitThingAction();

    public static event HitThingAction YouAreDead;

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();
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
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Submarine.Instance.AddPart(new Hull
        {
            Name = "shiny",
            Cost = 200,
            CoolnessFactor = 1000,
            Drag = 33,
            Durability = 9,
            Weight = 1000
        });
        if(GameObject.FindGameObjectsWithTag("Enemy").Contains(other.gameObject))
            YouAreDead?.Invoke();
    }
}