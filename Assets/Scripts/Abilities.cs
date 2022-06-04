using System.Collections;
using UnityEngine;

namespace characterMovement
{
    public class Abilities : Character
    {
        Character character;
        protected override void Initialisation()
        {
            base.Initialisation();
            character = GetComponent<Character>();
        }
    }
}