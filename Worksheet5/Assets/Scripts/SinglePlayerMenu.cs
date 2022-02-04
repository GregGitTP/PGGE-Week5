using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayerMenu : MonoBehaviour
{
    [SerializeField]
    Button BackBtn;

    void Start()
    {
        BackBtn.onClick.AddListener(
            delegate(){
                OnClickBack();
            }
        );
    }

    private void OnClickBack(){
        SceneManager.LoadScene("Menu");
    }
}
