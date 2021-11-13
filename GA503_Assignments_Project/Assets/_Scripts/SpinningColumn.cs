using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningColumn : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab = null;
    [SerializeField] private int columnHeight = 8;
    [SerializeField] private float rotationSpeed = 90f;

    private List<MeshRenderer> columnBlockMeshRenderers = new List<MeshRenderer>();
    
    private void Start()
    {
        GenerateTwistingColumn(columnHeight, this.transform.position);
    }

    private void Update()
    {
        for (int i = 0; i < columnBlockMeshRenderers.Count; i++)
        {
            // Rotate
            columnBlockMeshRenderers[i].transform.rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
            
            // Hue shift
            Color.RGBToHSV(columnBlockMeshRenderers[i].material.color, out float h, out float s, out float v);
            h = Mathf.Repeat(h + Time.deltaTime * (rotationSpeed / 360f), 1f);
            columnBlockMeshRenderers[i].material.SetColor("_Color", Color.HSVToRGB(h, 1f, 1f));
        }
    }
    
    private void GenerateTwistingColumn(int height, Vector3 startPosition)
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();
        Vector3 blockSize = blockRenderer.bounds.size;
        for (int i = 0; i < height; i++)
        {
            GameObject go = GameObject.Instantiate(blockPrefab);
            go.transform.position = startPosition + new Vector3(0, blockSize.y * i, startPosition.z);
            go.transform.rotation = Quaternion.Euler(0f, 15f * i, 0f);
            
            MeshRenderer rend = go.GetComponent<MeshRenderer>();
            rend.material.SetColor("_Color", Color.HSVToRGB(((float)i/height), 1f, 1f));
            columnBlockMeshRenderers.Add(rend);
        }
    }
}
