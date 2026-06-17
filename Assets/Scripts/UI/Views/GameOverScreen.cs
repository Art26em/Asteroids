using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        
        private void OnEnable()
        {
            exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveListener(ExitGame);   
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
        
    }
}