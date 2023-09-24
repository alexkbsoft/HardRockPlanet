using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    private EventBus _eventBus;
    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        // Commment
        _eventBus = EventBus.Instance; 
        _eventBus.PopupText.AddListener(Popup);
        _text.transform.localScale = Vector3.zero;
    }

    public void Popup(string text)
    {
        DelayedMessage(1f, text);
    }

    private IEnumerator DelayedMessage(float delay, string message)
    {
        _text.text = message;
        _text.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(delay);
        _text.transform.DOScale(0f, 0.5f);
    }
}
