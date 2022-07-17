using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Gameplay/Resources/Resource")]
public class Resource : ScriptableObject
{
    [SerializeField] private string displayName = "";
    [SerializeField] private Sprite icon = null;

    public string DisplayName { get => displayName;}
    public Sprite Icon { get => icon;}
}
