using UnityEngine;

public class Coin : MonoBehaviour, IPickuble
{
    [SerializeField] private int _amount = 1;

    public void PickUp()
    {
        var wallet = FindObjectOfType<Score>();

        if (wallet)
        {
            wallet.ScorePoint += _amount;
        }

        gameObject.SetActive(false);
    }
}
