using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    float speed=-0.1f;
    Transform back_transform;
    float back_size;
    float back_position;
    // Start is called before the first frame update
    void Start()
    {
        back_transform = GetComponent<Transform>();
        back_size = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        back_position += speed * Time.deltaTime;
        back_position = Mathf.Repeat(back_position, back_size);
        back_transform.position = new Vector3(0, back_position, 0);
    }
}
