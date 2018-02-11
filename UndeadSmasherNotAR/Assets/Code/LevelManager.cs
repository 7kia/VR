﻿using Assets.Code;
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

        public GameStateManager.GameState gameState = GameStateManager.GameState.NotLoad;
        // Use this for initialization
        void Start()
        {
            CreateLevel();
        }

        #region Update

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
                        CheckDistancePlayerDistance();
                        actorManager.Play();
                        break;
                    case GameStateManager.GameState.Pause:
                        actorManager.Pause();
                        break;
                    case GameStateManager.GameState.Defeat:
                        actorManager.Pause();
                        playerWindows.defeatPanel.SetActive(true);
                        playerWindows.SetInteractiveButtons(false);
                        gameState = GameStateManager.GameState.NotLoad;
                        break;
                    case GameStateManager.GameState.Victory:
                        actorManager.Pause();
                        playerWindows.victoryPanel.SetActive(true);
                        playerWindows.SetInteractiveButtons(false);
                        playerWindows.SetAwardValue(actorManager.GetAward());
                        gameState = GameStateManager.GameState.NotLoad;
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

        public void SwitchPauseState()
        {
            gameState = gameStateManager.GetGameState();
            if (gameState == GameStateManager.GameState.Play)
            {
                actorManager.Pause();
                playerWindows.SetPauseState(true);
            }
            else
            {
                actorManager.Play();
                playerWindows.SetPauseState(false);
            }
        }


        public void CreateLevel()
        {
            gameState = GameStateManager.GameState.NotLoad;

            ResetPlayerWindowStates();
            ClearLevel();
            gameObjectConfigManager.LoadConfig("GameObjects");
            mapLoader.LoadMap("Level");
            actorManager.GeneratePlayer();
            actorManager.playerManager.SetWeaponStorage();
            actorManager.FindMagicGenerator();

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

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}