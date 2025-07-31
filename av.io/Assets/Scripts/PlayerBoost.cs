using System.Collections;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float maxBoostTime = 100f;
    private float boostCounter;
    private bool isBoosting = false;

    void Start()
    {
        RechargeBoost();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isBoosting && boostCounter > 0)
            StartCoroutine(UseBoost());
    }

    private IEnumerator UseBoost()
    {
        Debug.Log("Boost");
        isBoosting = true;

        playerMovement.currentAcceleration = playerMovement.maxAcceleration * 1.5f;
        SmokeSpawner.instance.TurnOnSmoke();

        while(boostCounter > 0)
        {
            boostCounter -= Time.deltaTime;
        }
        playerMovement.ToggleEngine("off");
        SmokeSpawner.instance.TurnOffSmoke();

        Invoke(nameof(RechargeBoost), 1f);
    }

    private void RechargeBoost()
    {
        boostCounter = maxBoostTime;
    }
}
