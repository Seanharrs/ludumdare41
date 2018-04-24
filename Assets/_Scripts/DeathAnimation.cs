using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    private Animator m_Anim;
	
    private int m_DieHash;

    private void Awake()
    {
		
		//AudioSource audio = GetComponent<AudioSource>();
        
		//audio.Play();
        //audio.Play(Enemies_Death);
		
		
		Rigidbody rb = GetComponent<Rigidbody> ();
        if(rb)
            rb.velocity = Vector3.zero;

        Collider col = GetComponent<Collider>();
        if(col)
            col.enabled = false;

        m_Anim = GetComponent<Animator>();
        m_Anim.SetTrigger("Die");

        m_DieHash = Animator.StringToHash("Base Layer.Death");

		
        //audio.Play (Turret_firing_sound);
		//AudioClip[] possibleDeathSounds = Enemies Death;
        //AudioClip deathSound = possibleDeathSounds[Random.Range(0, possibleDeathSounds.Length)];

        //AudioSource.PlayClipAtPoint(deathSound, transform.position, PlayPrefs.GameSoundVolume);
    }

    private void Update()
    {
        AnimatorStateInfo state = m_Anim.GetCurrentAnimatorStateInfo(0);
        if(state.fullPathHash == m_DieHash && state.normalizedTime > 1.5f)
            gameObject.SetActive(false);
    }
}
