using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IContactsService
    {
        PersonResponse AddContact(PersonAddRequest? personDto);

        List<PersonResponse> GetContacts();

        PersonResponse? GetContact(Guid? id);
    }
}
