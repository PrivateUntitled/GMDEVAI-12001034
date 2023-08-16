using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speed;
    Vector2 velocity;

    [Header("Energy")]
    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRecoveryRate;
    [SerializeField] private float energyLossRate;
    private float energy;
    private bool isRunning;
    private bool isTired;

    [SerializeField] private Image energyBar;

    public float Speed { get { return speed; } }
    public bool IsRunning { get { return isRunning; } }

    private void Start()
    {
        energy = maxEnergy;
    }

    void Update()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);

        energyBar.fillAmount = energy / maxEnergy;
    }

    bool recoveringEnergy;
    private void LateUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !isTired && !isRunning)
        {
            StopCoroutine("IncreaseEnergy");
            StartCoroutine("DecreaseEnergy");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isTired && !recoveringEnergy)
        {
            StopCoroutine("DecreaseEnergy");
            StartCoroutine("IncreaseEnergy");
        }
    }

    IEnumerator DecreaseEnergy()
    {
        recoveringEnergy = false;
        speed *= 2.5f;
        isRunning = true;

        while (energy > 0)
        {
            energy = Mathf.Clamp(energy - energyLossRate, 0, maxEnergy);
            yield return new WaitForSeconds(0.1f);
        }

        isTired = true;

        StopCoroutine("DecreaseEnergy");
        StartCoroutine("IncreaseEnergy");
    }

    IEnumerator IncreaseEnergy()
    {
        recoveringEnergy = true;
        isRunning = false;

        speed /= 2.5f;
        yield return new WaitForSeconds(3f);

        isTired = false;

        while (energy <= maxEnergy)
        {
            energy = Mathf.Clamp(energy + energyLossRate, 0, maxEnergy);
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine("IncreaseEnergy");
    }
}
