using System;
using System.Collections.Generic;
using System.Linq;
using Parts;
using UnityEngine;

public class Submarine : MonoBehaviour
    {
        private List<Part> _parts = new();

        public static Submarine Instance;

        public int health;
        private double _previousDepth;

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
                SetParts(Instance._parts);
            }
            health = Instance.GetDurability + 200;
        }

        private static void SetParts(List<Part> parts)
        {
            Instance._parts = parts;
        }
        
        public void AddPart(Part part)
        {
            Debug.Log($"parts # {_parts.Count()}");
            _parts.Add(part);
        }

        public int GetDurability => _parts.Sum(p => p.durability);
        
        public float GetDrag => _parts.Sum(p => p.drag);
        
        public int GetDisplayDurability => (int)_parts.Sum(p => p.PerceivedStatInflated(p.durability));
        
        public float GetDisplayDrag => (float)_parts.Sum(p => p.PerceivedStatDeflated(p.drag));


        public bool HasPart(Part part) => _parts.Any(p => part.displayName == p.displayName 
                                                          && p.partType == part.partType);

        public void TakeDepthDamage(double depth)
        {
            if (Math.Abs(depth - _previousDepth) < 1d)
                return;
            _previousDepth = depth;
            var depthDamage = (int)Math.Ceiling(1 + Math.Pow(depth / 200f, 4) * 0.03f);
            Instance.health -= depthDamage;
        }

        public void TakeEnemyDamage(int damage)
        {
            Instance.health -= damage;
        }
    }
