using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SpawnerByPoints<T> : MonoBehaviour where T : Object
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _coolDown;
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _pool;
    
    private void Awake()
    {
        if (_spawnPoints.Length == 0)
            throw new NullReferenceException(name +  " SpawnPoints is empty");
        
        if (_prefab == null) 
            throw new NullReferenceException(nameof(_prefab));
        
        if (_pool == null) 
            throw new NullReferenceException(nameof(_pool));
    }

    private void OnEnable()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        var wait = new WaitForSeconds(_coolDown);
        
        while (enabled)
        {
            Create();
            yield return wait;
        }
    }

    private void Create()
    {
        Vector3 position = GetSpawnPoint();
        Instantiate(_prefab, position, Quaternion.identity, _pool);
    }

    private Vector3 GetSpawnPoint()
    {
        int randomPoint = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[randomPoint].position;
    }
}
