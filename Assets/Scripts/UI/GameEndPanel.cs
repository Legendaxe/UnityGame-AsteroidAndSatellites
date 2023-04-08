using System;
using UnityEngine;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private Game _game;
    private void Start() 
    {
        _game.OnGameVictory += OnGameVictory;
        _game.OnNewGame += OnNewGame;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _game.OnGameVictory -= OnGameVictory;
        _game.OnNewGame -= OnNewGame;    
    }




    private void OnNewGame(object sender, EventArgs e) 
    {
        gameObject.SetActive(false);
    }

    private void OnGameVictory(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }

    
}
