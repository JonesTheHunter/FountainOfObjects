



public class Text 
{
    public static bool IsAlpha(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        foreach (char c in str)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }
        return true;
    }
}

