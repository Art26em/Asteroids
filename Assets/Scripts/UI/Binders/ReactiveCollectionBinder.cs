using System;
using System.Collections.Generic;
using MVVM;
using UI.Views;
using UniRx;
using UnityEngine;

namespace UI.Binders
{
    public sealed class ReactiveCollectionBinder<TView, TModel> : IBinder where TView : Component
    {
        private readonly CollectionView<TView> _collectionView;
        private readonly IReadOnlyReactiveCollection<TModel> _collectionModel;

        private readonly Dictionary<TModel, (TView, IBinder)> _elements = new();
        private readonly List<IDisposable> _disposables = new();

        public ReactiveCollectionBinder(
            CollectionView<TView> collectionView, 
            IReadOnlyReactiveCollection<TModel> collectionModel)
        {
            _collectionView = collectionView;
            _collectionModel = collectionModel;
        }

        void IBinder.Bind()
        {
            _collectionModel
                .ObserveAdd()
                .Subscribe(v => OnItemAdded(v.Value)).AddTo(_disposables);
            _collectionModel
                .ObserveRemove()
                .Subscribe(v => OnItemRemoved(v.Value)).AddTo(_disposables);
            
            foreach (TModel item in _collectionModel)
            {
                TView view = _collectionView.AddItem();
                IBinder binder = BinderFactory.CreateComposite(view, item);
                binder.Bind();
            
                _elements.Add(item, (view, binder));
            }
        }

        void IBinder.Unbind()
        {
            foreach ((_, IBinder binder) in _elements.Values)
            {
                binder.Unbind();
            }

            _collectionView.Clear();
            _elements.Clear();
            _disposables.ForEach(it => it.Dispose());
        }

        private void OnItemAdded(TModel item)
        {
            if (_elements.ContainsKey(item))
            {
                return;
            }

            TView view = _collectionView.AddItem();
            BinderComposite binder = BinderFactory.CreateComposite(view, item);
            _elements.Add(item, (view, binder));

            binder.Bind();
        }

        private void OnItemRemoved(TModel item)
        {
            if (_elements.Remove(item, out (TView view, IBinder binder) tuple))
            {
                tuple.binder.Unbind();
                _collectionView.RemoveItem(tuple.view);
            }
        }
    }
}