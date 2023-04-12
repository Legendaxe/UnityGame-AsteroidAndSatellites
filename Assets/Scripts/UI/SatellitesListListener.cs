using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SatellitesListListener : MonoBehaviour
{
    [FormerlySerializedAs("_satellitesList")] [SerializeField] private TextMeshProUGUI satellitesList;

    [FormerlySerializedAs("_game")] [SerializeField] private Game game;


    private void Start()
    {
        game.OnCreatedSatellite += OnCreatedSatellite;
        game.OnNewGame += OnNewGame;
    }

    private void OnDestroy()
    {
        game.OnCreatedSatellite -= OnCreatedSatellite;
    }

    private void OnCreatedSatellite(object sender, StringEventArgs e)
    {
        satellitesList.text += e.Text + System.Environment.NewLine;
    }

    private void OnNewGame(object sender, EventArgs e)
    {
        satellitesList.text = "";
    }
}
