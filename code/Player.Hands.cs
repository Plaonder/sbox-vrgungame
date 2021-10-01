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


		protected Vector3 PosOffset => LeftHand.Rotation.Backward * 2f + RightHand.Rotation.Down * 4f;
		protected Rotation RotOffset => Rotation.FromPitch( 65 );

		public void CreateVRHands()
		{
			if ( LeftHand == null )
			{
				LeftHand = new AnimEntity();
				LeftHand.SetModel( "models/hands/alyx_hand_left.vmdl" );
				LeftHand.Scale = 1f;
				LeftHand.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				LeftHand.SetInteractsAs( CollisionLayer.Debris );

				Log.Info( "Made a new Left Hand!" );
			}
			if ( RightHand == null )
			{
				RightHand = new AnimEntity();
				RightHand.SetModel( "models/hands/alyx_hand_right.vmdl" );
				RightHand.Scale = 1f;
				RightHand.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				RightHand.SetInteractsAs( CollisionLayer.Debris );

				Log.Info( "Made a new Right Hand!" );
			}
		}

		public void SimulateServerHands()
		{
			LeftHand.Position = Velocity + ((Input.VR.LeftHand.Transform.Position * PosOffset) - LeftHand.Position) / Time.Delta + Input.VR.LeftHand.Velocity;
			LeftHand.Rotation = Input.VR.LeftHand.Transform.Rotation * RotOffset;

			RightHand.Velocity = Velocity + ((Input.VR.RightHand.Transform.Position * PosOffset) - RightHand.Position) / Time.Delta + Input.VR.RightHand.Velocity;
			RightHand.Rotation = Input.VR.RightHand.Transform.Rotation;
		}
	}
}
