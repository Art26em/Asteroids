using System;
using MVVM;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UI.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        [SerializeField]
        private Object view;
        
#if UNITY_EDITOR    
        [SerializeField]
        private MonoScript viewModelType;
#endif        
        
        [SerializeField]
        private string viewModelTypeName;
        
        [Inject]
        private DiContainer diContainer;

        private IBinder _binder;

        private void Awake()
        {
            _binder = CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (viewModelType != null)
            {
                viewModelTypeName = viewModelType.GetClass().AssemblyQualifiedName;
            }
        }
#endif
        
        private IBinder CreateBinder()
        {
            var model = diContainer.Resolve(Type.GetType(this.viewModelTypeName));
            return BinderFactory.CreateComposite(view, model);
        }
    }
}