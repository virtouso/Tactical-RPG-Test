using System.Collections;
using System.Collections.Generic;
using Panzers.Reference;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Panzers.UI
{
   public class EndPopup : MonoBehaviour, IEndPopup
   {
      [SerializeField] private TextMeshProUGUI _winnerText;
      [SerializeField] private Button _returnButton;

      public void ShowFinalMessage(MatchPlayerType winner)
      {
         gameObject.SetActive(true);
         _winnerText.text = "Winner:" + winner.ToString();
      }


      private void ReturnToMainMenu()
      {
         SceneManager.LoadScene(SceneNames.MenuScene);
      }


      private void Start()
      {
         _returnButton.onClick.AddListener(ReturnToMainMenu);
      }
   }
}