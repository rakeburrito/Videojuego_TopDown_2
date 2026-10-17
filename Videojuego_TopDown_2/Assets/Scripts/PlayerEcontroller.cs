using UnityEngine;
using TMPro;

public class PlayerEcontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasKey = false;
    public bool hasOil = false;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI notificationText;

    void Start()
    {

    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log("¡Tienes la llave!");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Key"))
        {
            hasOil = true;
            Debug.Log("¡Game Over! :p");
        }

        if (score >= 5 && hasKey == true && !hasOil)
        {
            Debug.Log("¡Ganaste!");
        }
        else
        {
            Debug.Log("¡Game Over! :p");
        }
    }
}