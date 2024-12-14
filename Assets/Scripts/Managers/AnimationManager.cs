using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;
    [SerializeField] public GameObject fireballPrefab;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public GameObject magicPrefab;


    private GameObject shotProjectile;
    private BaseUnit shooter;
    private BaseUnit target;
    private Vector3 targetPos;


    void Awake()
    {
        Instance = this;

    }

    void Update()
    {
        // move stot arrow toward target
        if (shotProjectile != null)
        {
            float zIndex = shooter.OccupiedTile.transform.position.z;
            var step = 6 * Time.deltaTime;

            shotProjectile.transform.position = Vector2.MoveTowards(shotProjectile.transform.position, targetPos, step);
            shotProjectile.transform.position = new Vector3(shotProjectile.transform.position.x, shotProjectile.transform.position.y, zIndex);
            // angle shot projectile
            var dir = targetPos - shotProjectile.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            shotProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (shotProjectile != null && shotProjectile.transform.position == targetPos)
        {
            Destroy(shotProjectile.gameObject);
            shotProjectile = null;
            shooter = null;
            target = null;
        }
    }

    // fireball skill explosion animation
    public void AnimateFireball(OverlayTile tile)
    {
        var fireball = Instantiate(fireballPrefab);
        fireball.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.001f, tile.transform.position.z);
        StartCoroutine(DeleteAnimation(fireball, 1.2f));

    }

    // animation for shooting arrow
    public void ShootArrow(BaseUnit Shooter, BaseUnit Target)
    {
        var arrow = Instantiate(arrowPrefab);
        arrow.transform.position = new Vector3(Shooter.transform.position.x, Shooter.transform.position.y, Shooter.transform.position.z);

        shotProjectile = arrow;
        shooter = Shooter;
        target = Target;
        targetPos = target.transform.position;

        StartCoroutine(DeleteAnimation(arrow, 1.2f));

    }

    // animation for shooting magic
    public void ShootMagic(BaseUnit Shooter, BaseUnit Target)
    {
        var magic = Instantiate(magicPrefab);
        magic.transform.position = new Vector3(Shooter.transform.position.x, Shooter.transform.position.y, Shooter.transform.position.z);

        shotProjectile = magic;
        shooter = Shooter;
        target = Target;
        StartCoroutine(DeleteAnimation(magic, 1.2f));

    }

    private IEnumerator DeleteAnimation(GameObject anim, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(anim.gameObject);
    }
}
