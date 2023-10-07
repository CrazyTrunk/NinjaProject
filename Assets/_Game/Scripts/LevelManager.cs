using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public LevelItemScriptableObject scriptableObject;
    [SerializeField] public Transform buttonsParent;
    [SerializeField] public LevelButton buttonPrefab;
    public static LevelManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SpawnButtons();
    }
    private void SpawnButtons()
    {
        foreach (LevelItem item in scriptableObject.LevelItems)
        {
            LevelButton levelButton = Instantiate(buttonPrefab, buttonsParent);
            levelButton.name = $"Level {item.Id}";
            levelButton.SetData(item.Id);
        }
    }
    public void DestroyLevelManager()
    {
        Destroy(gameObject);
    }
}
