using UnityEngine;

public class BalloonEnemy : BasicEnemy 
{
    [Header("Sine Wave Movement")]
    [SerializeField] private float frequency = 8f; // Quão rápido ele oscila
    [SerializeField] private float amplitude = 15f; // Quão longe ele vai para cima e para baixo

    private Vector3 initialPosition;
    private float randomOffset;

    protected override void Awake()
    {
        base.Awake();
        
        // Armazena a posição exata onde o balão foi criado
        initialPosition = transform.position; 
        
        // Isso garante que os balões não se movam todos em perfeita sincronia
        randomOffset = Random.Range(0f, 2f * Mathf.PI); 
    }


    protected override void FixedUpdate()
    {
        // Calcula a nova posição Y usando a onda senoidal
        float newY = initialPosition.y + (Mathf.Sin((Time.time * frequency) + randomOffset) * amplitude);

        // Cria a nova posição mantendo o X inicial e aplicando o novo Y
        Vector2 newPosition = new Vector2(initialPosition.x, newY);

        rb.MovePosition(newPosition);
    }
    
    // Move() vazio para que ele não execute a lógica de movimento da classe base.
    protected override void Move()
    {
        
    }
}