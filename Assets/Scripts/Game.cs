using Satellites;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [FormerlySerializedAs("_field")] [SerializeField] private GameObject field;

    [FormerlySerializedAs("_asteroidPrefabs")] [SerializeField] private GameObject[] asteroidPrefabs;

    [FormerlySerializedAs("_SatellitePrefab")] [SerializeField] private GameObject satellitePrefab;
    
    [FormerlySerializedAs("_gape")] [SerializeField] private int gape;

    private GameObject _asteroidInstance;
    private Asteroid _asteroid;

    private List<GameObject> _satelliteInstances;
    private List<Satellite> _satellites;
    private Vector3 _fieldLocalScale;

    public event EventHandler<StringEventArgs> OnCreatedSatellite;
    public event EventHandler OnGameVictory;
    public event EventHandler OnNewGame;

    void Start()
    {
        _satellites = new List<Satellite>();
        _satelliteInstances = new List<GameObject>();

        _fieldLocalScale = field.transform.localScale;
        
        NewGame();
    }

    public void NewGame()
    {

        int range = (int)field.transform.localScale.x / (2 * gape);

        Vector3 asteroidPosition;

        asteroidPosition.x = UnityEngine.Random.Range(-range, range + 1) * gape;

        range = (int)_fieldLocalScale.y / (2 * gape);
        asteroidPosition.y = UnityEngine.Random.Range(-range, range + 1) * gape;

        range = (int)_fieldLocalScale.z / (2 * gape);
        asteroidPosition.z = UnityEngine.Random.Range(-range, range + 1) * gape;

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
            _satelliteInstances.Clear();
        }

        _satellites.Clear();
        

        _asteroid = new Asteroid(asteroidPosition.x, asteroidPosition.y, asteroidPosition.z);
        _asteroidInstance = Instantiate(asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)], asteroidPosition, Quaternion.identity);
        _asteroidInstance.SetActive(false);
        
        OnNewGame?.Invoke(this, EventArgs.Empty);
    }

    public void CreateSatellite(int x, int y, int z)
    {
        if (_satellites.Any(s => (int)s.X == x && (int)s.Y == y && (int)s.Z == z))
        {
            return;
        }

        _satellites.Add(new Satellite(x, y, z));
        _satelliteInstances.Add(Instantiate(satellitePrefab, new Vector3(x, y, z), Quaternion.identity));

        if ((int)_asteroid.GetDistanceTo(_satellites.Last()) == 0)
        {
            GameVictory();
        }

        CreatedSatellite();
    }

    private void CreatedSatellite()
    {
        OnCreatedSatellite?.Invoke(this, new StringEventArgs(
            $"Satellite #{_satellites.Count}:{Environment.NewLine}Distance to asteroid {Math.Round(_asteroid.GetDistanceTo(_satellites.Last()), 2)}"));
    }

    private void GameVictory()
    {
        _asteroidInstance.SetActive(true);
        OnGameVictory?.Invoke(this, EventArgs.Empty);
    }
}
