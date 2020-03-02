﻿using System;
using SuperSafeBank.Core.Models.Events;

namespace SuperSafeBank.Core.Models
{
    public class Customer : BaseAggregateRoot<Customer, Guid>
    {
        private Customer() { }
        
        public Customer(Guid id, string firstname, string lastname) : base(id)
        {
            Firstname = firstname;
            Lastname = lastname;

            this.AddEvent(new CustomerCreated(this));
        }

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }

        protected override void Apply(IDomainEvent<Guid> @event)
        {
            switch (@event)
            {
                case Models.Events.CustomerCreated c:
                    this.Firstname = c.Firstname;
                    this.Lastname = c.Lastname;
                    break;
            }
        }

        public static Customer Create(string firstName, string lastName)
        {
            return new Customer(Guid.NewGuid(), firstName, lastName);
        }
    }
}