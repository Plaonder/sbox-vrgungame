
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VRGunGame
{

	/// <summary>
	/// game class!!!
	/// </summary>
	public partial class Game : Sandbox.Game
	{
		public Game()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );
				
				// Hud!
				new HudEntity();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// Client!!!! OMGGGGGG
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new Player();
			client.Pawn = player;

			player.Respawn();
		}

		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			base.PostCameraSetup( ref camSetup );

			camSetup.ZNear = 1f;
		}
	}

}
