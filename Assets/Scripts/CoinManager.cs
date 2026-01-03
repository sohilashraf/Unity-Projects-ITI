using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    public GameObject door;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString();
        if (coinCount >=11)
        { 
            Destroy(door);

        }
    }
}
