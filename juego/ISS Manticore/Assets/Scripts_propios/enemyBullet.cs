using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 2.0f;
    public bool active;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            PlayerStats.Instance.TakeDamage(1.0f);
            if (PlayerStats.Instance.Health == 0)
            {
                GameOverMenu.Instance.Display();
            }
            Debug.Log("Collided with player");
        }
        Destroy(gameObject);
    }
}