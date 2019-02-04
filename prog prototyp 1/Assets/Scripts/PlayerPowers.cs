using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public float freezeTimer = 2f;

    private bool abilityQ;

    public Hellfire hellfire;

	void Update ()
    {
        PlayerInput();

        if (abilityQ)
        {   
            StartCoroutine(FreezeHellfire());
        }
	}

    void PlayerInput()
    {
        bool input_Q = Input.GetKeyDown(KeyCode.Q);

        abilityQ = input_Q; 
    }

    IEnumerator FreezeHellfire()
    {
        hellfire.hellfire = false;
        yield return new WaitForSeconds(freezeTimer);
        hellfire.hellfire = true;
    }
}
