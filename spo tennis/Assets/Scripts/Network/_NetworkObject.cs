using Mirror;
using UnityEngine;

public class _NetworkObject : NetworkBehaviour
{
    [SerializeField] private Transform SpawnedObjectPrefab;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Transform SpawnedObjectTransform = Instantiate(SpawnedObjectPrefab);
            //SpawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
