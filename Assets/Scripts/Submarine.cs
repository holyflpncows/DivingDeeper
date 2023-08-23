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

        public void SetParts(List<Part> parts)
        {
            Instance._parts = parts;
        }
        
        public void AddPart(Part part)
        {
            Debug.Log($"parts # {_parts.Count()}");
            // check if has same type already 
            _parts.Add(part);
        }

        public void SwapPart(Part part)
        {
            // swap same type with new part
            _parts.Add(part);
        }
        
        public List<Part> GetParts => _parts;

        public int GetDurability => _parts.Sum(p => p.Durability);
        
        public int GetDrag => _parts.Sum(p => p.Drag);
        
    }
