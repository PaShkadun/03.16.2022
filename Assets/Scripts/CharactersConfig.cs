using UnityEngine;

[CreateAssetMenu]
public class CharactersConfig : ScriptableObject
{
    [SerializeField] private string[] maleCharacters;
    [SerializeField] private string[] femaleCharacters;

    public string[] MaleCharacters => maleCharacters;
    public string[] FemaleCharacters => femaleCharacters;
}