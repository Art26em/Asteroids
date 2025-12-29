using Core.StateControllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] float screenFadeOutStep;
        [SerializeField] Button startButton;
        [SerializeField] Button quitButton;
        
        private GameStartController _gameStartController;
        private CanvasGroup _canvasGroup;

        [Inject]
        private void Construct(GameStartController gameStartController)
        {
            _gameStartController = gameStartController;
        }
        
        private void Awake()
        {
            Time.timeScale = 0; 
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1;
        }
        
        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStart);
            quitButton.onClick.AddListener(OnQuit);
        }

        private void OnStart()
        {
            Time.timeScale = 1;
            _ = FadeOutScreen();
            _gameStartController.StartGame();
        }

        private async UniTask FadeOutScreen()
        {
            while (_canvasGroup.alpha >= 0)
            {
              _canvasGroup.alpha -= screenFadeOutStep;
              if (_canvasGroup.alpha <= 0)
              {
                  gameObject.SetActive(false);
              }
              await UniTask.Yield(PlayerLoopTiming.Update);  
            }
        }
        
        private void OnQuit()
        {
            Application.Quit();
        }
        
        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners(); 
            quitButton.onClick.RemoveAllListeners();
        }
        
    }
}
