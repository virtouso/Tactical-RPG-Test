using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class EndPopup : MonoBehaviour,IEndPopup
{
   [SerializeField] private TextMeshProUGUI _winnerText;


   public void ShowFinalMessage(MatchPlayerType winner)
   {
      gameObject.SetActive(true);
      _winnerText.text = "Winner:" + winner.ToString();
   }
   
}
