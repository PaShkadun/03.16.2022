using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharactersConfig config;
    [SerializeField] private Transform position;
    
    private GameObject character;
    private GameObject hairs;
    private GameObject face;
    private Material faceMaterial;
    private Material hairsMaterial;
    private GameObject body;
    private Material[] bodyMaterials;
    private List<Texture> textures = new List<Texture>();

    private void CheckCharacter()
    {
        if (character != null)
        {
            Destroy(character);
        }
    }

    private void CreateCharacter(string path)
    {
        character = Resources.Load<GameObject>(path);
        character = Instantiate(character, position);
        
        hairs = GameObject.FindWithTag("Hairs");
        hairsMaterial = hairs.GetComponent<Renderer>().material;

        body = GameObject.FindWithTag("Body");
        bodyMaterials = body.GetComponents<Renderer>().Select(x => x.material).ToArray();

        face = GameObject.FindWithTag("Face");
        faceMaterial = face.GetComponent<Renderer>().material;

        CreateTextures();
    }

    private void CreateTextures()
    {
        var directoryInfo = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "Textures"));
        var allFiles = directoryInfo.GetFiles("*.*");

        foreach (var fileInfo in allFiles)
        {
            if (!fileInfo.Name.Contains("Text") || fileInfo.Name.Contains("meta"))
            {
                continue;
            }

            var bytes = File.ReadAllBytes(fileInfo.FullName);
            var texture = new Texture2D(1, 1);

            texture.LoadImage(bytes);
            texture.Apply();

            textures.Add(texture);
            
            Debug.Log($"Text count is {textures.Count}");
        }
    }

    public void GenerateMale()
    {
        CheckCharacter();
        CreateCharacter("MaleHeroes/" + config.MaleCharacters[Random.Range(0, config.MaleCharacters.Length)]);
    }

    public void GenerateFemale()
    {
        CheckCharacter();
        CreateCharacter("FemaleHeroes/" + config.FemaleCharacters[Random.Range(0, config.MaleCharacters.Length)]);
    }

    public void ChangeHairsColor(Color color)
    {
        hairsMaterial.color = color;
    }
    
    public void ChangeBodyColor(Color color)
    {
        foreach (var material in bodyMaterials)
        {
            material.color = color;
        }
    }

    public void ChangeTexture(int i)
    {
        if (i < textures.Count)
        {
            faceMaterial.mainTexture = textures[i];
        }
    }
}