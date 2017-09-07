using System;
using System.Collections.Generic;
using System.Text;

namespace ActorLib.Messages
{
    public class Account
    {
	    public Contact Contact { get; set; }
	    public CreditCard CreditCard { get; set; }
	    public BankAccount BankAccount { get; set; }
    }
}
