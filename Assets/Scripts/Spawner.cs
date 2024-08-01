using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private Cube _cube;
    [SerializeField] private ObjectPoolCube _pool;

    private int _spawnCount = 10;

    private void Start()
    {
        StartCoroutine(CubesGenerator());
    }

    private void CreateCubes()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
        {
            Cube cube = _pool.Get();
            cube.gameObject.SetActive(true);
            cube.ChangeColor();
            cube.transform.position = _spawnPosition[i].position;
            cube.Died += PoolReturn;
            cube.ChangeTouch();
        }
    }

    private void PoolReturn(Cube cube)
    {
        cube.Died -= PoolReturn;
        _pool.Release(cube);
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
