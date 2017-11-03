﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tokens;

namespace Ship
{
    namespace AlphaClassStarWing
    {
        public class MajorVynder : AlphaClassStarWing
        {
            public MajorVynder() : base()
            {
                PilotName = "Major Vynder";
                ImageUrl = "https://raw.githubusercontent.com/guidokessels/xwing-data/master/images/pilots/Galactic%20Empire/Alpha-class%20Star%20Wing/major-vynder.png";
                PilotSkill = 7;
                Cost = 26;

                IsUnique = true;

                PrintedUpgradeIcons.Add(Upgrade.UpgradeType.Elite);

                PilotAbilities.Add(new PilotAbilitiesNamespace.MajorVynderAbility());
            }
        }
    }
}

namespace PilotAbilitiesNamespace
{
    public class MajorVynderAbility : GenericPilotAbility
    {
        public override void Initialize(Ship.GenericShip host)
        {
            base.Initialize(host);

            Host.AfterGotNumberOfDefenceDice += IncreaseDefenceDiceNumber;
        }

        private void IncreaseDefenceDiceNumber(ref int diceNumber)
        {
            if (Host.HasToken(typeof(WeaponsDisabledToken))) diceNumber++;
        }
    }
}