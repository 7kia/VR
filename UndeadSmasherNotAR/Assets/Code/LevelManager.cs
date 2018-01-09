using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Code.PlayerInterface;
using Assets.Code.LevelManagment;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {

        public MapLoader mapLoader;
        public GameObjectConfigManager gameObjectConfigManager;
        public ActorManager actorManager;
        public GameStateManager gameStateManager;
        public PlayerInterface.PlayerInterface playerWindows;

        public GameStateManager.GameState gameState = GameStateManager.GameState.NotLoad;
        // Use this for initialization
        void Start()
        {
            CreateLevel();
        }

        // Update is called once per frame
        void Update()
        {
            if ((gameState != GameStateManager.GameState.Defeat)
                && (gameState != GameStateManager.GameState.Victory)
                && (gameState != GameStateManager.GameState.NotLoad))
            {
                UpdatePlayerInterface();

                gameState = gameStateManager.GetGameState();
                switch (gameState)
                {
                    case GameStateManager.GameState.Play:
                        gameStateManager.nowPause = false;
                        actorManager.Play();
                        break;
                    case GameStateManager.GameState.Pause:
                        gameStateManager.nowPause = true;
                        actorManager.Pause();
                        break;
                    case GameStateManager.GameState.Defeat:
                        actorManager.Pause();
                        playerWindows.defeatPanel.SetActive(true);
                        playerWindows.SetInteractiveButtons(false);
                        break;
                    case GameStateManager.GameState.Victory:
                        actorManager.Pause();
                        playerWindows.victoryPanel.SetActive(true);
                        playerWindows.SetInteractiveButtons(false);
                        playerWindows.SetAwardValue(actorManager.GetAward());
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

        private void SetPlayerBulletCounterState(PlayerManager.PlayerWeapon weapon)
        {
            playerWindows.SetBulletCounterState(
                actorManager.GetPlayerBulletCount(weapon),
                weapon
            );
        }

        public void SwitchPauseState()
        {
            gameState = gameStateManager.GetGameState();
            if (gameState == GameStateManager.GameState.Play)
            {
                actorManager.Pause();
                playerWindows.SetPauseState(true);
                gameStateManager.nowPause = true;
            }
            else
            {
                actorManager.Play();
                playerWindows.SetPauseState(false);
                gameStateManager.nowPause = false;
            }
        }

        public void CreateLevel()
        {
            gameState = GameStateManager.GameState.NotLoad;

            ResetPlayerWindowStates();
            ClearLevel();
            gameObjectConfigManager.LoadConfig("GameObjects");
            mapLoader.LoadMap("Level");
            actorManager.FindMagicGenerator();
            actorManager.GeneratePlayer();
            actorManager.playerManager.SetWeaponStorage();
            actorManager.checkActors = true;

            gameStateManager.nowPause = false;

            gameState = GameStateManager.GameState.Play;
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

        public uint GetAward()
        {

            return 0;
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}