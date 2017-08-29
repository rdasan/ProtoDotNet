using System;
using System.Threading;
using System.Threading.Tasks;
using Proto;

namespace ActorLib
{
	public class HelloActor : IActor
	{
		public Task ReceiveAsync(IContext context)
		{
			var message = context.Message;

			if (message is Hello helloMessage)
			{
				
				Console.WriteLine($"Hello - {helloMessage.Who}");
				Thread.Sleep(200);
			}

			return Actor.Done;
		}
	}
}