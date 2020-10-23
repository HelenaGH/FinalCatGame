using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;//svi objekti u igri imaju pristup PlatformManager-u

    [SerializeField]
    GameObject platformPrefab;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start() //postavljanje triju platformi na različite pozicije
    {
        Instantiate(platformPrefab, new Vector2(1.5f, 9.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(10f, 15f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(20f, 20f), platformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
