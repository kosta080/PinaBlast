using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kor.Board
{
    public class ColumnView : MonoBehaviour
    {
        [SerializeField] private List<Image> images;
        [SerializeField] private List<Button> buttons;
        public event Action<int> OnButtonClick;

        private void Start()
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                var index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    OnButtonClick?.Invoke(index);
                });
            }
        }

        public void Apply(CellState[] columnData)
        {
            for (int i = 0; i < columnData.Length; i++)
            {
                images[i].color = columnData[i] == CellState.Red ? Color.red :
                    columnData[i] == CellState.Green ? Color.green : Color.white;
            }
            
        }
    }
}