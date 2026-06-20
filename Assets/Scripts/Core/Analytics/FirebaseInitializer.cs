using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace Core.Analytics
{
    public class FirebaseInitializer : MonoBehaviour
    {
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                    throw new Exception("Could not resolve all Firebase dependencies: ", task.Exception);
                
                var status = task.Result;
                if (status != DependencyStatus.Available)
                    throw new Exception($"Could not resolve all Firebase dependencies: {status}");
                
                print("Firebase initialized successfully");
                
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
    }
}
