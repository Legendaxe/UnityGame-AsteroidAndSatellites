using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEndPanel : MonoBehaviour
{
    [FormerlySerializedAs("_game")] [SerializeField] private Game game;
    private void Start() 
    {
        game.OnGameVictory += OnGameVictory;
        game.OnNewGame += OnNewGame;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        game.OnGameVictory -= OnGameVictory;
        game.OnNewGame -= OnNewGame;    
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
