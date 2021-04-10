using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverAI
{
    class MainLogic : UnityEngine.MonoBehaviour
    {
        public void Start()
        {
            Utility.InitiateVariables();
        }

        public void Update()
        {
            //Utility.FixedUpdateFunc();

            if (Input.GetKeyDown(UnityEngine.KeyCode.Insert))
            {
                // do something lol.
            }

            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
            {
                Globals.ResetControllers();
                return; // Go no further uwu
            }

            Utility.ResetCorridors();
            Utility.UpdateCorridors();
            //Utility.LookAheadByFour();
            Features.ProjectileMovement();
        }

        public void FixedUpdate() // Call it in fixed update, cuz physics is ran in here
        {
           
        }

        public void OnGUI()
        {
            EverAIGUI.InitializeGUIVars();
            EverAIGUI.DrawVisuals();

            /*
             * Debug info!
             */

            // Corridor X positions for players
            //-4.27, -2.15, 0, 2.15, 4.27
            //  0      1    2    3    4
            // Player is always -5.648438 Z, so we should move if their X is ever less than -5

            // Corridor X positions FOR PROJECTILES
            //-4.93, -2.36, 0, 2.36, 4.93
            //  0      1    2    3    4
            // Player is always -5.648438 Z, so we should move if their X is ever less than -5

            //UIHelper.DrawOutline(new UnityEngine.Rect(180, 100, 400, 90), recenthit, EverAIGUI.boldtext);

            //if (Event.current.type != EventType.Repaint)
            // return;
        }
    }
}
