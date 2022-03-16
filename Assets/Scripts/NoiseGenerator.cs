using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [SerializeField] private int width = 512;
    [SerializeField] private int height = 512;

    [SerializeField] private float xOright;
    [SerializeField] private float yOright;

    [SerializeField] private float scale = 10f;

    private Texture2D noiseTexture;
    private Color[] pix;
    private Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        noiseTexture = new Texture2D(width, height);
        pix = new Color[noiseTexture.width * noiseTexture.height];
        rend.material.mainTexture = noiseTexture;
    }

    void Update()
    {
        CalculateNoise();
    }

    private void CalculateNoise()
    {
        var y = -1f;

        while (++y < noiseTexture.height)
        {
            var x = -1f;

            while (++x < 512)
            {
                var xCoord = xOright + x / noiseTexture.width * scale;
                var yCoord = yOright + y / noiseTexture.height * scale;
                var sample = Mathf.PerlinNoise(xCoord, yCoord);

                pix[(int) y * noiseTexture.height + (int) x] = new Color(sample, sample, sample);
            }
        }
        
        noiseTexture.SetPixels(pix);
        noiseTexture.Apply();
    }

    [ContextMenu("Save")]
    public void SaveTexture()
    {
        var bytes = noiseTexture.EncodeToPNG();
        var path = Path.Combine(Application.dataPath, "Textures");
        Debug.Log(path);
        path = Path.Combine(path, "test.png");

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        
        File.WriteAllBytes(path, bytes);

        AssetDatabase.Refresh();
    }
}
