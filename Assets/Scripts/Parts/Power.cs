namespace Parts
{
    public class Power: Part
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