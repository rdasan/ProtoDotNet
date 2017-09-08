using System;
using System.Threading.Tasks;
using Proto;

namespace ActorLib.Actors
{
	public class CCActor : IActor
	{
		private readonly Behavior _behavior;

		public CCActor()
		{
			_behavior = new Behavior();
		}

		public Task ReceiveAsync(IContext context)
		{
			switch (context.Message)
			{
				case "Visa":
					_behavior.BecomeStacked(PreProcessVisa);
					break;
				case "MasterCard":
					_behavior.BecomeStacked(PreProcessMasterCard);
					break;
			}

			//Default processing for any card
			Console.WriteLine("Resuming Default processing for any card after all presprocessing is done");

			return Actor.Done;
		}

		private Task PreProcessMasterCard(IContext context)
		{
			//Do specific processing for Master Card
			Console.WriteLine("Preprocessing MasterCard");
			_behavior.UnbecomeStacked();
			return Actor.Done;
		}

		private Task PreProcessVisa(IContext context)
		{
			//Do specifics for Visa
			switch (context.Message)
			{
				case "Visa":
					Console.WriteLine("Preprocessing Visa");
					_behavior.UnbecomeStacked();
					break;
					
			}

			return Actor.Done;

		}


	}
}