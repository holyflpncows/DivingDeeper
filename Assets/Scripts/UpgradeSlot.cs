using Parts;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSlot : MonoBehaviour, IDropHandler
{
    public PartType Type;
    //public Part PartUpgrade;
    public GameObject UpgradesContainer;

    public void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        var draggable = dropped.GetComponent<DraggableUpgrade>();
        //PartUpgrade = draggable.PartUpgrade;

        //var upgrades = UpgradesContainer.GetComponentsInChildren<DraggableUpgrade>()
        //    .Where(u => u.PartUpgrade.Type == PartUpgrade.Type);

        //// This SHOULD be a single result, but just in case?
        //foreach (var upgrade in upgrades)
        //{
        //    upgrade.SetIgnoreDrag(false);
        //}

        draggable.ParentAfterDrag = transform;
        draggable.DroppedInSlot = true;
    }
}
