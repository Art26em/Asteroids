using MVVM;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class LaserStateView : MonoBehaviour
    {
        [Data("LaserState")]
        [SerializeField] public TMP_Text laserStateText; 
    }
}