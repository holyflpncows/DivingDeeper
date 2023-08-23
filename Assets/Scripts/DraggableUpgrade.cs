using UnityEngine;
using UnityEngine.EventSystems;
using Upgrades;

public class DraggableUpgrade : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup _canvasGroup;
    private Vector3 _originalAnchoredPosition;

    public Part Upgrade;
    public Canvas Canvas;
    [HideInInspector]
    public Transform ParentAfterDrag;
    [HideInInspector]
    public bool DroppedInSlot = false;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentAfterDrag = transform.parent;
        _originalAnchoredPosition = transform.position;

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DroppedInSlot)
        {
            transform.SetParent(ParentAfterDrag, false);
            transform.position = ParentAfterDrag.position;
        }
        else
        {
            transform.position = _originalAnchoredPosition;
        }

        _canvasGroup.blocksRaycasts = true;
    }
}
