using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController>
{
    [SerializeField] private GameObject _quizObject;
    public static bool IsQuiz;
    [SerializeField] private GameObject _resultObject;
    [SerializeField] private GameObject _currentObject;
    [SerializeField] private GameObject _notCurrentObject;
    [SerializeField] private GameObject _endObject;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private TextMeshProUGUI _currectNumText;
    [SerializeField] private TextMeshProUGUI _notCurrectNumText;
    private int _currectNum;
    private int _notCurrectNum;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuiz()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
        _startButton.SetActive(false);
        _quizObject.SetActive(true);
    }

    public void QuizTime()
    {
        IsQuiz = true;
        _quizObject.SetActive(true);
        QuizManager.Instance.NewQuiz();
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    public async UniTask EndQuiz()
    {
        IsQuiz = false;
        _quizObject.SetActive(false);
        await UniTask.WaitForSeconds(1.0f);
        QuizTime();
    }

    public async UniTask QuizResult(bool result)
    {
        _resultObject.SetActive(true);
        if (result)
        {
            _currectNum++;
            _audioSource.PlayOneShot(_audioClips[2]);
        }
        else 
        {
            _notCurrectNum++;
            _audioSource.PlayOneShot(_audioClips[3]);
        }
        _currentObject.SetActive(result);
        _notCurrentObject.SetActive(!result);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));

        _resultObject.SetActive(false);
        QuizManager.Instance.RemoveQuiz();
    }

    public void EndGame()
    {
        _quizObject.SetActive(false);
        _endObject.SetActive(true);
        _currectNumText.text = _currectNum.ToString();
        _notCurrectNumText.text= _notCurrectNum.ToString();
    }
}
