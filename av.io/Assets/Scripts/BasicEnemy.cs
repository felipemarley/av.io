using UnityEngine;

public abstract class BasicEnemy : MonoBehaviour
{
    public static int enemyCount = 0;

    [Header("Base Attributes")]
    [SerializeField] protected int health = 10;
    [SerializeField] protected float speed = 3f;

    protected Rigidbody2D rb;
    protected Transform playerTransform;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            playerTransform = playerGO.transform;
        }
    }

    protected virtual void OnEnable()
    {
        // Incrementa a contagem quando um inimigo é criado/ativado
        enemyCount++;
    }

    protected virtual void OnDisable()
    {
        // Decrementa a contagem quando um inimigo é destruído
        enemyCount--;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }
    
    protected virtual void Update()
    {
        CheckScreenBounds();
    }

    protected virtual void Move()
    {
        rb.linearVelocity = new Vector2(-speed, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private void CheckScreenBounds()
    {
        if (playerTransform == null) return; // Não faz nada se não houver jogador

        // Se o inimigo estiver muito longe do jogador, destrua-o.
        if (Vector3.Distance(transform.position, playerTransform.position) > 50f) // 30f é uma distância grande
        {
            Destroy(gameObject);
        }
    }
}