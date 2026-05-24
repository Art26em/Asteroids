using System;
using MVVM;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UI.Binders
{
    public class ImageBinder : IBinder, IObserver<Sprite>
    {
        private readonly Image _view;
        private readonly IReadOnlyReactiveProperty<Sprite> _property;
        private IDisposable _handle;
        
        public ImageBinder(Image view, IReadOnlyReactiveProperty<Sprite> property)
        {
            _view = view;
            _property = property;
        }
        
        public void Bind()
        {
            OnNext(_property.Value);
            _handle = _property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(Sprite value)
        {
            _view.sprite = value;
        }
        
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
        
    }
}