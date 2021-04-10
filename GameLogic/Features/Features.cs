using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace EverAI
{
    class Features
    {
        static public bool ProjectileMovement()
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            //List<GameObject> list = new List<GameObject>();
            //list = Everhood.ShootProjectile.GetProjectiles();
            /*
            if (Everhood.ShootProjectile.projectiles != null)
            {
                foreach (GameObject proj in Everhood.ShootProjectile.projectiles)
                {
                    if (proj.activeSelf)
                    {

                        if (!Everhood.ShootProjectile.unjumpablesProjectiles.Contains(proj)) // can i jump it?
                        {
                            if (Vector3.Distance(proj.transform.position, ply.transform.position) < 2)
                            {
                                //recenthit = "X: " + proj.transform.position.x + " Y: " + proj.transform.position.y + " Z: " + proj.transform.position.z;
                                VerticalMovement = 1;
                            }
                        }
                        else
                        {
                            if (IsInSameCorridor(proj.transform.position))
                            {
                                //recenthit = "X: " + proj.transform.position.x + " Y: " + proj.transform.position.y + " Z: " + proj.transform.position.z;
                                for (int i = 0; i < OpenCorridor.Length; i++)
                                {
                                    if (OpenCorridor[i])
                                    {
                                        if (ply.currentCorridor > i) // MOVE LEFT
                                        {
                                            if (IsPathOpen(i) || CanMakeProgress(i))
                                            {
                                                HorizontalMovement = -1;
                                                //HoriMoveMentController.Move(-1);
                                            }
                                        }
                                        else if (ply.currentCorridor < i) // MOVE RIGHT
                                        {
                                            if (IsPathOpen(i) || CanMakeProgress(i))
                                            {
                                                HorizontalMovement = 1;
                                                //HoriMoveMentController.Move(1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

            //int LegitimateMode = 1;

            int VerticalMovement = 0;
            int HorizontalMovement = 0;

            if (pLocal.tookDamage)
                Utility.printclosestProjectile();

            // If the corridor is blocked, it checks if it should move left/right.
            if (!Utility.IsCorridorOpen(pLocal.currentCorridor))
            {

                Globals.MoveVertical(-1);

                if (Utility.CanMoveRight())
                    HorizontalMovement = 1;
                else if (Utility.CanMoveLeft())
                    HorizontalMovement = -1;

                if (Utility.CanJumpRight())
                {
                    VerticalMovement = 1;
                    HorizontalMovement = 1;
                }
                else if (Utility.CanJumpLeft())
                {
                    VerticalMovement = 1;
                    HorizontalMovement = -1;
                }

                //if (VerticalMovement == 0 && HorizontalMovement == 0)
                    //VerticalMovement = -1;
            }

            /*
            if (VerticalMovement == -1)
            {
                Everhood.Battle.DeflectBehaviour defBehavior = UnityEngine.Object.FindObjectOfType<Everhood.Battle.DeflectBehaviour>();

                MethodInfo dynMethod = defBehavior.GetType().GetMethod("Deflect", BindingFlags.NonPublic | BindingFlags.Instance);
                if (dynMethod == null)
                    UnityEngine.Debug.LogError("DYNMETHOD IS NULL NOOOOOOOOO");

                dynMethod.Invoke(defBehavior, new object[] { });
            }*/

            // Simultaneously, it'll jump at all times, if it can.
            if (Utility.ShouldJump())
                VerticalMovement = 1;

            // End of projectile for loop.
            if (VerticalMovement != 0)
                Globals.MoveVertical(VerticalMovement);

            if (HorizontalMovement != 0)
                Globals.MoveHorizontal(HorizontalMovement);


            /*
            else
            {
                if (!Utility.IsCorridorOpen(pLocal.currentCorridor))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (Utility.IsCorridorOpen(i))
                        {
                            // Instant move myself here.
                            break;
                        }
                    }
                    Globals.MoveVertical(1);
                }
            }*/

            return VerticalMovement != 0 || HorizontalMovement != 0;
        }
        
    }
}
