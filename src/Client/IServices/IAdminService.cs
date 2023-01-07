using RateMyManagementWASM.Shared.Requests;

namespace RateMyManagementWASM.Client.IServices;

public interface IAdminService
{
    public Task PopulateDb(PopulateDbRequest content);
}