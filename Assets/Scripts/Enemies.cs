using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public static int direction = 1;
    public List<GameObject> objects;
    public GameObject Menu;
    public float Period=1;
    private bool ShotReady = true;
    public int ShootingVal = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(objects.Count==0)
            Menu.GetComponent<Menu>().Pause();
        transform.Translate(Vector2.down * Time.deltaTime * 0.05f);
        transform.Translate(Vector2.right * direction * Time.deltaTime * 0.3f);
        if (ShotReady)
            StartCoroutine(Shooting());
    }

    public void DestroyItem(GameObject enemy)
    {
        objects.Remove(enemy);
    }

    private IEnumerator Shooting()
    {
        ShotReady = false;
        for (int i = 0; i < ShootingVal; i++)
        {
            objects[Random.Range(0, objects.Count)].GetComponent<Enemy>().Shot();
        }
        yield return new WaitForSeconds(Period);
        ShotReady = true;
    }
}
