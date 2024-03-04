namespace Application.Validators;

public static class AgeValidator
{
    public static bool LegalAge(DateTime birth)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birth.Year;

        if (birth.Date > today.AddYears(-age))
            age--;

        if (age > 18)
            return true;
        else if (age == 18)
        {
            if (birth.Month < today.Month)
            {
                return true;
            }
            else if (birth.Month == today.Month)
            {
                if (birth.Day <= today.Day)
                {
                    return true;
                }
            }
        }

        return false;
    }
}