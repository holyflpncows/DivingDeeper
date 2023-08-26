using System;
using UnityEngine;

public class SeaCreature: MonoBehaviour
{
    public int speed;
    public int damage;
    public string displayName;

    private void Start()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.mass *= transform.localScale.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameObject.FindGameObjectWithTag("Player")== other.gameObject)
        {
            Submarine.Instance.TakeEnemyDamage(damage);
        }
    }
}