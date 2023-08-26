using System;
using UnityEngine;

public class Titanic : MonoBehaviour
{
    public static EventHandler TitanicArrived;
    private void OnCollisionEnter2D(Collision2D other)
    {
        TitanicArrived?.Invoke(this, EventArgs.Empty);
    }
}
