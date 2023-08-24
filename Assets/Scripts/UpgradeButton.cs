using System;
using Parts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : Part, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void PartsStats();

    public static event PartsStats PartsStatsEnter;
    public static event PartsStats PartsStatsExit;
    
    private void Start()
    {
        EventTrigger.Entry eventtype = new EventTrigger.Entry();
        eventtype.eventID = EventTriggerType.PointerEnter;
    }

    public void Purchase()
    {
        if (PlayerAttributes.Instance.cashMoney < this.cost)
        {
            Debug.Log("You poor");
            //TODO popup to say you can't afford
            return;
        }
        else if (Submarine.Instance.HasPart(this))
        {
            Debug.Log("You haz it already");
            //TODO popup already own this part
            return;
        }
        
        Submarine.Instance.AddPart(this);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PartsStatsEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PartsStatsExit?.Invoke();
    }
}
