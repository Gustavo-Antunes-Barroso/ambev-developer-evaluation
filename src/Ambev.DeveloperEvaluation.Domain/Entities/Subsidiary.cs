using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Subsidiary : BaseEntity
    {
        //TODO: Adicionar Country
        public Guid CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; }
        public string Complement { get; set; } = string.Empty;
    }
}
