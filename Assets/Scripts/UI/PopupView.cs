using Kor.Tools;
using TMPro;
using UnityEngine;

namespace Kor.UI
{
    public class PopupView : MonoBehaviour , IPopupView
    {
        [RequireReference] [SerializeField] private TMP_Text titleText;

        public void Init(string title)
        {
            titleText.text = title;
        }

    }

 
}