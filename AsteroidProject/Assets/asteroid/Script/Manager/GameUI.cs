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
        [SerializeField] TMP_Text score, ingameScore;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] Button homeBtn;
        [SerializeField] Image lifeBar;

        protected override void InitSystem()
        {
            base.InitSystem();
            homeBtn.onClick.AddListener(() =>
            {
                var sceneID = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneID);
            });

            ingameScore.text = "";
            gameOverPanel.SetActive(false);
            var ship = FindObjectOfType<SpaceShip>();
            if (ship != null)
            {
                score.text = "Score: " + ship.Score;
                GameMan.OnEndGame.AddListener(() =>
                {
                    score.text = "Score: " + ship.Score;
                    gameOverPanel.SetActive(true);
                    lifeBar.gameObject.SetActive(false);
                });

                ship.OnUpdateScore.AddListener((sc) =>
                {
                    ingameScore.text = "Score " + ship.Score;
                });

                ship.OnDamage.AddListener((dm) =>
                {
                    lifeBar.fillAmount = ship.NormalizedLifeValue;
                });

                ship.OnDeath.AddListener(() =>
                {
                    lifeBar.fillAmount = 0.0f;
                });
            }
        }
    }
}