using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaterOfTheDead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}