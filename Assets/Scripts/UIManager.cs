using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button[] chooseCharacterButtons;
    [SerializeField] private HairsConfig hairsConfig;
    [SerializeField] private BodyConfig bodyConfig;
    [SerializeField] private Button hairColorButton;
    [SerializeField] private Button bodyColorButton;
    [SerializeField] private Button textureButton;
    [SerializeField] private GameObject rootHair;
    [SerializeField] private GameObject rootBody;
    [SerializeField] private GameObject rootTexture;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void HideChooseCharacterButtons()
    {
        foreach (var button in chooseCharacterButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowColorButtons()
    {
        foreach (var color in hairsConfig.Colors)
        {
            var btn = Instantiate(hairColorButton, rootHair.transform);
            btn.GetComponent<Image>().color = color;
            
            btn.onClick.AddListener(delegate
            {
                controller.ChangeHairsColor(color);
            });
        }
        
        foreach (var color in bodyConfig.Colors)
        {
            var btn = Instantiate(bodyColorButton, rootBody.transform);
            btn.GetComponent<Image>().color = color;
            
            btn.onClick.AddListener(delegate
            {
                controller.ChangeBodyColor(color);
            });
        }

        var files = Directory.GetFiles(Path.Combine(Application.streamingAssetsPath, "Textures")).ToList();

        for (int i = files.Count - 1; i >= 0; i--)
        {
            if (files[i].Contains("meta"))
            {
                files.Remove(files[i]);
            }
        }

        for (var i = 0; i < files.Count; i++)
        {
            var btn = Instantiate(textureButton, rootTexture.transform);

            var y = i;
            
            btn.onClick.AddListener(delegate
            {
                controller.ChangeTexture(y);
            });
        }
    }
}