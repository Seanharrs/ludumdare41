using UnityEngine;
using UnityEngine.UI;

public class CurrencyText : MonoBehaviour
{
    public Text text;

    private void Awake() { text = GetComponent<Text>(); }

    public void UpdateCurrency(int val)
    {
        text.text = "Money: " + (val < 10 ? "0" : "") + val;
    }
}
