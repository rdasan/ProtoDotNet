using System;
using System.Threading.Tasks;
using ActorLib.Messages;
using Proto;
using Proto.Mailbox;

namespace ActorLib.Actors
{
	public class SuperVisor : IActor
	{
		private Props _contactProps;
		private Props _ccProps;
		private Props _bankAccProps;

		private PID _conactPid;

		public SuperVisor()
		{
			_contactProps = new Props()
				.WithProducer(() => new ContactActor())
				.WithChildSupervisorStrategy(new OneForOneStrategy((who, exeption) => SupervisorDirective.Stop, 3,
					TimeSpan.FromSeconds(2)))
				.WithMailbox(() => UnboundedMailbox.Create())
				.WithSpawner(Props.DefaultSpawner);
			_bankAccProps = Actor.FromProducer(() => new BankAccActor());
			_ccProps = Actor.FromProducer(() => new CCActor());

			_conactPid = Actor.Spawn(_contactProps);
		}


		public Task ReceiveAsync(IContext context)
		{
			var message = context.Message;

			if (message is Account account)
			{
				_conactPid.Request(account, context.Self);
			}

			return Actor.Done;
		}
	}
}