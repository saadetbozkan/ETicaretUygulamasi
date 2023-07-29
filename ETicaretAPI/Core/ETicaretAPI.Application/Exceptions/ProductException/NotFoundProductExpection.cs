using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions.ProductException
{
    public class NotFoundProductExpection: Exception
    {
        public NotFoundProductExpection(): base("Product not found")
        {

        }
        public NotFoundProductExpection(string message) : base(message)
        {

        }

    }
}
