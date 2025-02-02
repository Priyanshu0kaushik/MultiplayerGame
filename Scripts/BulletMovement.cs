using Unity.Netcode;
using UnityEngine;

public class BulletMovement : NetworkBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float speed = 20f;
    [SerializeField] float lifetime = 5f;

    void Start()
    {

        Invoke("DespawnBulletServerRpc", lifetime);
        //Destroy(gameObject, lifetime+1);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bullet Collision");
            collider.gameObject.GetComponent<PlayerNetworkScript>().TakeDamageServerRpc(damage);
            if (IsServer)
            {
                DespawnBullet();
            }
            else
            {
                DespawnBulletServerRpc();
            }
            //Destroy(gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void DespawnBulletServerRpc()
    {
        DespawnBullet();
    }

    private void DespawnBullet()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}