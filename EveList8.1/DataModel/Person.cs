namespace EveList8._1.DataModel
{
    public class Person
    {
        public Person(string firstName, string surName, string avatarUrl, string sex = "M")
        {
            FirstName = firstName;
            SurName   = surName;
            AvatarUrl = avatarUrl;
            Sex       = sex;
        }

        public Person()
        {
            FirstName = "User";
            SurName = "Userov";
            Sex = "M";
            AvatarUrl = "../Assets/Logo.png";
        }
        public string FirstName { get; set; }
        public string Sex       { get; set; }
        public string SurName   { get; set; }
        public string AvatarUrl { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1},\n пол: {2},\nAva: {3}", 
                FirstName,
                SurName,
                Sex,
                AvatarUrl);
        }
    }
}
