using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : SingletonMonoBehaviour<QuizManager>
{
    [Serializable]
    public class QuizData
    {
        public string QuizText;
        public bool IsCorrect;
        public string AnswerText;
    }
    [SerializeField] private List<QuizData> _quizDatas = new List<QuizData>();

    [SerializeField] private TextMeshProUGUI _quizText;
    [SerializeField] private TextMeshProUGUI _answerText;
    private bool _isAnswer;

    [SerializeField] private QuizDatasText _quizDataText;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in _quizDataText.QuizDatas)
        {
            QuizData data = new QuizData(){QuizText = item.QuizText, IsCorrect = item.IsCorrect, AnswerText = item.QuizAnswerText};
            _quizDatas.Add(data);
        }
        GameController.Instance.QuizTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void CheckAnswer(bool current)
    {
        if (_isAnswer) return;
        _isAnswer = true;
        _answerText.text = _quizDatas[0].AnswerText;
        if (_quizDatas[0].IsCorrect == current)
        {
            //ê≥â
            await GameController.Instance.QuizResult(true);
        }
        else 
        {
            //ïsê≥â
            await GameController.Instance.QuizResult(false);
        }
    }

    public void NewQuiz()
    {
        _quizText.text = _quizDatas[0].QuizText;
    }

    public async void RemoveQuiz()
    {
        _answerText.text = "";
        _quizDatas.RemoveAt(0);
        if (_quizDatas.Count == 0)
        {
            GameController.Instance.EndGame();
            return;
        }
        _isAnswer = false;
        await GameController.Instance.EndQuiz();
    }
}
