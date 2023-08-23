using Parts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public PartType PartType;
    public GameObject UpgradesContainer;
    [HideInInspector]
    public int UpgradeLevel;

    public void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        var draggable = dropped.GetComponent<DraggableUpgrade>();

        if (draggable.PartType != PartType)
            return;

        PartType = draggable.PartType;
        UpgradeLevel = draggable.UpgradeLevel;

        var upgrades = UpgradesContainer.GetComponentsInChildren<DraggableUpgrade>()
            .Where(u => u.PartType == PartType && u.UpgradeLevel != UpgradeLevel);

        // This SHOULD be a single result item that was dragged, but just in case?
        foreach (var upgrade in upgrades)
        {
            upgrade.SetIgnoreDrag(false);
        }

        draggable.ParentAfterDrag = transform;
        draggable.DroppedInSlot = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var dropped = eventData.pointerDrag;
        var draggable = dropped.GetComponent<DraggableUpgrade>();

        Debug.Log($"Enter {draggable.PartType == PartType}");
        // TODO: If above is true, indicate a hover over a valid slot...
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var dropped = eventData.pointerDrag;
        var draggable = dropped.GetComponent<DraggableUpgrade>();

        Debug.Log($"Exit {draggable.PartType == PartType}");

        // TODO: Remove indication of a hover over a valid slot...
    }
}
