using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        var draggable = dropped.GetComponent<DraggableUpgrade>();
        draggable.ParentAfterDrag = transform;
        draggable.DroppedInSlot = true;
    }
}
