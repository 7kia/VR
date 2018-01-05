using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Code.PlayerInterface;

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

        // Update is called once per frame
        void Update()
        {
            var gameState = gameStateManager.GetGameState();
            switch (gameState)
            {
                case GameStateManager.GameState.Play:
                    actorManager.Play();
                    break;
                case GameStateManager.GameState.Pause:
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
                    break;
            }
        }

        public void SwitchPauseState()
        {
            var gameState = gameStateManager.GetGameState();
            if (gameState == GameStateManager.GameState.Pause)
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
            ResetPlayerWindowStates();
            ClearLevel();
            gameObjectConfigManager.LoadConfig("GameObjects");
            mapLoader.LoadMap("Level");
            actorManager.GeneratePlayer();
            actorManager.playerManager.SetWeaponStorage();
            actorManager.checkActors = true;

            gameStateManager.nowPause = true;
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