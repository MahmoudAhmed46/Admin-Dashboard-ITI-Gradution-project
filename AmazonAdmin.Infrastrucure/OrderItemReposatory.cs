using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using AmazonAdmin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Infrastrucure
{
    public class OrderItemReposatory : Reposatory<OrderItem, int>, IOrderItemReposatory
    {
        public OrderItemReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
