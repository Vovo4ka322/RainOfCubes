using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    private float _minValue = 2;
    private float _maxValue = 5;
    private bool _isTouch = false;

    public event Action<Cube> Died;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Platform>() && _isTouch == false)
        {
            _isTouch = true;

            _renderer.material.color = UnityEngine.Random.ColorHSV();

            StartCoroutine(CubeRemover());
        }
    }

    private IEnumerator CubeRemover()
    {
        float random = UnityEngine.Random.Range(_minValue, _maxValue + 1);

        WaitForSeconds timeToRemove = new(random);

        yield return timeToRemove;

        Died?.Invoke(this);
    }

    public Color ReturnOriginalColor()
    {
        return _renderer.material.color = Color.white;
    }

    public void ChangeTouchOnFalse()
    {
        _isTouch = false;
    }
}
