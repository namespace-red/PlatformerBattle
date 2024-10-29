using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _coolDown;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _pool;
    
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
