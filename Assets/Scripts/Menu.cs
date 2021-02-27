using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Player;
    public InputField Name;
    public Text Table;
    public Text Score;
    private string path;
    Dictionary<string, int> Records;

    void Start()
    {
        Records = new Dictionary<string, int>();
        path = Application.dataPath + "/Saves/records.txt";
        if (!Directory.Exists(Application.dataPath + "/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        else
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream fs = new FileStream(path, FileMode.Open))
                {
                    if (fs.Length != 0)
                    {
                        try
                        {
                            Records = (Dictionary<string, int>)formatter.Deserialize(fs);
                            UpdateTable();
                        }
                        catch (Exception ex)
                        {
                            Debug.Log(ex.Message);
                        }
                    }
                }
            }
            else
                File.Create(path);
        }
        
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        Score.text = "Score: " + Player.GetComponent<Player>().GetScore();
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveScore()
    {
        if (Name.text.Length != 0)
        {
            try
            {
                if (Records.ContainsKey(Name.text))
                {
                    Records[Name.text] = Player.GetComponent<Player>().GetScore();
                }
                else
                    Records.Add(Name.text, Player.GetComponent<Player>().GetScore());
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, Records);
                }
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        else
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
        UpdateTable();
    }

    void UpdateTable()
    {
        StringBuilder builder = new StringBuilder();
        int n = 1;
        foreach (var item in Records.OrderByDescending(p => p.Value))
        {
            builder.AppendLine($"{n++}. {item.Key}: {item.Value}");
        }
        Table.text = builder.ToString();
    }
}
