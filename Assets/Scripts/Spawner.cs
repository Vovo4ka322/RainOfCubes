using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private Cube _cube;
    [SerializeField] private ObjectPoolCube _pool;

    private float _spawnCount = 0.4f;

    private void Start()
    {
        StartCoroutine(CubesGenerator());
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

    private void CreateCubes()
    {
        int randomPosition = Random.Range(0, _spawnPosition.Length);

        Cube cube = _pool.Get();
        cube.gameObject.SetActive(true);
        cube.ReturnOriginalColor();
        cube.transform.position = _spawnPosition[randomPosition].position;
        cube.Died += PoolReturn;
        cube.ChangeTouchOnFalse();

    }

    private void PoolReturn(Cube cube)
    {
        cube.Died -= PoolReturn;
        _pool.Release(cube);
    }
}
