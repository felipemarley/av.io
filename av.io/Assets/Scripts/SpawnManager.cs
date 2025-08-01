using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;

    [SerializeField] private float spawnRadius = 30f; // Distância do jogador para spawnar. Ajuste para garantir que seja fora da tela.
    [SerializeField] private int maxEnemies = 15; // Número máximo de inimigos na tela

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Espera um tempo aleatório
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            
            // Espera até que o número de inimigos seja menor que o máximo
            yield return new WaitUntil(() => BasicEnemy.enemyCount < maxEnemies);
            
            if (playerTransform == null) yield break; // Para o spawn se o jogador não existir

            // Escolhe um inimigo aleatório da lista para spawnar
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];
            
            // Lógica de Posição de Spawn
            Vector3 spawnPosition;
            AirplaneEnemy airplaneScript = enemyToSpawn.GetComponent<AirplaneEnemy>();

            if (airplaneScript != null) // SE for um avião
            {
                // Spawna na esquerda ou na direita do jogador
                float spawnDirection = (Random.value > 0.5f) ? 1f : -1f; // 1 para direita, -1 para esquerda
                spawnPosition = playerTransform.position + new Vector3(spawnRadius * spawnDirection, Random.Range(-5f, 5f), 0);
            }
            else // SE for qualquer outro inimigo (Balao, etc)
            {
                // Spawna em um ponto aleatório do círculo ao redor do jogador
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                spawnPosition = playerTransform.position + (Vector3)(randomDirection * spawnRadius);
            }

            // Cria o inimigo e configura se for um avião
            GameObject newInstance = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            if (airplaneScript != null)
            {
                // Informa ao avião para qual lado ele deve se mover (oposto de onde nasceu)
                AirplaneEnemy newAirplane = newInstance.GetComponent<AirplaneEnemy>();
                if (newInstance.transform.position.x > playerTransform.position.x)
                {
                    newAirplane.moveDirection = Vector2.left; // Nasceu na direita, anda pra esquerda
                }
                else
                {
                    newAirplane.moveDirection = Vector2.right; // Nasceu na esquerda, anda pra direita
                }
            }
        }
    }
}