using System;
using TMPro;
using UnityEngine;

public class SatellitesListListener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _satellitesList;

    [SerializeField] private Game _game;


    private void Start()
    {
        _game.OnCreatedSatellite += OnCreatedSatellite;
        _game.OnNewGame += OnNewGame;
    }

    private void OnDestroy()
    {
        _game.OnCreatedSatellite -= OnCreatedSatellite;
    }

    private void OnCreatedSatellite(object sender, StringEventArgs e)
    {
        _satellitesList.text += e.Text + System.Environment.NewLine;
    }

    private void OnNewGame(object sender, EventArgs e)
    {
        _satellitesList.text = "";
    }
}
