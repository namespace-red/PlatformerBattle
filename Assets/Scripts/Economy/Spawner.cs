using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _pointsParent;
    [SerializeField] private float _coolDown;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _pool;
    
    private Transform[] _points;
    
    private void Awake()
    {
        InitPoints();
    }

    private void OnEnable()
    {
        StartCoroutine(Run());
    }
    
    private void InitPoints()
    {
        _points = new Transform[_pointsParent.childCount];

        for (int i = 0; i < _pointsParent.childCount; i++)
            _points[i] = _pointsParent.GetChild(i).transform;
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
        int randomPoint = Random.Range(0, _points.Length);
        return _points[randomPoint].position;
    }
}
