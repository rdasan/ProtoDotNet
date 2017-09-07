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
			if (context.Message is Account account)
			{
				Console.WriteLine($"Child Contact Reveived: {account.Contact.FirstName} {account.Contact.LastName}");
				Console.WriteLine($"Message Counter : {_messageCounter++}");
			}

			return Actor.Done;
		}
	}
}