using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab = null;
    [SerializeField] private int wallWidth = 8;
    [SerializeField] private int wallHeight = 8;
    [Space]
    [SerializeField] private int twistingColumnHeight = 8;
    [Space]
    [SerializeField] private int pyramidHeight = 8;
    
    private void Start()
    {
        //GenerateWall();
        GenerateTwistingColumn(twistingColumnHeight, this.transform.position + Vector3.forward * 2f);
        GeneratePyramid(pyramidHeight, this.transform.position + Vector3.forward);
    }

    private void GenerateWall()
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();

        Vector3 blockSize = blockRenderer.bounds.size;
        for (int i = 0; i < wallWidth; i++)
        {
            for (int j = 0; j < wallHeight; j++)
            {
                GameObject go = GameObject.Instantiate(blockPrefab);
                go.transform.position = new Vector3(blockSize.x * i, blockSize.y * j, this.transform.position.z);
            }
        }
    }
    
    [ContextMenu("Generate TwistingColumn")]
    private void GenerateTwistingColumn(int height, Vector3 startPosition)
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();
        Vector3 blockSize = blockRenderer.bounds.size;
        for (int i = 0; i < height; i++)
        {
            GameObject go = GameObject.Instantiate(blockPrefab);
            go.transform.position = startPosition + new Vector3(0, blockSize.y * i, startPosition.z);
            go.transform.rotation = Quaternion.Euler(0f, 15f * i, 0f);
            
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.HSVToRGB(((float)i/height), 1f, 1f));
        }
    }

    [ContextMenu("Generate Pyramid")]
    private void GeneratePyramid(int height, Vector3 startPosition)
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();
        Vector3 blockSize = blockRenderer.bounds.size;
        startPosition += Vector3.up * (blockSize.y * height);
        for (int yIndex = 0; yIndex < height; yIndex++)
        {
            for (int xIndex = 0; xIndex < yIndex; xIndex++)
            {
                GameObject go = GameObject.Instantiate(blockPrefab);
                float startX = -(blockSize.x * yIndex) / 2f;
                go.transform.position = startPosition + new Vector3(startX + (blockSize.x * xIndex), blockSize.y * -yIndex, startPosition.z);
                
                Renderer rend = go.GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.HSVToRGB(((float)yIndex/height), Mathf.Lerp(0.5f, 1f, ((float)xIndex/yIndex)), 1f));
            }
        }
    }
    
}
