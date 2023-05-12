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

            // percorre cada tile do Tilemap at� que o n�mero de inst�ncias seja alcan�ado
            while (count < prefabData.spawnCount)
            {
                // obt�m uma posi��o aleat�ria dentro do Tilemap
                Vector3 randomPosition = new Vector3(Random.Range(tilemap.cellBounds.min.x, tilemap.cellBounds.max.x),
                                                     Random.Range(tilemap.cellBounds.min.y, tilemap.cellBounds.max.y),
                                                     tilemap.transform.position.z);

                // verifica se o tile existe na posi��o gerada
                Vector3Int tilePosition = tilemap.WorldToCell(randomPosition);
                if (tilemap.HasTile(tilePosition))
                {
                    // cria uma inst�ncia do prefab na posi��o gerada
                    Instantiate(prefabData.prefab, randomPosition, Quaternion.identity);
                    count++;
                }
            }
        }
    }
}
