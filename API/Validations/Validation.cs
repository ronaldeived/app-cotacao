using Core.Models;

namespace API.Validations;

public static class Validation
{
    public static bool ValidHouse(HouseDto house)
    {
        return house.Beneficiario != null &&
               house.Endereco != null;
    }

    public static bool IsHouse(HouseDto house)
    {
        return house.Beneficiario != null ||
               house.Endereco != null;
    }

    public static bool IsCar(CarDto carDto)
    {
        return !string.IsNullOrEmpty(carDto.Chassis) ||
               !string.IsNullOrEmpty(carDto.Placa) ||
               !string.IsNullOrEmpty(carDto.Modelo);
    }

    public static bool ValidCar(CarDto carDto)
    {
        return !string.IsNullOrEmpty(carDto.Chassis) &&
               !string.IsNullOrEmpty(carDto.Placa) &&
               !string.IsNullOrEmpty(carDto.Modelo);
    }
}