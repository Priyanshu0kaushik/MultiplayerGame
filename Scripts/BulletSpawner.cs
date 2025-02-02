using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{

    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayStateManager.Instance != null)
        {
            if (IsOwner && (Input.GetKey(KeyCode.E)||Input.GetMouseButton(0)) && Time.time > nextFireTime)
            {
                FireBulletServerRpc();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    [ServerRpc]
    void FireBulletServerRpc()
    {
        var instance = Instantiate(bulletPrefab);
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.transform.position = transform.position + new Vector3(0, 1, 0);
        instanceNetworkObject.Spawn();
    }
}
