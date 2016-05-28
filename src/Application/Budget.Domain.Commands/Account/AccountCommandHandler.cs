﻿// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.Budget.Domain.Commands.Account
{
    using BudgetFirst.Budget.Repositories;
    using BudgetFirst.SharedInterfaces.Commands;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Handles commands related to Accounts
    /// </summary>
    public class AccountCommandHandler : ICommandHandler<CreateAccountCommand>, ICommandHandler<ChangeAccountNameCommand>
    {
        /// <summary>
        /// The Account repository
        /// </summary>
        private readonly AccountRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The account repository</param>
        public AccountCommandHandler(AccountRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handles the ChangeAccountName command
        /// </summary>
        /// <param name="command">The ChangeAccountNameCommand</param>
        /// <param name="eventTransaction">The event transaction</param>
        public void Handle(ChangeAccountNameCommand command, IEventTransaction eventTransaction)
        {
            Aggregates.Account account = this.repository.Find(command.Id);
            account.ChangeName(command.Name);
            eventTransaction.Add(account.Events);
        }

        /// <summary>
        /// Handles the CreateAccountName command
        /// </summary>
        /// <param name="command">The CreateAccountNameCommand</param>
        /// <param name="eventTransaction">The event transaction</param>
        public void Handle(CreateAccountCommand command, IEventTransaction eventTransaction)
        {
            Aggregates.Account account = new Aggregates.Account(command.Id, command.Name);
            eventTransaction.Add(account.Events);
        }
    }
}
