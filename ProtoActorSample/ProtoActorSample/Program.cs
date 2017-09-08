using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ActorLib.Actors;
using ActorLib.Messages;
using Proto;
using Proto.Mailbox;
using Counter = ActorLib.Messages.Counter;
using Process = System.Diagnostics.Process;

namespace ProtoActorSample
{
    class Program
    {
        static void Main(string[] args)
        {
			//Basic way of creating an actor - default
	        Props propsCounterActor = Actor.FromProducer(() => new CounterActor());

			//Creating an actor with specifics
	        Props propsHelloActor = new Props()
		        .WithProducer(() => new HelloActor())
		        .WithDispatcher(new ThreadPoolDispatcher {Throughput = 3})
		        .WithSpawner(Props.DefaultSpawner);
				

			PID counter = Actor.Spawn(propsCounterActor);
			PID helloActor = Actor.SpawnNamed(propsHelloActor, "Hello Actor 1");

			//Console.WriteLine("Start Sending Messages");
			//for (int i = 1; i < 20; i++)
			//{
			//	Console.WriteLine($"Sending Message {i}");
			//	helloActor.Tell(new Hello { Who = $"Reji {i}" }); // Fire n forget
			//													  //Thread.Sleep(100);
			//	counter.Tell(new Counter { Count = i });
			//}

			//Console.WriteLine("All messages sent. Main Thread Processing other stuff");
			//Console.WriteLine("\n\n*****************************************\n\n");

			Props superVisorProps = new Props()
				.WithProducer(() => new SuperVisor())
				.WithChildSupervisorStrategy(
					new OneForOneStrategy((who, exeption) => SupervisorDirective.Stop, 3, TimeSpan.FromSeconds(2)))
				.WithMailbox(() => UnboundedMailbox.Create())
				.WithSpawner(Props.DefaultSpawner);

			PID superVisor = Actor.Spawn(superVisorProps);

	        List<Account> accontsList = new List<Account>
	        {
		        new Account
		        {
			        Contact = new Contact {FirstName = "Reji", LastName = "Dasan1"},
			        CreditCard = new CreditCard {CCNumber = "4111111111111111", ExpDate = "09/2021"}
		        },
		        new Account
		        {
			        Contact = new Contact {FirstName = "Reji", LastName = "Dasan2"},
			        CreditCard = new CreditCard {CCNumber = "4111111111111111", ExpDate = "09/2021"}
		        },
		        new Account
		        {
			        Contact = null,
			        CreditCard = null
		        },
		        new Account
		        {
			        Contact = new Contact {FirstName = "Reji", LastName = "Dasan3"},
			        CreditCard = new CreditCard {CCNumber = "4111111111111111", ExpDate = "09/2021"}
		        },
				new Account
		        {
			        Contact = new Contact {FirstName = "Reji", LastName = "Dasan4"},
			        CreditCard = new CreditCard {CCNumber = "4111111111111111", ExpDate = "09/2021"}
		        }
			};

			foreach (var account in accontsList)
			{
				superVisor.Tell(account);
			}

			PID ccActor = Actor.Spawn(Actor.FromProducer(() => new CCActor()));

			//ccActor.Tell("Visa");

			Console.Read();
        }
    }
}