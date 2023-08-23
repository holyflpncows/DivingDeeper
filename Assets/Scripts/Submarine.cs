using System;
using System.Collections.Generic;
using System.Linq;
using Parts;
using UnityEngine;

public class Submarine : MonoBehaviour
    {
        private List<Part> _parts = new();

        public static Submarine Instance;

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
        
        public int GetDrag => _parts.Sum(p => p.drag);

        public bool HasPart(Part part) => _parts.Any(p => part.name == p.displayName 
                                                          && p.partType == part.partType);
    }
