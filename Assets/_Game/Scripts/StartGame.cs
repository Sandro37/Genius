using UnityEngine;
namespace Assets._Game.Scripts
{
    class StartGame : MonoBehaviour
    {
        [SerializeField] GameController gameController;

        private void OnMouseDown()
        {
            gameController.StartGame();
        }
    }
}
