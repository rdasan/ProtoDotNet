using System;
using System.Diagnostics;
using ActorLib;
using Proto;
using Proto.Mailbox;
using Process = System.Diagnostics.Process;

namespace ProtoActorSample
{
    class Program
    {
        static void Main(string[] args)
        {
	        Props propsCounterActor = Actor.FromProducer(() => new CounterActor());

	        Props propsHelloActor = new Props()
		        .WithProducer(() => new HelloActor())
		        .WithDispatcher(new ThreadPoolDispatcher {Throughput = 3})
		        .WithChildSupervisorStrategy(new OneForOneStrategy((who, reason) => SupervisorDirective.Restart, 2, TimeSpan.FromSeconds(2)))
		        .WithSpawner(Props.DefaultSpawner);

	        PID pidCounter = Actor.Spawn(propsCounterActor);
	        PID pidHelloActor = Actor.Spawn(propsHelloActor);

			Console.WriteLine("Start Sending Messages");
	        for (int i = 1; i < 20; i++)
	        {
				Console.WriteLine($"Sending Message {i}");
		        pidHelloActor.Tell(new Hello {Who = $"Reji {i}"});
				pidCounter.Tell(new Counter {Count = i});
	        }

			Console.WriteLine("All messages sent");

	        ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;

	        Console.Read();
        }
    }
}