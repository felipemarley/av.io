using UnityEngine;

public class AirplaneEnemy : BasicEnemy
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private float nextFireTime = 0f;


    public Vector2 moveDirection = Vector2.left;

    protected override void Move()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    protected override void Update()
    {
        if (Time.time > nextFireTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (playerTransform == null) return; // Não atira se não encontrar o jogador

        nextFireTime = Time.time + fireRate;

        // Calcula a direção para a posição do jogador NAQUELE MOMENTO
        Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;

        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        // Envia a direção calculada para o projétil
        projectileInstance.GetComponent<EnemyProjectile>().SetDirection(directionToPlayer);
    }
}