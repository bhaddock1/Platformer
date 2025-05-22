using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject Tutorial;
    public void InitiateGame()
    {
        Tutorial.SetActive(true);
    }

}
