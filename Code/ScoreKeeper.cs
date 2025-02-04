using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private static float score;  // everyone has the same score
    private static Text scoreText; // everyone has the same text
    public GameObject youWin;

    // Use this for initialization
    internal void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateText();

    }

    private void Update()
    {
        if (score == 10)
        {
            EndGame();
            Instantiate(youWin, new Vector3(0, 0, 0), Quaternion.identity);
            score = score + 1;
        }

    }

    public static void AddToScore(float points)
    {
        score += points;
        UpdateText();
    }


    public static void EndingAddToScore(float points)
    {
        score += points;
        FinalUpdate();
    }


    private static void UpdateText()
    {
        scoreText.text = String.Format("GET 10 TO SURVIVE: {0}", score);
    }


    public static void FinalUpdate()
    {
        scoreText.color = Color.red;
        scoreText.text = String.Format("Score: {0}", score);
    }



    private void DestroyGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }
    }


    public void EndGame()
    {
        // Find and delete game objects with specific tags
        DestroyGameObjectsWithTag("Player");
        DestroyGameObjectsWithTag("Monster");
        DestroyGameObjectsWithTag("Bean");
        DestroyGameObjectsWithTag("Laser");

    }




}
