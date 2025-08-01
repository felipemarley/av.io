using UnityEngine;

public class HomingBalloonEnemy : BasicEnemy
{
    [Header("Homing Settings")]
    [Tooltip("A que distância do jogador o balão acelera.")]
    [SerializeField] private float detectionRadius = 10f; 

    [Tooltip("A velocidade do balão quando está perto do jogador.")]
    [SerializeField] private float boostedSpeed = 6f; 

    protected override void Move()
    {
        if (playerTransform != null)
        {
            // Calcula a distância atual entre o balão e o jogador
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Decide qual velocidade usar baseado na distância
            float currentSpeed;
            if (distanceToPlayer <= detectionRadius)
            {
                // Se estiver DENTRO do raio de detecção, usa a velocidade aumentada
                currentSpeed = boostedSpeed;
            }
            else
            {
                // Se estiver FORA, usa a velocidade normal (a variável 'speed' da classe base)
                currentSpeed = speed;
            }

            // Calcula a direção para o jogador
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            
            // Aplica a velocidade correta (normal ou aumentada) nessa direção
            rb.linearVelocity = direction * currentSpeed;
        }
        else
        {
            // Se o jogador não existir, o balão para.
            rb.linearVelocity = Vector2.zero;
        }
    }
}