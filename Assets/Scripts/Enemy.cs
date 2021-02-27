using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject Egg;
    public Transform EggPosition;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "l_Wall")
        {
            Enemies.direction = 1;
        }
        if (collision.gameObject.tag == "r_Wall")
        {
            Enemies.direction = -1;
        }
        if (collision.gameObject.tag=="Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            player.GetComponent<Player>().IncreaseScore();
            gameObject.GetComponentInParent<Enemies>().DestroyItem(gameObject);
        }
    }

    public void Shot()
    {
        Instantiate(Egg, EggPosition.position, transform.rotation);
    }
}
