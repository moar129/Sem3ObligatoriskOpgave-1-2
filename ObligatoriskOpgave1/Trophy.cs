namespace ObligatoriskOpgave1
{
    public class Trophy
    {
        private string _competition;
        private int _year;
        public int Id { get; set; }

        /// <summary>
        /// Competition
        /// Kan ikke være Null eller Tom
        /// </summary>
        public string Competition
        {
            get { return _competition; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Competition kan ikke være Null eller Tom");
                }
                if (value.Length < 3)
                {
                    throw new ArgumentException("Competition skal være mindst 3 tegn lang");
                }
                _competition = value;
            }
        }
        /// <summary>
        /// Year 
        /// skal være imellem 1970 og 2025
        /// </summary>
        public int Year 
        { 
            get { return _year; }
            set
            {
                if (value < 1970 || value > 2025)
                {
                    throw new ArgumentException("Year skal være mellem 1970 og 2025");
                }
                _year = value;
            } 
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Trophy() :this("Unkown", 2025) { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="competition"></param>
        /// <param name="year"></param>
        public Trophy(string competition, int year)
        {
            Competition = competition;
            Year = year;
        }

        public override string ToString()
        {
            return $"Competition: {Competition}, Year: {Year}";
        }

    }
}
