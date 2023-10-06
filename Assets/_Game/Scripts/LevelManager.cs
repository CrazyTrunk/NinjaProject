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
        DestroyCurrentLevel();
        currentLevelInstance = Instantiate(levelPrefab, spawnLevelPosition.position, Quaternion.identity);
        Transform spawnPoint = currentLevelInstance.transform.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            SetPlayerSpawnPoint(spawnPoint);
            ActivatePlayer();
            SetCameraToPlayer();
        }
    }
    private void DestroyCurrentLevel()
    {
        if (currentLevelInstance)
        {
            Destroy(currentLevelInstance);
        }
    }
    private void ActivatePlayer()
    {
        player.SetActive(true);
    }
    private void SetPlayerSpawnPoint(Transform spawnPoint)
    {
        player.transform.position = spawnPoint.position;
        SetPlayerSavePoint(spawnPoint.position);
    }
    private void SetCameraToPlayer()
    {
        Camera cameraScript = FindObjectOfType<Camera>();
        if (cameraScript != null)
        {
            cameraScript.SetPlayerCamera();
        }
    }
    private void SetPlayerSavePoint(Vector3 newSavePoint)
    {
        Player playerScript = player.GetComponent<Player>();

        if (playerScript != null)
        {
            playerScript.SetSavePointPlayer(newSavePoint);
        }
    }
}
