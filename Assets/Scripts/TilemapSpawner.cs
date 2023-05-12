using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

[System.Serializable]
public class PrefabData
{
    public GameObject prefab;
    public int spawnCount;
}

public class TilemapSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public List<PrefabData> prefabs = new List<PrefabData>();


    IEnumerator Start()
    {

        yield return new WaitForSeconds(0.1f); ;

        foreach (var prefabData in prefabs)
        {
            int count = 0;

            // percorre cada tile do Tilemap até que o número de instâncias seja alcançado
            while (count < prefabData.spawnCount)
            {
                // obtém uma posição aleatória dentro do Tilemap
                Vector3 randomPosition = new Vector3(Random.Range(tilemap.cellBounds.min.x, tilemap.cellBounds.max.x),
                                                     Random.Range(tilemap.cellBounds.min.y, tilemap.cellBounds.max.y),
                                                     tilemap.transform.position.z);

                // verifica se o tile existe na posição gerada
                Vector3Int tilePosition = tilemap.WorldToCell(randomPosition);
                if (tilemap.HasTile(tilePosition))
                {
                    // cria uma instância do prefab na posição gerada
                    Instantiate(prefabData.prefab, randomPosition, Quaternion.identity);
                    count++;
                }
            }
        }
    }
}
