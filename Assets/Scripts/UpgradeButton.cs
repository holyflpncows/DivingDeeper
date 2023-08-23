using Parts;

public class UpgradeButton : Part
{
    public void Purchase()
    {
        // TODO: Cant afford?  Return.
        // TODO: Part already added?  Return.

        Submarine.Instance.AddPart(this);
    }
}
