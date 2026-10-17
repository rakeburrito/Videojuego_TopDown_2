using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEcontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasKey = false;
    public bool hasOil = false;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI notificationText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTextScore();
    }

    // Update is called once per frame
    void Update()
    {
    
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
        transform.Translate(direction * speed * Time.deltaTime);

    }

    //Se ejecuta cuando se toque el collectable
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        int pointsGained = CalculatePointsByTag(other.tag);

        if (pointsGained > 0)
        {
            score += pointsGained;
            UpdateTextScore();
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
            Debug.Log("Game Over");
            ShowNotification("Game Over");
            
            Invoke("RestartLevel", 1f);
        }
        
        if (score >= 10 && hasKey == true && !hasOil)
        {
            Debug.Log("Ganaste!");
            ShowNotification("Ganaste!");
        }
        
    }
    void RestartLevel()
    {
        // Recarga la escena actual en ejecución
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void UpdateTextScore()
    {
        textScore.text = "Score: " + score + "/10";
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