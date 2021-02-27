using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public Transform BulletPosition;
    public GameObject Menu;
    public GameObject Bullet;
    public Image[] lives;
    public Text Score;

    public int Health = 3;
    private bool ShotReady = true;
    public float RateOfFire = 0.5f;
    private int ScorePoint = 0;
    float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rigidbody = GetComponent<Rigidbody2D>();
        Score.text = "Score: " + ScorePoint;
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < Health)
                lives[i].enabled = true;
            else
                lives[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Health==0)
        {
            Menu.GetComponent<Menu>().Pause();
        }
        float horizontal = Input.GetAxis("Horizontal");
        rigidbody.MovePosition(rigidbody.position + Vector2.right * horizontal * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ShotReady)
                StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        ShotReady = false;
        Instantiate(Bullet, BulletPosition.position, transform.rotation);
        yield return new WaitForSeconds(RateOfFire);
        ShotReady = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            WasteHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Egg")
        {
            Destroy(collision.gameObject);
            WasteHealth();
        }
    }

    public void IncreaseScore()
    {
        ScorePoint++;
        Score.text = "Score: " + ScorePoint;
    }

    public void WasteHealth()
    {
        Health--;
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < Health)
                lives[i].enabled = true;
            else
                lives[i].enabled = false;
        }
    }

    public int GetScore()
    {
        return ScorePoint;
    }
}

