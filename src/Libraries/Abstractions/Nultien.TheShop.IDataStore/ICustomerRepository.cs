﻿using Nultien.TheShop.Common.Models;

namespace Nultien.TheShop.IDataStore
{
    public interface ICustomerRepository
    {
        void AssignOrderToCustomer(Order order, string customerId);
    }
}