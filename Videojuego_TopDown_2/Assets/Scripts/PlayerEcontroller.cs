using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEcontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasKey = false;
    public bool hasOil = false;
    public int coinsCollected = 0;
    public int diamondsCollected = 0;

    public GameObject storyPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    private bool isStoryActive = true;
    public TextMeshProUGUI textScore;     
    public TextMeshProUGUI textCoins;      
    public TextMeshProUGUI textDiamonds;   
    public TextMeshProUGUI notificationText;

    void Start()
    {
        Time.timeScale = 1f;
        if (storyPanel != null)
        {
            storyPanel.SetActive(true); 
        }

        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (isStoryActive)
        {
            Time.timeScale = 0f;
            if(Input.GetMouseButtonDown(0))
            {
                if (storyPanel != null)
                {
                    storyPanel.SetActive(false);
                }
                isStoryActive = false;
                Time.timeScale = 1f;
            }
            return;
        }
    
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        int pointsGained = CalculatePointsByTag(other.tag);

        if (pointsGained > 0)
        {
            score += pointsGained;

            if (other.CompareTag("Collectable_1"))
            {
                coinsCollected++;
            }
            else if (other.CompareTag("Collectable_2"))
            {
                diamondsCollected++;
            }

            UpdateUI();
            ShowNotification("+" + pointsGained + " points!");
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log("Tienes la llave");
            ShowNotification("Tienes la llave");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Oil"))
        {
            hasOil = true;
            Time.timeScale = 0f;
            Debug.Log("Game Over");
            ShowNotification("Game Over");
            gameOverPanel.SetActive(true);
            
        }
        
        if (coinsCollected >= 15 && diamondsCollected >= 5 && score >= 25 && hasKey == true && !hasOil)
        {
            Debug.Log("Ganaste!");
            Time.timeScale = 0f;
            ShowNotification("Ganaste!");
            victoryPanel.SetActive(true);
            
        }
    }
    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void UpdateUI()
    {
        if (textScore != null)
        {
            textScore.text = "Score: " + score;
        }
        if (textCoins != null)
        {
            textCoins.text = "Coins: " + coinsCollected + "/15";
        }
        if (textDiamonds != null)
        {
            textDiamonds.text = "Diamonds: " + diamondsCollected + "/5";
        }
            
    }

    void ShowNotification(string message)
    {
        notificationText.text = message;
    }

    int CalculatePointsByTag (string objectTag)
    {
        if (objectTag == "Collectable_1")
        {
            return 1;
        }
       else if (objectTag == "Collectable_2")
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}