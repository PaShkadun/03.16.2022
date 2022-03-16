using UnityEngine;

[CreateAssetMenu]
public class BodyConfig : ScriptableObject
{
    [SerializeField] private Color[] colors;

    public Color[] Colors => colors;
}