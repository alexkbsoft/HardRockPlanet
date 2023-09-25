using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NarrativeSO : MonoBehaviour
{
    public string[] NarrativeStrings;
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _portrait;
    [SerializeField] private GameObject _info;
    [SerializeField] private float _textShowDelay;
    private EventBus _eventBus;
    private int _currentNarrationStep = 1;

    void Start()
    {
        _eventBus = EventBus.Instance;
        _textContainer.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(DelayedMessage(5f, NarrativeStrings[0]));
        StartCoroutine(DelayedMessage(15f, "Используй WASD для перемещения, ПКМ для прицеливания и ЛКМ стрельбы. \r\n Нажми Е чтобы начать бурить, нажми F чтобы открыть меню улучшений", false));
        StartCoroutine(DelayedMessage(25f, "Стрелки указывают на ближайшие месторождения, чем больше и зеленнее стрелка - тем ближе", false));
        
        //for (int i = 1; i < 11; i++)
        //{
        //    StartCoroutine(DelayedMessage(15f+i*10f, NarrativeStrings[i]));
        //}
        _eventBus.EnterMine.AddListener(onEnterMine);
    }

    private void onEnterMine(Mine mine)
    {
        if (_currentNarrationStep>= NarrativeStrings.Length) {
            return;
        }

        StartCoroutine(DelayedMessage(1f, NarrativeStrings[_currentNarrationStep]));
        _currentNarrationStep++;
    }

    private IEnumerator DelayedMessage(float delay, string message, bool showPortrait = true)
    {
        yield return new WaitForSeconds(delay);
        if (showPortrait) 
        {
            _portrait.SetActive(true);
            _info.SetActive(false);
        }
        else
        {
            _portrait.SetActive(false);
            _info.SetActive(true);
        }
        _text.text = message;
        _textContainer.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(_textShowDelay);
        _textContainer.transform.DOScale(0f, 0.5f);
    }


}
