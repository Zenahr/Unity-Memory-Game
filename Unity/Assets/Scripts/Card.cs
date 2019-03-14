using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public static bool DO_NOT = false;

    [SerializeField]
    private enum State { start, end };
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private bool _initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFace;
    
    private GameObject _manager;
    private State _state;

    private void Start()
    {
        _state = State.start;
        _manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void setupGraphics()
    {
        _cardBack = _manager.GetComponent<GameManager>().getCardBack();
        _cardFace = _manager.GetComponent<GameManager>().getCardFace(_cardValue);
        flipCard();
    }

    public void flipCard()
    {
        if (_state == State.start)
            _state = State.end;
        else
            _state = State.start;

        if (_state == State.end && !DO_NOT)
            GetComponent<Image>().sprite = _cardBack;

        else if (_state == State.start && !DO_NOT)
            GetComponent<Image>().sprite = _cardFace;
    }

    public int cardValue {

        get => _cardValue;
        set { _cardValue = value; }

    }
    
    public bool initialized
    {
        get => _initialized;
        set { _initialized = value; }
    }

    public void falseCheck()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        if (_state == State.end)
            GetComponent<Image>().sprite = _cardBack;
        else if (_state == State.start)
            GetComponent<Image>().sprite = _cardFace;
        DO_NOT = false;
    }
}
