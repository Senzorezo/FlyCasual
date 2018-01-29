﻿using UnityEngine;
using Ship;
using Tokens;
using Movement;

namespace RulesList
{
    public class IonizationRule
    {

        public IonizationRule()
        {
           GenericShip.OnTokenIsAssignedGlobal += CheckIonization;
        }

        private void CheckIonization(GenericShip ship, System.Type tokenType)
        {
            if (tokenType == typeof(IonToken))
            {
                if ((ship.GetToken(typeof(IonToken)).Count == 1) && ship.ShipBaseSize == BaseSize.Small )
                {
                    Messages.ShowError("Ship is ionized!");
                    DoIonized(ship);
                }
                if ((ship.GetToken(typeof(IonToken)).Count == 2) && ship.ShipBaseSize == BaseSize.Large)
                {
                    Messages.ShowError("Ship is ionized!");
                    DoIonized(ship);
                }
            }
        }

        private void DoIonized(GenericShip ship)
        {
            ship.OnManeuverIsReadyToBeRevealed += AssignWhiteForwardOneManeuver;
            ship.OnMovementExecuted += RegisterRemoveIonization;
            ship.ToggleIonized(true);
        }

        private void AssignWhiteForwardOneManeuver(GenericShip ship)
        {
            GenericMovement ionizedMovement = new StraightMovement(1, ManeuverDirection.Forward, ManeuverBearing.Straight, ManeuverColor.White) { IsRealMovement = false };
            ship.SetAssignedManeuver(ionizedMovement);

            ship.OnManeuverIsReadyToBeRevealed -= AssignWhiteForwardOneManeuver;
        }

        private void RegisterRemoveIonization(GenericShip ship)
        {
            ship.OnMovementExecuted -= RegisterRemoveIonization;

            Triggers.RegisterTrigger(new Trigger
            {
                Name = "Remove ionization",
                TriggerType = TriggerTypes.OnShipMovementExecuted,
                TriggerOwner = ship.Owner.PlayerNo,
                EventHandler = RemoveIonization,
                Sender = ship
            });
        }

        private void RemoveIonization(object sender, System.EventArgs e)
        {
            Messages.ShowInfo("Ship isn't ionized anymore");

            GenericShip ship = sender as GenericShip;
            ship.ToggleIonized(false);
            ship.RemoveToken(
                typeof(IonToken),
                Triggers.FinishTrigger
            );
        }

    }
}
