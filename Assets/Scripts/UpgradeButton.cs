using System;
using Parts;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : Part, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void PartsStats(Part part);

    public GameObject lockedGameObject;
    public static event PartsStats PartsStatsEnter;
    public static event PartsStats PartsStatsExit;
    
    private void Start()
    {
        if (isLocked)
        {
            // Not sure how to set the rect transform. 
            // var locked = Instantiate(lockedGameObject, new Vector3(-7,-7,-1), Quaternion.identity);
            // locked.GetComponent<Transform>().parent = gameObject.transform;
            // PrefabUtility.RecordPrefabInstancePropertyModifications(locked.GetComponent<Transform>());
        }
        var eventType = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
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
        }        else if (isLocked)
        {
            Debug.Log("You can't haz it yet");
            //TODO popup already own this part
            return;
        }
        Submarine.Instance.AddPart(this);
        PlayerAttributes.Instance.cashMoney -= cost;
        Debug.Log($"you have ${PlayerAttributes.Instance.cashMoney} left");
        var soundFx = GameObject.Find("UpgradeSound").GetComponent<AudioSource>();
        soundFx.time = 0.3375f;
        soundFx.Play();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        PartsStatsEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PartsStatsExit?.Invoke(this);
    }
}
