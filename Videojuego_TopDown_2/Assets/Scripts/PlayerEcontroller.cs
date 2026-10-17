using UnityEngine;
using TMPro;

public class PlayerEcontroller : MonoBehaviour
{
    public float speed = 5f;
    public int score = 0;
    public bool hasDiamond = false;
    public bool hasMud = false;
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

}
