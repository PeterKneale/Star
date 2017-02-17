using System;
using ServiceStack;

namespace Services.Account.Models
{
    [Route("/Account/{id}", "DELETE", Summary = "Delete an Account")]
    public class DeleteAccount : IDelete, IReturn<DeleteAccountResponse>
    {
        [ApiMember(Name = "Id", Description = "Identifier", ParameterType = "path", DataType = "guid", IsRequired = true)]
        public Guid Id { get; set; }
    }

    public class DeleteAccountResponse
    {

    }

    public class AccountDeletedEvent
    {
        public int Id { get; set; }
    }
}