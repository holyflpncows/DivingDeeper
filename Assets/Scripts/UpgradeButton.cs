using Parts;
using UnityEngine;

public class UpgradeButton : Part
{
    public void Purchase()
    {
        if (PlayerAttributes.Instance.CashMoney < this.cost)
        {
            //TODO popup to say you can't afford
            return;
        }
        else if (Submarine.Instance.HasPart(this))
        {
            //TODO popup already own this part
            return;
        }
        
        Submarine.Instance.AddPart(this);
        
    }
}
