using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamingAssetsController : MonoBehaviour
{
    [SerializeField] private ImageDate baseIcon;
    
    void Start()
    {
        baseIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        
        baseIcon.gameObject.SetActive(true);

        var directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        print("Streaming: " + directoryInfo);
        var allFiles = directoryInfo.GetFiles("*.*");

        foreach (var fileInfo in allFiles)
        {
            print("File name: " + fileInfo.Name);

            if (fileInfo.Name.Contains("meta"))
            {
                continue;
            }

            var imageData = Instantiate(baseIcon, baseIcon.transform.parent);
            var bytes = File.ReadAllBytes(fileInfo.FullName);
            var texture = new Texture2D(1, 1);

            texture.LoadImage(bytes);

            var rect = new Rect(0, 0, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(texture, rect, pivot);
            
            imageData.image.sprite = sprite;
            imageData.text.text = fileInfo.Name;
        }
        
        baseIcon.gameObject.SetActive(false);
    }
}
