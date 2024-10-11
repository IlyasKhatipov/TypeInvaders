using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Enemy targetedEnemy; 
    private string inputWord = ""; 
    public Transform attackPoint; 
    public GameObject letterPrefab; 

    void Update()
    {
        MovePlayer();
        RotateTowardsTarget(); 
        if (Input.anyKeyDown && targetedEnemy != null)
        {
            foreach (char c in Input.inputString)
            {
                inputWord += c.ToString();
                ShootLetter(c.ToString());
            }
        }
    }

    void MovePlayer()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);
    }

    void RotateTowardsTarget()
    {
        if (targetedEnemy != null)
        {
            Vector3 direction = (targetedEnemy.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, speed*3 * Time.deltaTime);
        }
    }

    void ShootLetter(string letter)
    {
        if (letterPrefab != null)
        {
            GameObject letterObj = Instantiate(letterPrefab, attackPoint.position, Quaternion.identity);
            letterObj.GetComponent<Letter>().Initialize(letter, targetedEnemy);
        }
    }

    public void TargetEnemy(Enemy enemy)
    {
        targetedEnemy = enemy;
    }
}
