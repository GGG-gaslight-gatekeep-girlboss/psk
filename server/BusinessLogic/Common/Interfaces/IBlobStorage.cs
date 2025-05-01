using CoffeeShop.BusinessLogic.Common.DTOs;

namespace CoffeeShop.BusinessLogic.Common.Interfaces;

public interface IBlobStorage
{
    Task UploadImage(UploadImageDTO uploadImageDTO);
}