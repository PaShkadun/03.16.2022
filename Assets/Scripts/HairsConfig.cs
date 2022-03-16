using UnityEngine;

[CreateAssetMenu]
public class HairsConfig : ScriptableObject
{
    [SerializeField] private Color[] colors;

    public Color[] Colors => colors;
}