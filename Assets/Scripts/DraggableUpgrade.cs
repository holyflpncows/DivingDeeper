using UnityEngine;
using UnityEngine.EventSystems;
using Upgrades;

public class DraggableUpgrade : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Part Upgrade;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}
