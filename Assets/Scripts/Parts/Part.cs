using System;
using UnityEngine;

namespace Parts
{
    public class Part : MonoBehaviour
    {
        private const float EgoAndCoolnessSum = 200f;
        /// <summary>
        /// affects the lenght of time curve stays flat
        /// </summary>
        private const double FlatNess = 8;

        /// <summary>
        /// affects the magnitude of the final inflated amount
        /// </summary>
        private const double Magnitude = 10;

        /// <summary>
        /// PartType
        /// </summary>
        public PartType partType;

        /// <summary>
        /// Display Name
        /// </summary>
        public string displayName;
        
        /// <summary>
        /// Name Name
        /// </summary>
        public string notDisplayName;

        /// <summary>
        /// Cost in cash-money
        /// </summary>
        public int cost;

        /// <summary>
        /// in kilograms ish
        /// </summary>
        public int weight;

        /// <summary>
        /// Coefficient of how cool it looks, smells, or sounds
        /// 0-100
        /// </summary>
        [Range(0, 100)] public int coolnessCoefficient;

        /// <summary>
        /// affects speed and manoeuvring
        /// </summary>
        public int drag;

        /// <summary>
        /// how much damage it can take measured in HP
        /// </summary>
        public int durability;

        /// <summary>
        /// Can't buy yet
        /// </summary>
        public bool isLocked;

        /// <summary>
        /// Inflate the stat so it looks like more than it is 
        /// What the player sees
        /// </summary>
        /// <returns></returns>
        public double PerceivedStatInflated (int stat) =>
            Math.Ceiling(stat *
                         (1 + Math.Pow((coolnessCoefficient + PlayerAttributes.Instance.ego) / EgoAndCoolnessSum, FlatNess)) *
                         Magnitude);

        /// <summary>
        /// Inflate the stat so it looks like more than it is 
        /// What the player sees
        /// </summary>
        /// <returns></returns>
        public double PerceivedStatDeflated(int stat) =>
            Math.Max(stat * 
                     (1 + Math.Pow((coolnessCoefficient + PlayerAttributes.Instance.ego) / EgoAndCoolnessSum, FlatNess)) * 
                     -1 * Magnitude, stat / 6f);
    }
}