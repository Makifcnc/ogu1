using UnityEngine;

[CreateAssetMenu(fileName = "NewElementData", menuName = "Scriptable Objects/Element Data")]
public class ElementData : ScriptableObject
{
    public ElementType elementType;            // Örn: Fire, Water, Earth, Air
    public Color tintColor;                    // Örn: Kırmızı, Mavi, Kahverengi, vs.
    //public RuntimeAnimatorController animCtrl; // Her elemente özel animasyon
}
