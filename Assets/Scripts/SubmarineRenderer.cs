using Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubmarineRenderer : MonoBehaviour
{
    public bool Flip;
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
            ShowPartWithName(part.name);
        }
    }

    public void PartAdded(object sender, Part part)
    {
        var originalParts = _parts.Where(p => p.name.StartsWith($"{part.partType}_"));
        foreach (var originalPart in originalParts)
        {
            _parts.Remove(originalPart);
        }

        string name = GetPartComponentName(part);

        HideAllPartsOfType(part.partType);
        ShowPartWithName(name);
    }

    public void ShowTemporaryPart(Part part)
    {
        string name = GetPartComponentName(part);

        HideAllPartsOfType(part.partType);
        ShowPartWithName(name);
    }

    public void HideTemporaryPart(Part part)
    {
        string name = GetPartComponentName(part);
        HidePartWithName(name);
    }

    private string GetPartComponentName(Part part)
    {
        return part.name;
    }

    private void ShowPartWithName(string name)
    {
        var child = GetComponentsInChildren<Transform>(true).First(c => c.name == name);
        child.gameObject.SetActive(true);
    }

    private void HidePartWithName(string name)
    {
        var child = GetComponentsInChildren<Transform>(true).First(c => c.name == name);

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
