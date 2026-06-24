using System;
using System.Threading;
using Core.States;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private float _screenFadeOutStep;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;
        
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
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1;
        }


        private void Start()
        {
            _signalBus.Fire(new GameStateChangedSignal(GameState.Paused));
        }
        
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStart);
            _quitButton.onClick.AddListener(OnQuit);
        }

        private void OnStart()
        {
            _signalBus.Fire(new GameStateChangedSignal(GameState.Starting));
            _ = FadeOutScreen();
        }

        private async UniTask FadeOutScreen()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (_canvasGroup.alpha > 0)
                {
                    _canvasGroup.alpha -= _screenFadeOutStep;
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
            _startButton.onClick.RemoveListener(OnStart); 
            _quitButton.onClick.RemoveListener(OnQuit);
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
