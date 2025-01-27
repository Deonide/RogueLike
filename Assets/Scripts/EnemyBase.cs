using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Collections;

public class EnemyBase : Character
{
    [SerializeField]
    private WaveManager m_waveManager;
    [SerializeField]
    protected float m_healthMod;
    [SerializeField]
    private TMP_Text m_enemyHealthText;
    
    protected Player m_player;

    protected float m_diffModifier;
    public int m_spikeValue;
    private int m_abilityToUse;

    protected override void Awake()
    {
        base.Awake();
        m_player = FindFirstObjectByType<Player>();

        if (GameManager.Instance.m_currentWave <= 25)
        {
            m_diffModifier = 0.25f;
        }
        else if (GameManager.Instance.m_currentWave >= 26 && GameManager.Instance.m_currentWave <= 50)
        {
            m_diffModifier = 0.5f;
        }
        else if (GameManager.Instance.m_currentWave >= 51 && GameManager.Instance.m_currentWave <= 75)
        {
            m_diffModifier = 1f;
        }
        else if (GameManager.Instance.m_currentWave >= 76 && GameManager.Instance.m_currentWave <= 100)
        {
            m_diffModifier = 2f;
        }
        else if (GameManager.Instance.m_currentWave > 100)
        {
            m_diffModifier = 4f;
        }

        m_currentHealth = 10 + Mathf.RoundToInt(m_healthMod + (GameManager.Instance.m_currentWave / 2) * m_diffModifier );
        AbilityToUse();
        UpdateHUD();
    }

    protected override void Update()
    {
        base.Update();
        if (m_waveManager == null)
        {
            m_waveManager = FindFirstObjectByType<WaveManager>();
        }

        if (m_currentHealth <= 0)
        {
            m_animator.SetTrigger("Deaded");
        }
    }

    private void AnimationOver()
    {
        m_waveManager.RemoveFromList(this.gameObject);
        Destroy(gameObject);
    }

    public void IncreaseArmor(int armorIncrease)
    {
        m_armor += armorIncrease;
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        UpdateHUD();
    }

    #region AbilityToUse
    public virtual void AbilityToUse()
    {
        m_abilityToUse = Random.Range(0, 3);
    }

    public virtual void UseAbility()
    {
        switch(m_abilityToUse)
        {
            case 0:
                m_animator.SetTrigger("Attack");
                UseAbilityOne();
                break;
            case 1:
                m_animator.SetTrigger("Attack");
                UseAbilityTwo();
                break;
            case 2:
                m_animator.SetTrigger("Attack");
                UseAbilityThree();
                break;
        }
    }

    protected virtual void UseAbilityOne()
    {

    }

    protected virtual void UseAbilityTwo()
    {

    }

    protected virtual void UseAbilityThree()
    {

    }
    #endregion
    #region Attacks
    protected virtual void LightAttack()
    {
        Debug.Log("Light Attack");
        float damage = (12 + m_strength) * m_diffModifier;
        CheckVulnerable(damage);
        CheckWeakend(damage);
        m_player.Damage(Mathf.RoundToInt(damage));
        ArmorCheck(Mathf.RoundToInt(damage));
    }

    protected virtual void HeavyAttack()
    {
        Debug.Log("Heavy Attack");
        float damage = (16 + m_strength) * m_diffModifier;
        CheckVulnerable(damage);
        CheckWeakend(damage);
        m_player.Damage(Mathf.RoundToInt(damage));
        ArmorCheck(Mathf.RoundToInt(damage));
    }

    protected virtual void PoisononousGas()
    {
        Debug.Log("Poisonous Gas");
        float damage = (8 + m_strength) * m_diffModifier;
        CheckVulnerable(damage);
        CheckWeakend(damage);
        m_player.Damage(Mathf.RoundToInt(damage));
        ArmorCheck(Mathf.RoundToInt(damage));
        m_player.m_poisonValue += Mathf.RoundToInt(5 * m_diffModifier);
    }

    protected virtual void WeakeningStrike()
    {
        Debug.Log("Weakening Strike");
        float damage = (8 + m_strength) * m_diffModifier;
        CheckVulnerable(damage);
        CheckWeakend(damage);
        m_player.Damage(Mathf.RoundToInt(damage));
        ArmorCheck(Mathf.RoundToInt(damage));
        m_player.m_weakValue += 1;
    }

    protected virtual void VulnerableStrike()
    {
        Debug.Log("Vulnerable Strike");
        float damage = (8 + m_strength) * m_diffModifier;
        CheckVulnerable(damage);
        CheckWeakend(damage);
        m_player.Damage(Mathf.RoundToInt(damage));

        m_player.m_vulnerableValue += 1;
    }

    protected virtual void SpikeTrap()
    {
        Debug.Log("Spike Trap");
        m_spikeValue += Mathf.RoundToInt(7 * m_diffModifier);
    }

    protected virtual void StrengthIncrease()
    {
        Debug.Log("Strength Increase");
        int buffRandomEnemy = Random.Range(0, m_waveManager.m_spawnedEnemies.Count);

        EnemyBase targetEnemy = m_waveManager.m_spawnedEnemies.ElementAt(buffRandomEnemy).GetComponent<EnemyBase>();
        targetEnemy.m_strength += 1;
    }
    #region Repeatables
    private float CheckWeakend(float damage)
    {
        if (m_isWeak)
        {
            damage = Mathf.RoundToInt(damage * 0.75f);
        }
        return damage;
    }

    private float CheckVulnerable(float damage)
    {
        if (m_player.m_isVulnerable)
        {
            damage = damage * 1.25f;
        }
        return damage;
    }
    public void ArmorCheck(int damage)
    {
        if(m_player.m_armor > 0)
        {
            m_player.DecreaseArmor(damage);
            GameManager.Instance.m_uIManager.UpdateText();
        }
    }
    #endregion
    #endregion

    private void UpdateHUD()
    {
        m_enemyHealthText.text = "HP: " + m_currentHealth.ToString();
    }
}
