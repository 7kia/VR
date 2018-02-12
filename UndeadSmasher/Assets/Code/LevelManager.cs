using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Code.PlayerInterface;
using Assets.Code.LevelManagment;
using UnityEngine.UI;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {

        public MapLoader mapLoader;
        public GameObjectConfigManager gameObjectConfigManager;
        public ActorManager actorManager;
        public GameStateManager gameStateManager;
        public PlayerInterface.PlayerInterface playerWindows;

        // Use this for initialization
        void Start()
        {
            CreateLevel();
        }

        #region Update

        // Update is called once per frame
        void Update()
        {
            if ((gameStateManager.gameState == GameStateManager.GameState.Play)
                || (gameStateManager.gameState == GameStateManager.GameState.Pause))
            {
                UpdatePlayerInterface();

                gameStateManager.gameState = gameStateManager.GetGameState();
                switch (gameStateManager.gameState)
                {
                    case GameStateManager.GameState.Play:
                        actorManager.UpdateAndCheckActors();
                        CheckDistancePlayerDistance();
                        break;
                    case GameStateManager.GameState.Pause:
                        SetPauseState();
                        break;
                    case GameStateManager.GameState.Defeat:
                        SetPauseState();
                        playerWindows.SetInteractiveButtons(false);
                        playerWindows.defeatPanel.SetActive(true);
                        gameStateManager.gameState = GameStateManager.GameState.NotLoad;
                        break;
                    case GameStateManager.GameState.Victory:
                        SetPauseState();
                        playerWindows.SetInteractiveButtons(false);
                        playerWindows.victoryPanel.SetActive(true);
                        playerWindows.SetAwardValue(actorManager.GetAward());
                        gameStateManager.gameState = GameStateManager.GameState.NotLoad;
                        break;
                }
            }
            
        }

        private void UpdatePlayerInterface()
        {
            playerWindows.SetHealthBarState(actorManager.GetPlayerHealth(), actorManager.GetPlayerMaxHealth());
            SetPlayerBulletCounterState(PlayerManager.PlayerWeapon.CobbleWeapon);
            SetPlayerBulletCounterState(PlayerManager.PlayerWeapon.BombWeapon);
        }

        // Игрок не может атаковать вблизи крепости
        private void CheckDistancePlayerDistance()
        {

            Vector3 playerPosition = actorManager.playerManager.player.transform.position;
            Vector3 magicGeneratorPosition = actorManager.magicGenerator.transform.position;
            float distance = Vector3.Distance(playerPosition, magicGeneratorPosition);

            if (distance < 5.0f)
            {
                playerWindows.shootButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                playerWindows.shootButton.GetComponent<Button>().interactable = true;
            }
        }

        #endregion


        private void SetPlayerBulletCounterState(PlayerManager.PlayerWeapon weapon)
        {
            playerWindows.SetBulletCounterState(
                actorManager.GetPlayerBulletCount(weapon),
                weapon
            );
        }

        #region PauseState
        public void SwitchPauseState()
        {
            gameStateManager.gameState = gameStateManager.GetGameState();
            if (gameStateManager.gameState == GameStateManager.GameState.Play)
            {
                SetPauseState();
            }
            else
            {
                SetPlayState();
            }
        }

        private void SetPauseState()
        {
            playerWindows.SetPauseState(true);

            gameStateManager.gameState = GameStateManager.GameState.Pause;
        }

        private void SetPlayState()
        {
            playerWindows.SetPauseState(false);

            gameStateManager.gameState = GameStateManager.GameState.Play;
        }
        #endregion

        public void CreateLevel()
        {
            gameStateManager.gameState = GameStateManager.GameState.NotLoad;

            ResetPlayerWindowStates();
            ClearLevel();

            gameObjectConfigManager.LoadConfig("GameObjects");
            mapLoader.LoadMap("Level");

            RecreatePlayer();

            actorManager.FindMagicGenerator();

            gameStateManager.gameState = GameStateManager.GameState.Pause;
            playerWindows.SetPauseState(false);
        }

        private void RecreatePlayer()
        {
            actorManager.GeneratePlayer();
            actorManager.playerManager.SetWeaponStorage();
        }

        private void ResetPlayerWindowStates()
        {
            playerWindows.ResetPlayerWindowStates();
            playerWindows.SetInteractiveButtons(true);
        }

        private void ClearLevel()
        {
            actorManager.ClearScene();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}