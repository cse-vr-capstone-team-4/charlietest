using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.MagicLeap;
using UnityEngine;


public class CombatController : MonoBehaviour
{
    private MLInputController _controller;
    // private bool _enabled = false;   // Enable is for the start of the whole game
    //private bool _trigger = false;
    public static int soldierCount = 0;
    public static int enemyCount = 0;
    private GameObject Island;

    public GameObject playerObject1;
    public GameObject playerObject2;
    public GameObject playerObject3;
    private GameObject[] soldierList;
    public GameObject enemyObject;
    public static int gold;

    void Awake()
    {
        MLInput.Start();
        MLInput.OnControllerButtonUp += OnButtonUp;
        _controller = MLInput.GetController(MLInput.Hand.Left);

        soldierList = new GameObject[3];
        soldierList[0] = playerObject1;
        soldierList[1] = playerObject2;
        soldierList[2] = playerObject3;
        EnemySpawn(2, GameObject.Find("GameObject3"));
    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    void EnemySpawn(int n, GameObject enemyIsland) {
        for (int i = 0; i < n; i++)
        {
            GameObject newEnemy = Instantiate(enemyObject) as GameObject;
            newEnemy.transform.parent = enemyIsland.transform;
            newEnemy.transform.localPosition = new Vector3(-1 * i, 0, 4);
            newEnemy.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            enemyCount += 1;
        }
    } 

    void Update()
    {
        Island = MagicLeap.VirtualPointer.island;
        CheckControl();
        if (enemyCount == 0) {
            //EnemySpawn(2);
        }
    }

    void CheckControl()
    {

    }

    // Home button to spawn soldier
    void OnButtonUp(byte controller_id, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.HomeTap && soldierCount < 3)
        {
            // spawn new soldier
            GameObject newSoldier = Instantiate(soldierList[soldierCount % 3]) as GameObject;
            soldierCount = soldierCount + 1;

            // at some position of the island

            newSoldier.transform.parent = Island.transform;

            newSoldier.transform.localPosition = new Vector3((soldierCount % 3) * (-1) + 1, 0, 0);
            newSoldier.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            //_trigger = false;

        }
    }

}
