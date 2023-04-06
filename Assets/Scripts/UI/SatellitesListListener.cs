using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SatellitesListListener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _satellitesList;

    [SerializeField] private Game _game;


    private void Start()
    {
        _game.OnCreatedSatellite += OnCreatedSatellite;
    }

    private void OnDestroy()
    {
        _game.OnCreatedSatellite -= OnCreatedSatellite;
    }

    private void OnCreatedSatellite(object sender, StringEventArgs e)
    {
        _satellitesList.text += e.Text + System.Environment.NewLine;
    }
}
