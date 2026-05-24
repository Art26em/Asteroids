using MVVM;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class MovementStatsView : MonoBehaviour
    {
        [Data("Speed")]
        [SerializeField] public TMP_Text speedText;
        [Data("Rotation")]
        [SerializeField] public TMP_Text rotationText;
        [Data("PosX")]
        [SerializeField] public TMP_Text posXText;  
        [Data("PosY")]
        [SerializeField] public TMP_Text posYText;  
    }
}