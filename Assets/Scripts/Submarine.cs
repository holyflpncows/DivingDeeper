using Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [HideInInspector]
    public List<Part> Parts = new();

    public static Submarine Instance;

    public int health;
    public EventHandler<Part> OnPartAdded;
    private double _previousDepth;
    private const bool godmode = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (Instance != null)
        {
            SetParts(Instance.Parts);
        }
        health = Instance.GetDurability + 200;
    }

    private static void SetParts(List<Part> parts)
    {
        Instance.Parts = parts;
    }

    public void AddPart(Part part)
    {
        Debug.Log($"parts # {Parts.Count()}");
        Parts.Add(part);

        OnPartAdded?.Invoke(this, part);
    }

    public int GetDurability => Parts.Sum(p => p.durability);

    public float GetDrag => Parts.Sum(p => p.drag);

    public int GetDisplayDurability => (int)Parts.Sum(p => p.PerceivedStatInflated(p.durability));

    public float GetDisplayDrag => (float)Parts.Sum(p => p.PerceivedStatDeflated(p.drag));


    public bool HasPart(Part part) => Parts.Any(p => part.displayName == p.displayName
                                                      && p.partType == part.partType);

    public void TakeDepthDamage(double depth)
    {
        if (godmode)
            return;
        if (Math.Abs(depth - _previousDepth) < 1d)
            return;
        _previousDepth = depth;
        var depthDamage = (int)Math.Ceiling(1 + Math.Pow(depth / 200f, 4) * 0.03f);
        Instance.health -= depthDamage;
    }

    public void TakeEnemyDamage(int damage)
    {
        if (godmode)
            return;
        Instance.health -= damage;
    }
}
