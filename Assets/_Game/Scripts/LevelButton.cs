using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] public Button levelButton;
    [SerializeField] public TextMeshProUGUI textLevelButton;
    // Start is called before the first frame update

    public void SetData(int id)
    {
        textLevelButton.text = $"Level {id}";
        levelButton.onClick.AddListener(() =>
        {
            (bool isLoadSuccess , Transform spawnPosition) = LoadResourceLevel(id);
            if (isLoadSuccess)
            {
                LevelManager.Instance.DestroyLevelManager();
                GameObject player = GameObject.Find("Player");
                if (player != null && spawnPosition != null)
                {
                    player.transform.position = spawnPosition.position;
                }
            }
        });
    }
    private (bool, Transform) LoadResourceLevel(int levelId)
    {
        GameObject levelPrefab = Resources.Load<GameObject>($"Level{levelId}");
        if (levelPrefab != null)
        {
            GameObject currentLevel = Instantiate(levelPrefab);
            Transform spawnPoint = currentLevel.transform.Find("SpawnPoint");
            return (true, spawnPoint);
        }

        Debug.Log("This Level Is Underdevelopment");
        return (false, null);
    }
}
