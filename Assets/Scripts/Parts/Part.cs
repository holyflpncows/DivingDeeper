using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Parts
{
    public class Part : MonoBehaviour
    {
        /// <summary>
        /// affects the lenght of time curve stays flat
        /// </summary>
        private const double FlatNess = 3;
        
        /// <summary>
        /// affects the magnitude of the final inflated amount
        /// </summary>
        private const double Magnitude = 5;

        /// <summary>
        /// PartType
        /// </summary>
        public PartType partType;

        /// <summary>
        /// Display Name
        /// </summary>
        public string displayName;

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
        public int coolnessCoefficient;

        /// <summary>
        /// affects speed and manoeuvring
        /// </summary>
        public int drag;

        /// <summary>
        /// how much damage it can take measured in HP
        /// </summary>
        public int durability;

        /// <summary>
        /// Perceived Durability
        /// What the player sees
        /// </summary>
        /// <returns></returns>
        public double PerceivedDurability =>
            Math.Ceiling(durability * ( 1 + Math.Pow((coolnessCoefficient + PlayerAttributes.Instance.Ego) / 200f, FlatNess)) * Magnitude);
    }
}