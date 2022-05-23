using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Placholder Item", menuName = "Debug Placholders" )]
public class PlaceholderAsset : ScriptableObject
{
    public List<Sprite> Sprites;  
}

public interface IPlaceholder {

}
