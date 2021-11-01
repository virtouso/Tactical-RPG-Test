using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnPopup : MonoBehaviour,ITurnPopup
{
    [SerializeField] private TextMeshProUGUI _turnText;
    [SerializeField] private TextMeshProUGUI _moveNumberText;

    [SerializeField] private float _stayAliveTime; 
    
    public IEnumerator ShowTurnMessage(MatchPlayerType playerTurn, int possibleMoves)
    {
        gameObject.SetActive(true);
        _turnText.text = playerTurn.ToString();
        _moveNumberText.text = "Moves:" + possibleMoves;
        yield return new WaitForSeconds(_stayAliveTime);
        gameObject.SetActive(false);
    }
}
