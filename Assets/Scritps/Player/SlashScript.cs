using UnityEngine;

public class SlashScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundsManager.Instance.hitSound.Play();
        IDamagable damagable = collision.GetComponent<IDamagable>();
        IDeflectable deflectable = collision.GetComponent<IDeflectable>();

        if (deflectable != null)
        {
            EffectsManager.Instance.PlayHitEffect(collision.transform.position, playerAttack.attackDirection);
            deflectable.Deflect(playerAttack.attackDirection);
        }

        else if (damagable != null)
        {
            if (playerController.transform.position.x > collision.transform.position.x)
            {
                damagable.TakeDamage(Vector2.left);
                if (IsHorizontalAttack())
                {
                    playerController.StartKnockUp(Vector2.right);
                }
            }
            else
            {
                damagable.TakeDamage(Vector2.right);
                if (IsHorizontalAttack())
                {
                    playerController.StartKnockUp(Vector2.left);
                }
            }
        }

        if (playerAttack.attackDirection == Vector2.down)
        {
            playerController.PerformJumpAttack();
            EffectsManager.Instance.PlayHitEffect(collision.transform.position, playerAttack.attackDirection);
        }
    }

    private bool IsHorizontalAttack()
    {
        return playerAttack.attackDirection != Vector2.down && playerAttack.attackDirection != Vector2.up;
    }

    
}
