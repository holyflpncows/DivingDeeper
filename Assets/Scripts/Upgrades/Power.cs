namespace Upgrades
{
    public class Power: Base
    {
        /// <summary>
        /// measured in kWh
        /// </summary>
        public int Energy;

        public enum Type
        {
            DieselFuel,
            Gasoline,
            NiMh,
            LiPo,
            NuclearReactor,
        }
    }
}