using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button SinglePlayerBtn;
    [SerializeField]
    Button MultiPlayerBtn;

    private void Start(){
        SinglePlayerBtn.onClick.AddListener(
            delegate(){
                OnClickSinglePlayer();
            }
        );
        MultiPlayerBtn.onClick.AddListener(
            delegate(){
                OnClickMultiPlayer();
            }
        );
    }

    private void OnClickSinglePlayer(){
        SceneManager.LoadScene("SinglePlayer");
    }

    private void OnClickMultiPlayer(){
        SceneManager.LoadScene("MultiPlayerLauncher");
    }
}
