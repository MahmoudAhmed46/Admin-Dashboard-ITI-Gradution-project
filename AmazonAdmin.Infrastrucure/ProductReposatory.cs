using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using AmazonAdmin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Infrastructure
{
    public class ProductReposatory : Reposatory<Product, int>, IProductReposatory
    {
        public ProductReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
