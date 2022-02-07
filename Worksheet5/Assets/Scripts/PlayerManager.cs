using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    string PlayerPrefabName;
    [SerializeField]
    Transform Camera;
    [SerializeField]
    Text AmmoTxt;
    [SerializeField]
    List<Transform> SpawnPoints = new List<Transform>();

    private void Start(){
        Transform SpawnPoint = GetRandomSpawn();

        GameObject player = PhotonNetwork.Instantiate(PlayerPrefabName, SpawnPoint.position, SpawnPoint.rotation, 0);

        player.GetComponent<Player>().camera = Camera;
        player.GetComponent<Player>().ammoTxt = AmmoTxt;
    }

    private Transform GetRandomSpawn(){
        int num = Random.Range(0, SpawnPoints.Count);
        return SpawnPoints[num];
    }
}
