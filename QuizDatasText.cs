using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizDatas", menuName = "ScriptableObjects/QuizDatas", order = 1)]
public class QuizDatasText : ScriptableObject
{
    [SerializeField] public List<QuizData> QuizDatas;
}
[Serializable]
public class QuizData
{
    public string QuizText;
    public bool IsCorrect;
    public string QuizAnswerText;
}