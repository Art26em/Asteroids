using MVVM;
using UnityEngine;

namespace UI.Views
{
    public class HealthBarView : MonoBehaviour
    {   
        [Data("HealthBar")]
        public CollectionView<HealthItemView> collection;
    }
}