using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentalHealthComponent : MonoBehaviour
{
    private float mentalHealth;
    [SerializeField] private float mentalHealthMax;
    [SerializeField] private Image mentalHealthBar;
    [SerializeField] private float mentalHealthGain;

    private RenderVignette vignette;
    private bool mentalHealthIncreasing;

    private IEnumerator increaseMentalHealth;

    public float MentalHealth { get { return mentalHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        mentalHealth = mentalHealthMax;

        vignette = this.GetComponentInChildren<RenderVignette>();

        increaseMentalHealth = IncreaseMentalHealth(mentalHealthGain);

        SetMentealEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if (noEnemiesNearby())
        {
            if (!mentalHealthIncreasing)
            {
                mentalHealthIncreasing = true;
                StartCoroutine(increaseMentalHealth);
            }
        }
        else
        {
            if (mentalHealthIncreasing)
            {
                mentalHealthIncreasing = false;
                StopCoroutine(increaseMentalHealth);
            }
        }
    }

    private void SetMentealEffects()
    {
        mentalHealthBar.fillAmount = mentalHealth / mentalHealthMax;

        float value = 1.0f - (mentalHealth / mentalHealthMax);

        vignette.vignetteAmount = value;
        vignette.grayScaleAmount = value;
        
    }

    private bool noEnemiesNearby()
    {
        foreach(GameObject _enemy in SpawnManager.instance.Enemies)
        {
            if(_enemy.GetComponent<AttackComponent>().DrainingPlayer)
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator DecreaseMentalHealth(float mentalDrainRate)
    {
        while (mentalHealth > 0)
        {
            Debug.Log(Mathf.Max(mentalHealth - mentalDrainRate, 0));
            mentalHealth = Mathf.Max(mentalHealth - mentalDrainRate, 0);
            
            Debug.Log(mentalHealth);
            SetMentealEffects();
            Debug.Log(mentalHealth);


            if (mentalHealth <= 0)
            {
                PanelManager.instance.PlayerLoses();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator IncreaseMentalHealth(float healthGain)
    {
        yield return new WaitForSeconds(3.0f);

        while (mentalHealth <= mentalHealthMax)
        {
            Debug.Log("Heal");
            mentalHealth = Mathf.Clamp(mentalHealth + healthGain, 0, mentalHealthMax);
            SetMentealEffects();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
