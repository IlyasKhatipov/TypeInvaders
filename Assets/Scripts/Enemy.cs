using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private string[] randomWords;
    private string currentWord;
    private bool[] coloredLetters;
    public float speed = 2f;
    public GameManager gameManager;

    private TouchScreenKeyboard keyboard;
    private string previousInput = "";  // Предыдущее состояние текста

    void Start()
    {
        LoadWordsFromFile();
        currentWord = GetRandomWord();
        textField.text = currentWord;
        coloredLetters = new bool[currentWord.Length];
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Проверка, есть ли открытая клавиатура на мобильной платформе
        if (keyboard != null && keyboard.active)
        {
            // Если введён текст изменился
            if (keyboard.text.Length > previousInput.Length)
            {
                // Берём последнюю введённую букву
                char lastLetter = keyboard.text[keyboard.text.Length - 1];
                ProcessInput(lastLetter.ToString());
            }

            previousInput = keyboard.text;  // Обновляем предыдущее состояние
        }
    }

    void LoadWordsFromFile()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("words");
        if (wordFile != null)
        {
            randomWords = wordFile.text.Split('\n');
            for (int i = 0; i < randomWords.Length; i++)
            {
                randomWords[i] = randomWords[i].Trim();
            }
        }
        else
        {
            randomWords = new string[] { "Backup", "Default", "Fallback" };
        }
    }

    string GetRandomWord()
    {
        return randomWords[Random.Range(0, randomWords.Length)];
    }

    void OnMouseDown()
    {
        // Открываем клавиатуру на мобильной платформе
        if (Application.isMobilePlatform)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            previousInput = ""; // Сбрасываем предыдущее состояние текста
        }
        else
        {
            FindObjectOfType<PlayerController>().TargetEnemy(this);
        }
    }

    void ProcessInput(string input)
    {
        foreach (char letter in input)
        {
            TakeDamage(letter.ToString());
        }
    }

    public void TakeDamage(string letter)
    {
        for (int i = 0; i < currentWord.Length; i++)
        {
            if (!coloredLetters[i] && currentWord[i].ToString().Equals(letter, System.StringComparison.OrdinalIgnoreCase))
            {
                coloredLetters[i] = true;
                UpdateTextWithColor();
                break;
            }
        }
    }

    void UpdateTextWithColor()
    {
        string coloredWord = "";
        bool allLettersColored = true;

        for (int i = 0; i < currentWord.Length; i++)
        {
            if (coloredLetters[i])
            {
                coloredWord += $"<color=#FFFF00>{currentWord[i]}</color>";
            }
            else
            {
                coloredWord += currentWord[i];
                allLettersColored = false;
            }
        }

        textField.text = coloredWord;

        if (allLettersColored)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        gameManager.UpdateScores();
    }
}
