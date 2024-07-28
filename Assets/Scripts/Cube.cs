using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ObjectPoolCube _cubePool;
    [SerializeField] private MeshRenderer _renderer;

    private float _minValue = 2;
    private float _maxValue = 5;
    private float _random;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Platform>())
        {
            _renderer.material.color = Random.ColorHSV();

            StartCoroutine(CubeRemover());
        }
    }

    private IEnumerator CubeRemover()
    {
        _random = Random.Range(_minValue, _maxValue + 1);

        WaitForSeconds timeToRemove = new(_random);

        while (enabled)
        {
            _cubePool.Release(this);
            this.gameObject.SetActive(false);

            yield return timeToRemove;
        }
    }
}
