﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Proto;

namespace ActorLib
{
	public class CounterActor : IActor
	{
		public Task ReceiveAsync(IContext context)
		{
			var message = context.Message;

			if (message is Counter counter)
			{
				Console.WriteLine($"Message Recived by CounterActor No: {counter.Count}");
				Thread.Sleep(100);

			}

			return Actor.Done;
		}
	}
}