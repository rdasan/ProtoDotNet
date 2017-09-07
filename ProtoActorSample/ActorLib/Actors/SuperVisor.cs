using System;
using System.Linq;
using System.Threading.Tasks;
using ActorLib.Messages;
using Proto;
using Proto.Mailbox;

namespace ActorLib.Actors
{
	public class SuperVisor : IActor
	{
		//private Props _contactProps;
		private Props _ccProps;
		private Props _bankAccProps;

		//private PID _conactPid;

		public SuperVisor()
		{
			_bankAccProps = Actor.FromProducer(() => new BankAccActor());
			_ccProps = Actor.FromProducer(() => new CCActor());
		}


		public Task ReceiveAsync(IContext context)
		{
			var message = context.Message;
			PID contactChild;
			if (message is Account account)
			{
				if (context.Children == null || context.Children.Count == 0)
				{
					contactChild = context.Spawn(Actor.FromProducer(() => new ContactActor()));
				}
				else
				{
					contactChild = context.Children.First();
				}

				contactChild.Request(account, context.Self);
			}

			return Actor.Done;
		}
	}
}