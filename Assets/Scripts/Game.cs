using Satellites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _GameEndPanel;

    [SerializeField] private GameObject _field;

    [SerializeField] private GameObject[] _asteroidPrefabs;

    [SerializeField] private GameObject _SatellitePrefab;
    
    [SerializeField] private int _gape;

    private GameObject _asteroidInstance;
    private Asteroid _asteroid;

    private List<GameObject> _satelliteInstances;
    private List<Satellite> _satellites;

    public event EventHandler<StringEventArgs> OnCreatedSatellite;

    void Start()
    {
        NewGame();
        
        _satellites = new List<Satellite>();
        _satelliteInstances = new List<GameObject>();

    }

    public void NewGame()
    {
        _GameEndPanel.SetActive(false);

        int Range = (int)_field.transform.localScale.x / (2 * _gape);

        Vector3 asteroidPosition;

        asteroidPosition.x = UnityEngine.Random.Range(-Range, Range + 1) * _gape;

        Range = (int)_field.transform.localScale.y / (2 * _gape);
        asteroidPosition.y = UnityEngine.Random.Range(-Range, Range + 1) * _gape;

        Range = (int)_field.transform.localScale.z / (2 * _gape);
        asteroidPosition.z = UnityEngine.Random.Range(-Range, Range + 1) * _gape;

        if( _asteroidInstance != null)
        {
            Destroy(_asteroidInstance);
        }

        if(_satelliteInstances != null)
        {
            foreach(GameObject obj in _satelliteInstances)
            {
                Destroy(obj);
            }
        }

        _asteroid = new Asteroid(asteroidPosition.x, asteroidPosition.y, asteroidPosition.z);
        _asteroidInstance = Instantiate(_asteroidPrefabs[UnityEngine.Random.Range(0, _asteroidPrefabs.Length)], asteroidPosition, Quaternion.identity);
        _asteroidInstance.SetActive(false);

    }

    public void CreateSatellite(int x, int y, int z)
    {
        if (_satellites.Any(s => s.X == x && s.Y == y && s.Z == z))
        {
            return;
        }

        _satellites.Add(new Satellite(x, y, z));
        _satelliteInstances.Add(Instantiate(_SatellitePrefab, new Vector3(x, y, z), Quaternion.identity));

        if ((int)_asteroid.GetDistanceTo(_satellites.Last()) == 0)
        {
            _asteroidInstance.SetActive(true);
            _GameEndPanel.SetActive(true);
        }

        CreatedSatellite();
    }

    private void CreatedSatellite()
    {
        OnCreatedSatellite?.Invoke(this, new StringEventArgs($"Satellite #{_satellites.Count}:" + System.Environment.NewLine +
                                                             $"Distance to asteroid {Math.Round(_asteroid.GetDistanceTo(_satellites.Last()), 2)}"));
    }
}
