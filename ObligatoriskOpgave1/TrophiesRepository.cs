using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public class TrophiesRepository: ITrophiesRepository
    {
        // Næste id til tildeling ved oprettelse af ny trophy
        private int _nextId = 1;
        // Trophies liste med data
        private List<Trophy> _trophies = new List<Trophy>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public TrophiesRepository()
        {
            // Tilføj nogle trophies til listen
            Add(new Trophy("Premier League", 2020));
            Add(new Trophy("FA Cup", 2021));
            Add(new Trophy("Champions League", 2022));
            Add(new Trophy("La Liga", 2020));
            Add(new Trophy("Bundesliga", 2021));
        }

        /// <summary>
        /// Henter alle trophies med mulighed for filtrering og sortering
        /// </summary>
        /// <param name="year"></param>
        /// <param name="sortby"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<Trophy> Get(int? year = null, string? sortby = null)
        {
            // kopier af listen
            IEnumerable<Trophy> trophies = new List<Trophy>(_trophies);
            // FIltering
            if (year != null)
            {
                trophies = trophies.Where(t => t.Year == year);
            }
            // Sorting
            if (sortby != null)
            {
                sortby = sortby.ToLower();
                switch (sortby)
                {
                    case "competition": // fall through to next case
                    case "competition_asc":
                        trophies = trophies.OrderBy(t => t.Competition);
                        break;
                    case "competition_desc":
                        trophies = trophies.OrderByDescending(t => t.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        trophies = trophies.OrderBy(t => t.Year);
                        break;
                    case "year_desc":
                        trophies = trophies.OrderByDescending(t => t.Year);
                        break;
                    default:
                        throw new ArgumentException("Ukendt sortering: " + sortby);
                }
            }
            return trophies;
        }
        /// <summary>
        /// Henter trophy ud fra id returnerer null hvis ikke fundet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Trophy? GetById(int id)
        {
            // Find trophy med id og returner den eller null hvis ikke fundet
            return _trophies.Find(t => t.Id == id);

        }

        /// <summary>
        /// Tilføjer en ny trophy til listen og tildeler et id, returnerer den tilføjede trophy.
        /// Hvis trophy er null kastes ArgumentNullException
        /// </summary>
        /// <param name="trophy"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Trophy Add(Trophy trophy)
        {
            if (trophy == null)
            {
                throw new ArgumentNullException("Trophy kunne ikke tilføjes, fordi det er null");
            }
            // tildel id
            trophy.Id = _nextId++;
            _trophies.Add(trophy);
            return trophy;
        }

        /// <summary>
        /// Fjerner en trophy ud fra id og returnerer den fjernede trophy
        /// Hvis trophy med id ikke findes kastes ArgumentException
        /// </summary>
        /// <param name="id">Id'et på den på trophy der skal slettes</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Trophy? Remove(int id)
        {
            // Find trophy med id
            var trophy = GetById(id);
            // Returner null hvis trophy ikke findes
            if (trophy == null)
            {
                return null;
            }
            // fjern trophy
            _trophies.Remove(trophy);
            // returner den fjernede trophy
            return trophy;
        }

        /// <summary>
        /// Opdaterer en trophy ud fra id med værdierne i trophy parameteren
        /// hvis trophy med id ikke findes returneres null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trophy"></param>
        /// <returns></returns>
        public Trophy? Update(int id, Trophy trophy)
        {
            // Find trophy med id
            var existingTrophy = GetById(id);
            // returner null hvis trophy ikke findes
            if (existingTrophy == null)
            {
                return null;
            }
            // opdater værdier på den fundne trophy
            existingTrophy.Competition = trophy.Competition;
            existingTrophy.Year = trophy.Year;
            // returner den opdaterede trophy
            return existingTrophy;
        }

    }
}
