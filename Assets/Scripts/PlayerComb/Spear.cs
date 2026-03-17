using UnityEngine;

public class Spear : Weapon
{
    public override void Attack()
    {
        PerformAttack();
        // Mo¿na dodaæ animacjê miecza, dŸwiêk, detekcjê kolizji itp.
        Debug.Log("Shaking spear!");
    }
}