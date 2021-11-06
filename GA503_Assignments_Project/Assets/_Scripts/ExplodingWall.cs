using System.Collections.Generic;
using UnityEngine;

public class ExplodingWall : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab = null;
    [SerializeField] private int wallWidth = 8;
    [SerializeField] private int wallHeight = 8;
    [Space]
    [SerializeField] private GameObject explosionEffectPrefab = null;
    [SerializeField] private LayerMask clickLayerMask;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;
    
    private List<Rigidbody> rigidBodies = new List<Rigidbody>();

    private void Start()
    {
        GenerateWall();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode(transform.position);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f, clickLayerMask.value))
            {
                if (rigidBodies.Contains(raycastHit.rigidbody))
                {
                    Explode(raycastHit.point);
                }
            }
        }
    }

    private void Explode(Vector3 explosionCenter)
    {
        foreach (Rigidbody rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
            rigidBody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius, 1f, ForceMode.Impulse);
        }

        GameObject go = GameObject.Instantiate(explosionEffectPrefab, explosionCenter, Quaternion.identity);
        Destroy(go, 0.6f);
    }

    private void GenerateWall()
    {
        Renderer blockRenderer = blockPrefab.GetComponent<Renderer>();

        Vector3 blockSize = blockRenderer.bounds.size;
        Vector3 startOffset = new Vector3(-(blockSize.x * wallWidth) / 2f, blockSize.y/2f, 0f);
        for (int i = 0; i < wallWidth; i++)
        {
            for (int j = 0; j < wallHeight; j++)
            {
                Rigidbody rb = GameObject.Instantiate(blockPrefab).GetComponent<Rigidbody>();
                rb.transform.position = startOffset + new Vector3(blockSize.x * i, blockSize.y * j, this.transform.position.z);
                rigidBodies.Add(rb);
                rb.isKinematic = true;
            }
        }
    }
    
}