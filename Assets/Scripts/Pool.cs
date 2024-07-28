using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _object;

    private List<T> _pool = new();

    public void Release(T @object)
    {
        if (_pool.Contains(@object) == false)
        {
            _pool.Add(@object);
            @object.gameObject.SetActive(false);
        }
    }

    public T Get()
    {
        if (_pool.Count != 0)
        {
            T firstElement = _pool[0];

            _pool.Remove(firstElement);

            return firstElement;
        }

        T objectForReturn = CreateObject(_object);

        objectForReturn.gameObject.SetActive(false);

        return objectForReturn;
    }

    private T CreateObject(T objectCreator) => Instantiate(objectCreator);
}