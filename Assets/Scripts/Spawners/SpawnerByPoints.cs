using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SpawnerByPoints<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _coolDown;
    [SerializeField] private T _prefab;
    
    private void Awake()
    {
        if (_spawnPoints.Length == 0)
            throw new NullReferenceException(name +  " SpawnPoints is empty");
        
        if (_prefab == null) 
            throw new NullReferenceException(nameof(_prefab));
    }

    private void OnEnable()
    {
        StartCoroutine(Run());
    }
    
    private void OnValidate()
    {
        if (_coolDown < 0)
            _coolDown = 0;
    }


    private IEnumerator Run()
    {
        var wait = new WaitForSeconds(_coolDown);
        
        while (enabled)
        {
            if (HaveFreeSpawnPoint())
            {
                Create();
            }
            
            yield return wait;
        }
    }

    private bool HaveFreeSpawnPoint()
    {
        return _spawnPoints.Any(spawnPoint => spawnPoint.IsFree);
    }
    
    private void Create()
    {
        SpawnPoint spawnPoint = GetSpawnPoint();
        var created = Instantiate(_prefab, spawnPoint.transform);
        
        if (created.TryGetComponent(out IPickable pickable))
        {
            spawnPoint.Occupy(pickable);
        }
    }

    private SpawnPoint GetSpawnPoint()
    {
        int randomPoint;
        
        do
        {
            randomPoint = Random.Range(0, _spawnPoints.Length);
        } while (_spawnPoints[randomPoint].IsFree == false);

        return _spawnPoints[randomPoint];
    }
}
