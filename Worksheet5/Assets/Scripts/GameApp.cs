using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using UnityEngine.SceneManagement;

public class GameApp : Singleton<GameApp>
{
  public bool Pause = false;

  void Start()
  {
    SceneManager.LoadScene("Menu");
  }

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape)) GamePause(!Pause);
  }

  public void GamePause(bool _pause){
    Pause = _pause;
    if(Pause){
      Time.timeScale = 0f;
    }
    else{
      Time.timeScale = 1f;
    }
  }
}
