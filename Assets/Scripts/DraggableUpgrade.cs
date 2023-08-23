using UnityEngine;
using UnityEngine.EventSystems;
using Parts;

public class DraggableUpgrade : Part, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup _canvasGroup;
    private GameObject _clone;

    public PartType PartType;
    public int UpgradeLevel;
    public Canvas Canvas;
    public Camera Camera;
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
        var index = transform.GetSiblingIndex();

        transform.SetParent(Canvas.transform, false);
        _canvasGroup.blocksRaycasts = false;
        _clone = Instantiate(gameObject, ParentAfterDrag);
        _clone.transform.SetSiblingIndex(index);
        _clone.GetComponent<DraggableUpgrade>().SetIgnoreDrag(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.ScreenToWorldPoint(Input.mousePosition);
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
            //Destroy(gameObject);
            //_clone.GetComponent<DraggableUpgrade>().SetIgnoreDrag(false);
        }

        _canvasGroup.blocksRaycasts = true;
    }

    public void SetIgnoreDrag(bool dragging)
    {
        _canvasGroup.alpha = dragging ? 0.5f : 1;
        _canvasGroup.blocksRaycasts = !dragging;
    }
}
