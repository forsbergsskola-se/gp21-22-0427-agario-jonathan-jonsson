using TMPro;
using UnityEngine;

namespace Assignment
{
    public class UIUpdate : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField]
        private RequestServerTime rst;
        private void OnEnable()
        {
            rst.OnRequestDateAndTime += UpdateUI;
        }

        private void Start()
        {
            text.SetText("");
        }

        private void UpdateUI(string textToUpdate)
        {
            text.SetText(textToUpdate);
        }
    }
}
