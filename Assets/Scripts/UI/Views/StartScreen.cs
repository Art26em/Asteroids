using System;
using System.Threading;
using Core.States;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private float screenFadeOutStep;
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        
        private SignalBus _signalBus;
        private CanvasGroup _canvasGroup;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
            _signalBus.Fire(new GameStateChangedSignal(GameState.Starting));
            Time.timeScale = 1;
            _ = FadeOutScreen();
        }

        private async UniTask FadeOutScreen()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (_canvasGroup.alpha > 0)
                {
                    _canvasGroup.alpha -= screenFadeOutStep;
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
                gameObject.SetActive(false);
            }
            catch (OperationCanceledException) {}
        }
        
        private void OnQuit()
        {
            Application.Quit();
        }
        
        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners(); 
            quitButton.onClick.RemoveAllListeners();
            SafeCancelAndDispose();
        }

        private void OnDestroy()
        {
            SafeCancelAndDispose();
        }
        
        private void SafeCancelAndDispose()
        {
            if (_cancellationTokenSource == null) return;
            try
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                    _cancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException) {}
            
            try
            {
                _cancellationTokenSource.Dispose();
            }
            catch (ObjectDisposedException) {}
            
            _cancellationTokenSource = null;
        }
        
    }
}
