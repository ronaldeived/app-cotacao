namespace QuotationConsumer.BusinessRules;

public static class CalculateRisk
{
    public static int CalculateRiskValue(DateTime dateOfBirth)
    {
        DateTime startDate = DateTime.Today.AddYears(-100);
        DateTime endDate = DateTime.Today.AddYears(-18);

        
        int age = DateTime.Today.Year - dateOfBirth.Year;
        
        if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
            age--;
        
        double ageProportion = (age - 18) / (double)(100 - 18);
        int riskValue = (int)(1000 - ageProportion * (1000 - 100));

        return riskValue;
    }
}