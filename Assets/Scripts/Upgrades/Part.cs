namespace Upgrades
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
        /// How looks, smells, or sounds cool
        /// </summary>
        public int CoolnessFactor;
        /// <summary>
        /// affects speed and manoeuvring
        /// </summary>
        public int Drag;
        /// <summary>
        /// how much damage it can take measured in HP
        /// </summary>
        public int Durability;

        public void TakeDamage(int damage)
        {
            Durability -= damage;
        }
    }
}