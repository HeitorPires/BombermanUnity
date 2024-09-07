using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _buttonLevel1;
    private Button _buttonLevel2;
    private Slider _slider;

    [SerializeField]private PowerUpSpawnerSO _powerUpSpawner;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _buttonLevel1 = _document.rootVisualElement.Q("Level1") as Button;
        _buttonLevel2 = _document.rootVisualElement.Q("Level1") as Button;
        _slider = _document.rootVisualElement.Q("PowerUpSpawnChance") as Slider;

        _buttonLevel1.RegisterCallback<ClickEvent>(evt => OnButtonLevelClick(1));
        _buttonLevel2.RegisterCallback<ClickEvent>(evt => OnButtonLevelClick(2));

        _slider.value = _powerUpSpawner.SpawnChance*100;

    }

    private void OnDisable()
    {
        _buttonLevel1.UnregisterCallback<ClickEvent>(evt => OnButtonLevelClick(1));
        _buttonLevel2.UnregisterCallback<ClickEvent>(evt => OnButtonLevelClick(2));
    }

    private void OnButtonLevelClick(int i)
    {
        _powerUpSpawner.SpawnChance = _slider.value/100;
        SceneManager.LoadScene(i);
    }
}
