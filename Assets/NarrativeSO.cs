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

    void Start()
    {
        _textContainer.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(DelayedMessage(5f, NarrativeStrings[0]));
        StartCoroutine(DelayedMessage(15f, "Используй WASD для перемещения, мышь и ЛКМ для прицеливания и стрельбы. Нажми Е чтобы начать бурить, нажми F чтобы открыть меню улучшений",false));
        for (int i = 1; i < 11; i++)
        {
            StartCoroutine(DelayedMessage(15f+i*10f, NarrativeStrings[i]));
        }
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
