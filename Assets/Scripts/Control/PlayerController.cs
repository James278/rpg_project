﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    using RPG.Movement;
    using RPG.Combat;
    using System;

    public class PlayerController : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) {return;}
            if (InteractWithMovement()) { return; }
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); // GetMouseRay (ray) = origin and direction of ray
                                                                   // RayCastHit (hit) = where ray hit
            if (hasHit) // if ray has hit a point
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Movement>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
