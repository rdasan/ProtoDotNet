using System;
using System.Threading;
using System.Threading.Tasks;
using ActorLib.Messages;
using Proto;

namespace ActorLib.Actors
{
	public class HelloActor : IActor
	{

		public Task ReceiveAsync(IContext context)
		{
			var message = context.Message;

			if (message is Hello helloMessage)
			{
				Console.WriteLine($"Message Recived by HelloActor - Welcome to Actors {helloMessage.Who}");
				Thread.Sleep(200);
			}

			return Actor.Done;
		}
	}
}