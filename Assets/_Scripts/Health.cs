using UnityEngine;

public class Health : MonoBehaviour
{
    public int initialAndMaxHealth = 100;
    public int healthRemaining;

    private float m_TimeSinceDamageTaken;
    private bool p_WasAttackedRecently { get { return m_TimeSinceDamageTaken < 0.5f; } }

    private bool m_IsInvulnerable;
    public bool isInvulnerable { get { return m_IsInvulnerable || p_WasAttackedRecently; } }

    private AudioSource m_Audio;
    private Animator m_Anim;

    [SerializeField]
    private GUIHealthBar m_HealthBar;

    private void Awake()
    {
        healthRemaining = initialAndMaxHealth;

        m_Audio = GetComponent<AudioSource>();
        m_Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        m_TimeSinceDamageTaken += Time.deltaTime;
    }

    private void SetHealth(int newHp)
    {
        if(healthRemaining > newHp)
            m_TimeSinceDamageTaken = 0f;

        healthRemaining = newHp;
        if(m_HealthBar)
            m_HealthBar.UpdateGUI(healthRemaining, initialAndMaxHealth);

        if(healthRemaining == 0)
        {
            gameObject.AddComponent<DeathAnimation>();
            enabled = false;
        }
    }

    /// <summary>Deals damage to the entity, reducing its health.</summary>
    /// <param name="amount">The amount of damage to deal to the entity.</param>
    public void TakeDamage(int amount)
    {
        if(p_WasAttackedRecently)
            return;

		if(!m_Audio.isPlaying)
        {
			//AudioClip[] damageSounds = /*get hurt sound*/;
			//m_Audio.clip = damageSounds[Random.Range(0, damageSounds.Length)];
			//m_Audio.Play();
		}

        if(isInvulnerable)
            return;

        SetHealth(Mathf.Clamp(healthRemaining - amount, 0, initialAndMaxHealth));
        if(healthRemaining > 0)
            m_Anim.SetTrigger("Stagger");
    }

    /// <summary>Forcibly kills the entity, regardless of whether it is invulnerable or has been attacked recently.</summary>
    public void ForceKill()
    {
        if(!m_Audio.isPlaying)
        {
            //AudioClip[] damageSounds = /*get hurt sound*/;
            //m_Audio.clip = damageSounds[Random.Range(0, damageSounds.Length)];
            //m_Audio.Play();
        }

        SetHealth(0);
    }

    /// <summary>Heals the entity, increasing its health.</summary>
    /// <param name="amount">The amount of health to recover.</param>
    public void RecoverHealth(int amount)
    {
        SetHealth(Mathf.Clamp(healthRemaining + amount, 0, initialAndMaxHealth));
    }

    /// <summary>Resets the entity's health to full.</summary>
    public void ResetHealth()
    {
        healthRemaining = initialAndMaxHealth;
        if(m_HealthBar)
            m_HealthBar.UpdateGUI(healthRemaining, initialAndMaxHealth);
    }
}
