using Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SubmarineRenderer : MonoBehaviour
{
    [FormerlySerializedAs("Flip")] public bool flip;
    private Submarine _subInstance;
    private List<Part> _parts = new List<Part>();

    private void Start()
    {
        _subInstance = Submarine.Instance;
        _subInstance.OnPartAdded += PartAdded;

        HideAllParts();
        foreach (var part in _subInstance.Parts)
        {
            _parts.Add(part);
            ShowPartWithName(part.notDisplayName);
        }
    }

    public void PartAdded(object sender, Part part)
    {
        var originalParts = _parts.Where(p => p.notDisplayName.StartsWith($"{part.partType}_"));
        foreach (var originalPart in originalParts)
        {
            _parts.Remove(originalPart);
        }
        _parts.Add(part);

        string partName = GetPartComponentName(part);

        HideAllPartsOfType(part.partType);
        ShowPartWithName(partName);
    }

    public void ShowTemporaryPart(Part part)
    {
        string partName = GetPartComponentName(part);

        HideAllPartsOfType(part.partType);
        ShowPartWithName(partName);
    }

    public void HideTemporaryPart(Part part)
    {
        if (_parts.Any(p=>p.notDisplayName == part.notDisplayName))
            return;
        string partName = GetPartComponentName(part);
        HidePartWithName(partName);
    }

    private string GetPartComponentName(Part part)
    {
        return part.notDisplayName;
    }

    private void ShowPartWithName(string partName)
    {
        var child = GetComponentsInChildren<Transform>(true).First(c => c.name == partName);
        child.gameObject.SetActive(true);
    }

    private void HidePartWithName(string partName)
    {
        var child = GetComponentsInChildren<Transform>(true).First(c => c.name == partName);

        child.gameObject.SetActive(false);
    }

    private void HideAllPartsOfType(PartType type)
    {
        var children = GetComponentsInChildren<Transform>(true).Where(c => c.name.StartsWith($"{type}_"));
        foreach (var child in children)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void HideAllParts()
    {
        var partTypes = Enum.GetValues(typeof(PartType));

        foreach (var partType in partTypes)
        {
            HideAllPartsOfType((PartType)partType);
        }
    }
}
