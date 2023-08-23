using System;

namespace Parts
{
    public class Part
    {
        /// <summary>
        /// Display Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Cost in cash-money
        /// </summary>
        public int Cost;

        /// <summary>
        /// in kilograms ish
        /// </summary>
        public int Weight;

        /// <summary>
        /// Coefficient of how cool it looks, smells, or sounds
        /// 0-100
        /// </summary>
        public int CoolnessCoefficient;

        /// <summary>
        /// affects speed and manoeuvring
        /// </summary>
        public int Drag;

        /// <summary>
        /// how much damage it can take measured in HP
        /// </summary>
        public int Durability;

        /// <summary>
        /// Perceived Durability
        /// What the player sees
        /// </summary>
        /// <returns></returns>
        public double PerceivedDurability =>
            Math.Ceiling(Durability + Math.Pow(Durability, (CoolnessCoefficient + PlayerAttributes.Instance.Ego) / 100f));
    }
}