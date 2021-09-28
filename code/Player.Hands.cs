using Sandbox;
using System;
using System.Linq;

namespace VRGunGame
{
	public partial class Player : Sandbox.Player
	{
		[Net, Predicted]
		public AnimEntity LeftHand { get; set; }
		[Net, Predicted]
		public AnimEntity RightHand { get; set; }

		public void CreateVRHands()
		{
			if ( LeftHand == null )
			{
				LeftHand = new AnimEntity();
				LeftHand.SetModel( "models/hands/alyx_hand_left.vmdl" );
				LeftHand.Scale = 1f;
				LeftHand.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, true );
				RightHand.CollisionGroup = CollisionGroup.Player;
			}
			if ( RightHand == null )
			{
				RightHand = new AnimEntity();
				RightHand.SetModel( "models/hands/alyx_hand_right.vmdl" );
				RightHand.Scale = 1f;
				RightHand.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, true );
				RightHand.CollisionGroup = CollisionGroup.Player;
			}
		}

		public void SimulateLocalHands()
		{
			LeftHand.Transform = Input.VR.LeftHand.Transform;
			LeftHand.Position += Velocity * Time.Delta;

			RightHand.Transform = Input.VR.RightHand.Transform;
			RightHand.Position += Velocity * Time.Delta;
		}
		public void SimulateServerHands()
		{
			LeftHand.Transform = Input.VR.LeftHand.Transform;

			RightHand.Transform = Input.VR.RightHand.Transform;
		}
	}
}
