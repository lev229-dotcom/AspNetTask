using AspNetTask.Data;
using AspNetTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetTask.Services;

public class ServiceObjectService
{
    ApplicationContext applicationContext;

	public ServiceObjectService(ApplicationContext applicationContext)
	{
		this.applicationContext = applicationContext;
	}

	public async Task<Guid> CreateServiceObject(string name, int amount)
	{
		var serviceObject = new ServiceObject() { Name = name, Amount = amount };

        applicationContext.Services.Add(serviceObject);
		await applicationContext.SaveChangesAsync();
		return serviceObject.Id;
	}

	public async Task<string> UpdateServiceObject(string? name, int? amount, Guid id)
	{
		if (name is null && amount is null)
			return "Название услуги и изначальное кол-во оборудования должно быть заполнено";
		else
		{
			var currentServiceObject = await applicationContext.Services.FirstOrDefaultAsync(x => x.Id == id);
			currentServiceObject.Name = name;
			currentServiceObject.Amount = amount;
			applicationContext.SaveChanges();
			return currentServiceObject.Id.ToString();
        }

    }

	public async Task<(bool, int?)> Booking(Guid Id, int amount)
	{
		var serviceObject = await applicationContext.Services.FirstOrDefaultAsync(i => i.Id == Id);
		var resultAmount = amount < 0 ? serviceObject.Amount -= amount : (amount > 0 ? serviceObject.Amount -= amount : serviceObject.Amount);
        applicationContext.SaveChanges();
        if (resultAmount < 0)
			return (false, null);
		else
			return (true, resultAmount);

    }
}
