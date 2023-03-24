namespace Tamrin1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FirstName = Inquire("What is your first name?");
            person.LastName = Inquire("What is your sir name?");
            person.Age = InquireAge("How old are you?");
            person.Profession = InquireProfession(@$"
            What is your profession? 
            ((Choose between {Enum.GetName(Professions.BackendDeveloper)},{Enum.GetName(Professions.FrontendDeveloper)},{Enum.GetName(Professions.Designer)}))");

            var finalAnswer = Inquire("Are you positive about the accuracy of data entered? (y/n)");
            if (FinalAnswerIsAcceptable(finalAnswer))
                ShowEnteredData(person);
        }

        static void ShowEnteredData(Person person)
        {
            Console.WriteLine($"First Name : {person.FirstName}");
            Console.WriteLine($"Last Name : {person.LastName}");
            Console.WriteLine($"Age : {person.Age}");
            Console.WriteLine($"Profession : {person.Profession}");

            Console.ReadKey();
        }

        static readonly IEnumerable<string> _acceptableFinalAnswers = new[] { "y", "yes", "yeah", "1" };

        //این کد از Linq استفاده میکنه، جلو تر یاد میگیریم
        static bool FinalAnswerIsAcceptable(string answer) => _acceptableFinalAnswers.Any(x => x == answer);

        static string Inquire(string question)
        {
            Console.WriteLine(question + Environment.NewLine);
            var answer = Console.ReadLine();
            while (IsNullOrEmptyOrWhitespace(answer))
            {
                answer = TryAgain();
            }

            return answer!;
        }

        static string? TryAgain(string? prefix = null)
        {
            Console.WriteLine(prefix + "Please try again!");
            return Console.ReadLine();
        }

        static bool IsNullOrEmptyOrWhitespace(string? input) =>
            string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);

        static string InquireProfession(string question)
        {
            var strProfession = Inquire(question);

            while (UserEnteredANumber(strProfession!) || !Enum.TryParse<Professions>(strProfession, out _))
            {
                strProfession = TryAgain($@"((Choose between 
                              {Enum.GetName(Professions.BackendDeveloper)},
                              {Enum.GetName(Professions.FrontendDeveloper)},
                              {Enum.GetName(Professions.Designer)})) ");
            }

            return strProfession;
        }

        static bool UserEnteredANumber(string val) => int.TryParse(val, out _);

        static int InquireAge(string question)
        {
            var strAge = Inquire(question);

            int age;
            while (!int.TryParse(strAge, out age))
            {
                strAge = TryAgain("Age must be a number! ");
            }

            return age;
        }
    }
}