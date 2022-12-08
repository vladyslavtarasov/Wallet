namespace ConsoleWallet;

public class UserChoice
{
    public static int GetChoice(int lowerBound, int upperBound)
    {
        int choice;
        bool dataIsCorrect;
        do
        {
            dataIsCorrect = int.TryParse(Console.ReadLine(), out choice) 
                            && choice >= lowerBound && choice <= upperBound;
            
            if (dataIsCorrect) continue;
            
            Console.WriteLine("\nEnter correct value.\n");
        } while (!dataIsCorrect);
        
        Console.WriteLine();

        return choice;
    }
}