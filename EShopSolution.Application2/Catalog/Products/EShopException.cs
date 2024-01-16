using System.Runtime.Serialization;

namespace EShopSolution.Application2.Catalog.Products
{
    [Serializable]
    internal class EShopException : Exception
    {
        public EShopException()
        {
        }

        public EShopException(string? message) : base(message)
        {
        }

        public EShopException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EShopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}