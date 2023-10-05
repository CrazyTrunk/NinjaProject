using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public GameObject levelPrefab;
    [SerializeField] private GameObject currentLevelInstance;
    [SerializeField] private Button levelButton;
    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnLevelPosition;
    private void Awake()
    {
        player.SetActive(false);
    }
    private void OnEnable()
    {
        levelButton.onClick.AddListener(OnLevelButtonClick);
    }
    private void OnDisable()
    {
        levelButton.onClick.RemoveListener(OnLevelButtonClick);
    }
    private void OnLevelButtonClick()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (currentLevelInstance)
        {
            Destroy(currentLevelInstance);
        }
        currentLevelInstance = Instantiate(levelPrefab, spawnLevelPosition.position, Quaternion.identity);
        Transform spawnPoint = currentLevelInstance.transform.Find("SpawnPoint");
        if(spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            SetPlayerSavePoint(spawnPoint.position);
            player.SetActive(true);
            Camera cameraScript = FindObjectOfType<Camera>();
            if (cameraScript != null)
            {
                cameraScript.SetPlayerCamera();
            }
        }
    }
    private void SetPlayerSavePoint(Vector3 newSavePoint)
    {
        Player playerScript = player.GetComponent<Player>(); 

        if (playerScript != null)
        {
            playerScript.savePoint = newSavePoint;
        }
    }
}
