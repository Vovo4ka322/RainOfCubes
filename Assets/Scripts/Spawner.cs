using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private Transform _cube;
    [SerializeField] private ObjectPoolCube _pool;

    private int _spawnCount = 10;

    void Start()
    {
        StartCoroutine(CubesGenerator());
    }

    private void CreateCubes()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
        {
            Cube cube = _pool.Return();
            cube.gameObject.SetActive(true);
            cube.transform.position = _spawnPosition[i].position;
        }
    }

    private IEnumerator CubesGenerator()
    {
        WaitForSeconds timeToSpawn = new(_spawnCount);

        while (enabled)
        {
            CreateCubes();

            yield return timeToSpawn;
        }
    }
}
