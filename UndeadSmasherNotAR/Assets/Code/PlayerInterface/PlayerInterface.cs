﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.PlayerInterface
{
    public class PlayerInterface : MonoBehaviour
    {
        public GameObject defeatPanel;
        public GameObject victoryPanel;

        public GameObject pauseButton;
        public GameObject cobbleWeaponButton;
        public GameObject bombWeaponButton;
        public GameObject shootButton;

        public void ResetPlayerWindowStates()
        {
            defeatPanel.active = false;
            victoryPanel.active = false;
        }

        public void SetPauseState(bool pause)
        {
            cobbleWeaponButton.GetComponent<Button>().interactable = !pause;
            bombWeaponButton.GetComponent<Button>().interactable = !pause;
            shootButton.GetComponent<Button>().interactable = !pause;
        }

        public void SetInteractiveButtons(bool interactive)
        {
            pauseButton.GetComponent<Button>().interactable = interactive;
            cobbleWeaponButton.GetComponent<Button>().interactable = interactive;
            bombWeaponButton.GetComponent<Button>().interactable = interactive;
            shootButton.GetComponent<Button>().interactable = interactive;
        }
        //public GameObject pausePanel;
    }
}
