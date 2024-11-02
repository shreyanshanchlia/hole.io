using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    [SerializeField] List<GameObject> player;
    [SerializeField, ReadOnly] int currentLevel;
    [SerializeField, ReadOnly] int currentScore;
    public List<HoleLevelData> holeLevelData;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private Image levelProgress;
    
    [System.Serializable] public struct HoleLevelData
    {
        public int scoreRequirement;
        public float sizeMultiplier;
    }
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        currentScore = 0;
        currentLevel = 1;
        AddScore(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collectable>())
        {
            AddScore(other.GetComponent<Collectable>().scoreValue);
            Destroy(other.gameObject, 0.1f);
        }
    }

    void AddScore(int score)
    {
        currentScore += score;
        CheckScore();
    }

    private void CheckScore()
    {
        int thisLevelScore = holeLevelData[currentLevel - 1].scoreRequirement;
        int nextLevelScore = holeLevelData[currentLevel].scoreRequirement;
        int scoreRequirementForLevel = nextLevelScore - thisLevelScore;
        int scoreToNextLevel = nextLevelScore - currentScore;
        levelProgress.fillAmount = 1 - ((float)scoreToNextLevel / scoreRequirementForLevel);
        scoreText.text = currentScore.ToString();
        if (currentScore >= nextLevelScore)
        {
            currentLevel++;
            foreach (var playerElement in player)
            {
                Vector3 newPlayerScale = playerElement.transform.localScale;
                newPlayerScale.x *= holeLevelData[currentLevel].sizeMultiplier;
                newPlayerScale.y *= holeLevelData[currentLevel].sizeMultiplier;
                playerElement.transform.localScale = newPlayerScale;
            }
            AddScore(0);
        }
    }
}
