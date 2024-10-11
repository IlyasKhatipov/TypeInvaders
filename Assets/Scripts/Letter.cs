using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public float speed = 10f; 
    private string letter;
    public TextMeshProUGUI textField;
    private Enemy target; 
    private Vector3 targetDirection; 

    public void Initialize(string letter, Enemy target)
    {
        this.letter = letter;
        textField.text = letter;
        this.target = target;
        targetDirection = (target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(targetDirection * speed * Time.deltaTime);
        if (transform.position.magnitude > 100f)
        {
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null && enemy == target)
            {
                enemy.TakeDamage(letter);
                Destroy(gameObject);
            }
        }
    }
}
