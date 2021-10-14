using GameplayFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidGame.Actor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace AsteroidGame.Manager
{
    public class GameUI : GameSystem
    {
        [SerializeField] TMP_Text score;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] Button homeBtn;

        protected override void InitSystem()
        {
            base.InitSystem();
            homeBtn.onClick.AddListener(() =>
            {
                var sceneID = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneID);
            });

            gameOverPanel.SetActive(false);
            var ship = FindObjectOfType<SpaceShip>();
            if (ship != null)
            {
                score.text = "Score: " + ship.Score;
                GameMan.OnEndGame.AddListener(() =>
                {
                    score.text = "Score: " + ship.Score;
                    gameOverPanel.SetActive(true);
                });
            }
        }
    }
}