using System;
using System.Threading.Tasks;
using ActorLib.Messages;
using Proto;

namespace ActorLib.Actors
{
	public class ContactActor : IActor
	{
		private int _messageCounter;


		public ContactActor()
		{
			_messageCounter = 1;
		}

		public Task ReceiveAsync(IContext context)
		{
			switch (context.Message)
			{
				case Started msg:
					Console.WriteLine("Started, initialize actor here");
					break;
				case Stopping r:
					Console.WriteLine("Stopping, actor is about shut down");
					break;
				case Stopped r:
					Console.WriteLine("Stopped, actor is stopped");
					break;
				case Restarting r:
					Console.WriteLine("Restarting, actor is about restart");
					break;
				case Account account:
					try
					{
						Console.WriteLine($"Child Contact Reveived: {account.Contact.FirstName} {account.Contact.LastName}");
						Console.WriteLine($"Message Counter : {_messageCounter++}");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						throw;
					}
					break;
			}

			return Actor.Done;
		}		
	}
}