using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts
{
    public class Botao : MonoBehaviour
    {
        [SerializeField] GameController gameController;
        [SerializeField] private int idButton;

        private void OnMouseDown()
        {
            if (gameController.GameState == GameState.RESPONDER)
                gameController.StartCoroutine(gameController.Responder(idButton));
        }
    }
}