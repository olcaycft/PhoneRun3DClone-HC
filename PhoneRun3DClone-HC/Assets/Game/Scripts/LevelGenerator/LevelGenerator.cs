using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.LevelGenerator
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("StartPoint")] 
        [SerializeField] private GameObject patternBase;

        [Header("PatternListAndPrefabs")] 
        [SerializeField] private List<Pattern> patterns;

        [Header("Index Settings")] 
        [SerializeField] private int startPatternIndex;

        [SerializeField] private int gatePatternIndex;

        [SerializeField] private int miniGamePatternIndex;


        [Header("PatternCount")] 
        [SerializeField] private int patternCount;
        [Header("Gate Pattern Settings")]
        
        [SerializeField] private int gateStartPoint;
        [SerializeField] private int gateCount;
        [SerializeField] private int gateSpawnFrequency;
        private int gateCountFlag = 0;
        
        private GameObject pattern;
        private Vector3 nextPatternStartPoint;
        private int randomPatternIndex;
        private int gatePatternCount => patterns[tag.CompareTo("Gate")].prefab.Count;

        private void Awake()
        {
            GeneratePattern();
        }


        private void GeneratePattern()
        {
            pattern = Instantiate(patterns[startPatternIndex]
                .prefab[0], nextPatternStartPoint, Quaternion.identity);
            pattern.transform.parent = patternBase.transform;
            nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;

            for (int i = 0; i < patternCount; i++)
            {
                if (gateStartPoint == i && gateCountFlag <= gateCount)
                {
                    pattern = Instantiate(patterns[gatePatternIndex]
                            .prefab[Random.Range(0, gatePatternCount)],
                        nextPatternStartPoint, Quaternion.identity);
                    pattern.transform.parent = patternBase.transform;
                    nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;

                    gateStartPoint = Random.Range(gateStartPoint + gateSpawnFrequency, patternCount - 3);

                    gateCountFlag++;
                }

                pattern = Instantiate(
                    patterns[randomPatternIndex].prefab[Random.Range(0, patterns[randomPatternIndex].prefab.Count)],
                    nextPatternStartPoint, Quaternion.identity);
                pattern.transform.parent = patternBase.transform;
                nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;
            }

            pattern = Instantiate(patterns[miniGamePatternIndex]
                .prefab[0], nextPatternStartPoint, Quaternion.identity);
            pattern.transform.parent = patternBase.transform;
            nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;
        }
    }


    [System.Serializable]
    public class Pattern
    {
        public string tag;
        public List<GameObject> prefab;
    }
}